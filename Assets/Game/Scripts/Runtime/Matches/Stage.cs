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

		[Header("Dependencies")]
		[SerializeField]
		private Door door;

		[SerializeField]
		private SpawnPointCollection[] spawnPoints;

		[SerializeField]
		private KeySpawner keySpawner;

		[SerializeField]
		private PlayerSpawner playerSpawner;
		
		[SerializeField]
		private Transform cameraFocus;
		
		#endregion

		#region Public Methods

		/// <summary>
		/// Sets spawn points active
		/// </summary>
		/// <param name="active">Whether to set the spawn points active</param>
		public void SetSpawnPointsActive(bool active)
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
			
			keySpawner.StartSpawningKey();
		}
		
		#endregion
	}
}