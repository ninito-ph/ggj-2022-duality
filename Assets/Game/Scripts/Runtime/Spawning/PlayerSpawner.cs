using System.Linq;
using Game.Runtime.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Runtime.Spawning
{
	/// <summary>
	/// A class which spawns players
	/// </summary>
	public sealed class PlayerSpawner : MonoBehaviour
	{
		#region Fields

		[Header("Dependencies")]
		[SerializeField]
		private SpawnPoint[] spawnPoints;

		#endregion

		#region Public Methods

		/// <summary>
		/// Spawns a player
		/// </summary>
		/// <param name="playerInput">The player's input provider</param>
		/// <returns>The player's Entity component</returns>
		public Entity SpawnPlayer(PlayerInput playerInput)
		{
			SpawnPoint spawnPoint = spawnPoints.FirstOrDefault(point => point.IsPointFree());

			if (spawnPoint == null)
			{
				Debug.LogError("No free spawn points!");
				return null;
			}

			Debug.Log(spawnPoint.name);
			playerInput.transform.SetPositionAndRotation(spawnPoint.transform.position, Quaternion.identity);
			return playerInput.GetComponent<Entity>();
		}

		/// <summary>
		/// Spawns a single player
		/// </summary>
		public void RespawnPlayer(Entity playerEntity)
		{
			SpawnPoint spawnPoint = spawnPoints.FirstOrDefault(point => point.IsPointFree());

			if (spawnPoint == null)
			{
				Debug.LogError("No free spawn points!");
				return;
			}

			playerEntity.transform.position = spawnPoint.transform.position;
			playerEntity.Revive();
		}

		#endregion
	}
}
