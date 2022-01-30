using System;
using Game.Runtime.Systems;
using UnityEngine;

namespace Game.Runtime.Entities.Player
{
	/// <summary>
	/// A class that is responsible for shooting bullets
	/// </summary>
	public sealed class PlayerShooter : Shooter
	{
		#region Fields

		[Header("Dependencies")]
		[SerializeField]
		public ManaWallet manaSource;

		[SerializeField]
		public Entity shooterEntity;
		
		[SerializeField]
		public Transform bulletSpawnPoint;

		[Header("Bullets")]
		[SerializeField]
		private GameObject darkBulletPrefab;

		[SerializeField]
		private GameObject lightBulletPrefab;

		[Header("Configurations")]
		[SerializeField]
		public float manaCostPerShot = 17.5f;

		[SerializeField]
		private float fireInterval = 0.15f;

		private float _lastFireTime = Single.MinValue;

		#endregion

		#region Properties

		private bool CanFire => manaSource.Mana >= manaCostPerShot && Time.time > _lastFireTime + fireInterval;

		#endregion

		#region Public Methods
		
		public override void Shoot()
		{
			if (!CanFire) return;

			GameObject bullet = InstantiateBullet();
			
			bullet.GetComponent<Bullet.Bullet>().Shooter = this;
			manaSource.TryExpendMana(manaCostPerShot);
			_lastFireTime = Time.time;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Instantiates a bullet GameObject
		/// </summary>
		/// <returns></returns>
		private GameObject InstantiateBullet()
		{
			return Instantiate(shooterEntity.Type == DualType.Dark ? darkBulletPrefab : lightBulletPrefab,
				GetBulletSpawnPosition(), bulletSpawnPoint.rotation);
		}

		/// <summary>
		/// Gets the bullet's spawn position
		/// </summary>
		/// <returns>The bullet's spawn position</returns>
		private Vector3 GetBulletSpawnPosition()
		{
			Vector3 bulletSpawnPosition = bulletSpawnPoint.position;
			bulletSpawnPosition.z = 0;
			return bulletSpawnPosition;
		}

		#endregion
	}
}