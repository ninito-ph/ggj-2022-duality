using System;
using UnityEngine;

namespace Game.Runtime.Entities.Bullet
{
	/// <summary>
	/// A class that controls a bullet
	/// </summary>
	[RequireComponent(typeof(Collider2D))]
	public sealed class Bullet : MonoBehaviour
	{
		#region Fields

		[Header("Configurations")]
		[SerializeField]
		private float speed = 10f;

		[SerializeField]
		private float lifeTime = 5f;

		[SerializeField]
		private float manaRestore = 15f;

		[SerializeField]
		private DualType _type = DualType.Dark;

		[Header("VFX")]
		[SerializeField]
		private GameObject hitVFX;

		[SerializeField]
		private GameObject endVFX;

		private float _birthTime;

		#endregion

		#region Properties

		public float ManaRestore => manaRestore;
		public Shooter Shooter { get; set; }
		public DualType Type => _type;

		private bool IsLifetimeOver => Time.time > _birthTime + lifeTime;

		#endregion

		#region Unity Callbacks

		private void Awake()
		{
			_birthTime = Time.time;
		}

		public void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject == Shooter.gameObject) return;

			if (!collision.gameObject.CompareTag("Player"))
			{
				Destroy(gameObject);
			}

			if (collision.gameObject.TryGetComponent(out Entity entity))
			{
				HitEntity(entity);
			}
		}

		public void FixedUpdate()
		{
			transform.Translate(Vector3.up * speed * Time.deltaTime);

			if (!IsLifetimeOver) return;

			Destroy(gameObject);
		}

		private void OnDestroy()
		{
			if (endVFX != null)
			{
				Instantiate(endVFX, transform.position, Quaternion.identity);
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Hits an entity with the bullet
		/// </summary>
		/// <param name="entity">The entity to hit</param>
		private void HitEntity(Entity entity)
		{
			entity.HandleBullet(this);

			if (hitVFX != null)
			{
				Instantiate(hitVFX, transform.position, Quaternion.identity);
			}

			Destroy(gameObject);
		}

		#endregion
	}
}