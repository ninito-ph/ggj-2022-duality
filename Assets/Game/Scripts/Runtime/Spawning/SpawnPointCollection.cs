using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Runtime.Spawning
{
	/// <summary>
	/// A class that manages multiple spawn points at a time
	/// </summary>
	public sealed class SpawnPointCollection : MonoBehaviour
	{
		#region Fields

		[Header("Dependencies")]
		[SerializeField]
		private SpawnPoint[] spawnPoints;

		[Header("Configurations")]
		[SerializeField]
		private GameObject spawn;

		[SerializeField]
		private float spawnDelay = 1f;

		private Coroutine _spawnCoroutine;
		
		#endregion

		#region Unity Callbacks

		private void Reset()
		{
			spawnPoints = GetComponentsInChildren<SpawnPoint>();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Starts spawning the spawn
		/// </summary>
		public void StartSpawning()
		{
			if (_spawnCoroutine != null)
			{
				StopSpawning();
			}
			
			_spawnCoroutine = StartCoroutine(SpawnThroughPoints());
		}

		/// <summary>
		/// Stops spawning the spawn
		/// </summary>
		public void StopSpawning()
		{
			if (_spawnCoroutine == null) return;
			StopCoroutine(_spawnCoroutine);
		}
		
		#endregion
		
		#region Private Methods

		/// <summary>
		/// Shuffles the spawn points using a simple Fisher-Yates shuffle
		/// </summary>
		private void ShuffleSpawnPoints()
		{
			for (int i = 0; i < spawnPoints.Length; i++)
			{
				SpawnPoint temp = spawnPoints[i];
				int randomIndex = Random.Range(i, spawnPoints.Length);
				spawnPoints[i] = spawnPoints[randomIndex];
				spawnPoints[randomIndex] = temp;
			}
		}

		/// <summary>
		/// Spawns the spawn through the spawn points in random order
		/// </summary>
		private IEnumerator SpawnThroughPoints()
		{
			WaitForSeconds delay = new WaitForSeconds(spawnDelay);
			ShuffleSpawnPoints();

			while (true)
			{
				for (int index = 0; index < spawnPoints.Length; index++)
				{
					if (!spawnPoints[index].TrySpawn(spawn))
					{
						yield return null;
						continue;
					}

					yield return delay;
				}

				ShuffleSpawnPoints();
			}
		}

		#endregion
	}
}