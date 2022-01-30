using System;
using Game.Runtime.Input;
using Game.Runtime.Systems;
using UnityEngine;

namespace Game.Runtime.Entities
{
	/// <summary>
	/// An entity that can be killed
	/// </summary>
	[RequireComponent(typeof(ManaWallet))]
	public class Entity : MonoBehaviour
	{
		#region Fields

		[Header("Dependencies")]
		[SerializeField]
		private InputHandler inputHandler;

		private ManaWallet _wallet;
		private event Action<Entity> _onDeath;

		#endregion

		#region Properties

		public DualType Type { get; private set; } = DualType.Light;
		public InputHandler InputHandler => inputHandler;

		public event Action<Entity> OnDeath
		{
			add => _onDeath += value;
			remove => _onDeath -= value;
		}

		#endregion

		#region Unity Callbacks

		private void Awake()
		{
			TryGetComponent(out _wallet);
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Flips the entity's type
		/// </summary>
		public void FlipType()
		{
			Type = Type == DualType.Light ? DualType.Dark : DualType.Light;
		}
		
		/// <summary>
		/// Handles a bullet collision
		/// </summary>
		/// <param name="bullet">The bullet to handle</param>
		public void HandleBullet(Bullet.Bullet bullet)
		{
			if (bullet.Type != Type)
			{
				Kill();
			}
			else
			{
				_wallet.RestoreMana(bullet.ManaRestore);
			}
		}
		
		/// <summary>
		/// Kills the entity
		/// </summary>
		public void Kill()
		{
			inputHandler.InputSuspended = true;
			_onDeath?.Invoke(this);
			Destroy(gameObject);
		}

		#endregion
	}
}