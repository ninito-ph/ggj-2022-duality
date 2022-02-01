using System;
using System.Collections.Generic;
using System.Linq;
using Game.Runtime.Input;
using UnityEngine;

namespace Game.Runtime.Entities.Player
{
	/// <summary>
	/// A simple class that moves an entity
	/// </summary>
	[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
	public sealed class PlayerMover : Mover
	{
		#region Fields

		#region Collision

		[Header("Collisions")]
		[SerializeField]
		private LayerMask _groundLayer;

		[SerializeField]
		private int _collisionDetectorRayCount = 3;

		[SerializeField]
		private float _detectorRayLenght = 0.1f;

		private RayProfile _rayUp;
		private RayProfile _rayDown;

		private bool _grounded;
		private bool _hitCeiling;

		private float _lastTimeGrounded;

		#endregion

		#region Walking

		[Header("Walking")]
		[SerializeField]
		private float _acceleration = 90f;

		[SerializeField]
		private float _walkSpeedClamp = 13f;

		[SerializeField]
		private float _walkSpeedDeAcceleration = 60f;

		[SerializeField]
		private float _jumpApexSpeedBonus = 2f;

		#endregion

		#region Gravity

		[Header("Gravity")]
		[SerializeField]
		private float _fallSpeedClamp = -40f;

		[SerializeField]
		private float _minFallSpeed = -20f;

		[SerializeField]
		private float _maxFallSpeed = 120f;

		private float _fallSpeed;

		#endregion

		#region Jumping

		[Header("Jumping")]
		[SerializeField]
		private float _jumpHeight = 30f;

		[SerializeField]
		private float _jumpApexThreshold = 10f;

		[SerializeField]
		private int _coyoteTimeRange = 7;

		[SerializeField]
		private int _jumpBuffer = 7;

		[SerializeField]
		private float _earlyJumpEndGravityModifier = 3f;

		private bool _coyoteUsable;
		private bool _executedBufferedJump;
		private bool _endedJumpEarly = true;
		private float _apexPoint;
		private float _lastJumpPressed = Single.MinValue;

		#endregion

		#region General

		public event Action<bool> OnGroundedChanged;
		public event Action OnJump;
		public event Action<bool> OnMovementChanged;
		public event Action<float> OnDirectionChanged;

		private Rigidbody2D _rigidbody;
		private BoxCollider2D _collider;
		private Transform _transform;

		private Vector3 _velocity;
		private Vector3 _lastPosition;

		private float _hSpeed;
		private float _vSpeed;

		private int _fixedFrame;

		#endregion

		#endregion

		#region Properties

		public Vector3 RawMovement { get; set; }

		public bool Grounded
		{
			get => _grounded;
			private set
			{
				if (value != _grounded)
				{
					OnGroundedChanged?.Invoke(value);
				}

				_grounded = value;
			}
		}

		private bool CanUseCoyote => _coyoteUsable && !Grounded && _lastTimeGrounded + _coyoteTimeRange > _fixedFrame;

		private bool HasBufferedJump =>
			_grounded && _lastJumpPressed + _jumpBuffer > _fixedFrame && !_executedBufferedJump;

		#endregion

		#region Unity Callbacks

		private void Awake()
		{
			_transform = transform;
			TryGetComponent(out _rigidbody);
			TryGetComponent(out _collider);
		}

		private void Update()
		{
			_velocity = GetCurrentVelocity();
			_lastPosition = _transform.position;

			if (Input.JumpPress)
			{
				_lastJumpPressed = _fixedFrame;
			}

			CalculateJumpMovement();
		}

		protected override void FixedUpdate()
		{
			_fixedFrame++;
			Move();
		}

		#endregion

		#region Public Methods

		public override void Move()
		{
			CheckCollisions();

			CalculateWalkMovement();
			CalculateJumpApex();
			CalculateFallMovement();

			ExecuteMovement();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Gets the current velocity of the entity
		/// </summary>
		/// <returns>The current velocity of the entity</returns>
		private Vector3 GetCurrentVelocity()
		{
			return (_transform.position - _lastPosition) / Time.deltaTime;
		}

		/// <summary>
		/// Checks the collisions for the movement
		/// </summary>
		private void CheckCollisions()
		{
			CalculateRaysRange();
			bool groundedCheck = IsThereRayCollision(_rayDown);

			switch (Grounded)
			{
				case true when !groundedCheck:
					_lastTimeGrounded = _fixedFrame;
					break;
				case false when groundedCheck:
					_coyoteUsable = true;
					_executedBufferedJump = false;
					break;
			}

			Grounded = groundedCheck;

			_hitCeiling = IsThereRayCollision(_rayUp);
		}

		/// <summary>
		/// Checks whether the given ray profile has a collision
		/// </summary>
		/// <param name="rayProfile">The profile to test</param>
		/// <returns>Whether the given RayProfile has a collision</returns>
		private bool IsThereRayCollision(RayProfile rayProfile)
		{
			return EvaluateRayPositions(rayProfile).Any(point =>
				Physics2D.Raycast(point, rayProfile.Direction, _detectorRayLenght, _groundLayer));
		}

		/// <summary>
		/// Evaluates ray positions and ranges
		/// </summary>
		/// <param name="profile">The ray profile to evaluate</param>
		/// <returns>An IEnumerable of ray positions</returns>
		private IEnumerable<Vector2> EvaluateRayPositions(RayProfile profile)
		{
			for (int index = 0; index < _collisionDetectorRayCount; index++)
			{
				float t = (float)index / (_collisionDetectorRayCount - 1);
				yield return Vector2.Lerp(profile.StartPos, profile.EndPos, t);
			}
		}

		/// <summary>
		/// Calculates the range of the collision checking rays
		/// </summary>
		private void CalculateRaysRange()
		{
			Bounds bounds = _collider.bounds;

			_rayDown = new RayProfile(
				new Vector2(bounds.min.x, bounds.min.y),
				new Vector2(bounds.max.x, bounds.min.y),
				Vector2.down);

			_rayUp = new RayProfile(
				new Vector2(bounds.min.x, bounds.max.y),
				new Vector2(bounds.max.x, bounds.max.y),
				Vector2.up);
		}

		/// <summary>
		/// Calculates walking movement for the entity
		/// </summary>
		private void CalculateWalkMovement()
		{
			if (!Mathf.Approximately(0f, Input.AxisMovement.x))
			{
				_hSpeed += Input.AxisMovement.x * _acceleration * Time.fixedDeltaTime;
				_hSpeed = Mathf.Clamp(_hSpeed, _walkSpeedClamp * -1f, _walkSpeedClamp);

				float apexBonus = Mathf.Sign(Input.AxisMovement.x) * _jumpApexSpeedBonus * _apexPoint;
				_hSpeed += apexBonus * Time.fixedDeltaTime;
			}
			else
			{
				_hSpeed = Mathf.MoveTowards(_hSpeed, 0f, _walkSpeedDeAcceleration * Time.fixedDeltaTime);
			}
		}

		/// <summary>
		/// Calculates falling movement for the entity
		/// </summary>
		private void CalculateFallMovement()
		{
			if (Grounded)
			{
				if (_vSpeed < 0)
				{
					_vSpeed = 0;
				}
			}
			else
			{
				float fallSpeed = _endedJumpEarly && _vSpeed > 0
					? _fallSpeed * _earlyJumpEndGravityModifier
					: _fallSpeed;

				_vSpeed -= fallSpeed * Time.fixedDeltaTime;

				if (_vSpeed < _fallSpeedClamp)
				{
					_vSpeed = _fallSpeedClamp;
				}
			}
		}

		/// <summary>
		/// Calculates the jump apex point for the entity
		/// </summary>
		private void CalculateJumpApex()
		{
			if (!Grounded)
			{
				_apexPoint = Mathf.InverseLerp(_jumpApexThreshold, 0, Mathf.Abs(_velocity.y));
				_fallSpeed = Mathf.Lerp(_minFallSpeed, _maxFallSpeed, _apexPoint);
			}
			else
			{
				_apexPoint = 0;
			}
		}

		/// <summary>
		/// Calculates jumping movement for the entity
		/// </summary>
		private void CalculateJumpMovement()
		{
			if (CanJump())
			{
				Jump();
			}

			if (CanEndJumpEarly())
			{
				_endedJumpEarly = true;
			}

			if (HasHitCeiling())
			{
				_vSpeed = 0;
			}
		}

		/// <summary>
		/// Checks whether the player has hit the ceiling
		/// </summary>
		/// <returns>Whether the player has hit the ceiling</returns>
		private bool HasHitCeiling()
		{
			return _hitCeiling && _vSpeed > 0;
		}

		/// <summary>
		/// Checks whether the player can end the jump early
		/// </summary>
		/// <returns>Whether the player can end the jump early</returns>
		private bool CanEndJumpEarly()
		{
			return !_grounded && Input.JumpRelease && !_endedJumpEarly && _velocity.y > 0;
		}

		/// <summary>
		/// Executes a jump
		/// </summary>
		private void Jump()
		{
			_vSpeed = _jumpHeight;
			_endedJumpEarly = false;
			_coyoteUsable = false;
			_lastTimeGrounded = _fixedFrame;
			_executedBufferedJump = true;
			OnJump?.Invoke();
		}

		private bool CanJump()
		{
			return (Input.JumpPress && CanUseCoyote) || HasBufferedJump;
		}

		/// <summary>
		/// Executes movement for the entity
		/// </summary>
		private void ExecuteMovement()
		{
			RawMovement = new Vector3(_hSpeed, _vSpeed);
			Vector3 temporalMovement = RawMovement * Time.fixedDeltaTime;

			_rigidbody.MovePosition(_rigidbody.position + (Vector2)temporalMovement);

			OnMovementChanged?.Invoke(Mathf.Abs(temporalMovement.x) > 0.1f && _grounded);
			OnDirectionChanged?.Invoke(temporalMovement.x);
		}

		#endregion
	}
}
