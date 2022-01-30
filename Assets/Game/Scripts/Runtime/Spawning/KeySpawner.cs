using System.Collections;
using Game.Runtime.Matches;
using UnityEngine;

namespace Game.Runtime.Spawning
{
	/// <summary>
	/// A class that spawns keys
	/// </summary>
	public sealed class KeySpawner : MonoBehaviour
	{
		#region Fields

		[Header("Dependencies")]
		[SerializeField]
		private MatchManager matchManager;
		
		[SerializeField]
		private SpawnPoint[] spawnPoints;
		
		[SerializeField]
		private GameObject keyPrefab;

		private Coroutine _spawnCoroutine;
		
		#endregion

		#region Public Methods

		/// <summary>
		/// Starts spawning keys
		/// </summary>
		public void StartSpawningKey()
		{
			if (_spawnCoroutine != null)
			{
				StopSpawningKey();
			}
			
			_spawnCoroutine = StartCoroutine(SpawnKeyPeriodically());
		}
		
		/// <summary>
		/// Stops spawning keys
		/// </summary>
		public void StopSpawningKey()
		{
			if (_spawnCoroutine == null) return;
			StopCoroutine(_spawnCoroutine);
		}

		#endregion
		
		#region Private Methods

		/// <summary>
		/// Spawns the spawn through the spawn points in random order
		/// </summary>
		private IEnumerator SpawnKeyPeriodically()
		{
			WaitForSeconds delay = new WaitForSeconds(5f);

			while (true)
			{
				if (matchManager.IsThereKeyActive)
				{
					yield return delay;
				}
				else
				{
					spawnPoints[Random.Range(0, spawnPoints.Length)].TrySpawn(keyPrefab);
					yield return delay;
				}
			}
		}

		#endregion
	}
}