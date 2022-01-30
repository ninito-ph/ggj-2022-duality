using System;
using Game.Runtime.Props.Interactables;
using Game.Runtime.Spawning;
using UnityEngine;

namespace Game.Runtime.Matches
{
	/// <summary>
	/// A class that contains data about a stage
	/// </summary>
	[Serializable]
	public sealed class Stage
	{
		#region Fields

		public Action OnStageCompleted;
		
		[Header("Dependencies")]
		[SerializeField]
		private Door door;

		[SerializeField]
		private SpawnPointCollection[] spawnPoints;

		[SerializeField]
		private KeySpawner keySpawner;

		[SerializeField]
		private PlayerSpawner _playerSpawner;
		
		[SerializeField]
		private Transform cameraFocus;
		
		#endregion

		#region Properties

		public PlayerSpawner PlayerSpawner => _playerSpawner;
		public Transform CameraFocus => cameraFocus;

		#endregion

		#region Public Methods

		/// <summary>
		/// Initializes the stage
		/// </summary>
		public void Initialize()
		{
			door.OnDoorOpened += HandleDoorOpening;
			SetSpawnPointsActive(true);
		}

		/// <summary>
		/// Ends the stage
		/// </summary>
		public void End()
		{
			door.OnDoorOpened -= HandleDoorOpening;
			SetSpawnPointsActive(false);
		}
		
		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Sets spawn points active
		/// </summary>
		/// <param name="active">Whether to set the spawn points active</param>
		private void SetSpawnPointsActive(bool active)
		{
			foreach (SpawnPointCollection collection in spawnPoints)
			{
				if (active)
				{
					collection.StartSpawning();
				}
				else
				{
					collection.StopSpawning();
				}
			}

			if (active)
			{
				keySpawner.StartSpawningKey();
			}
			else
			{
				keySpawner.StopSpawningKey();
			}
		}
		
		/// <summary>
		/// Marks the stage as complete when the door is opened
		/// </summary>
		private void HandleDoorOpening()
		{
			OnStageCompleted?.Invoke();
		}
		
		#endregion
	}
}