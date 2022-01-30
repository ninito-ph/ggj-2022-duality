using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Game.Runtime.Entities;
using Ninito.UsualSuspects;
using UnityEngine;

namespace Game.Runtime.Matches
{
	/// <summary>
	/// A class that manages match progression
	/// </summary>
	public sealed class MatchManager : Singleton<MatchManager>
	{
		#region Fields

		[Header("Dependencies")]
		[SerializeField]
		private Stage[] stages;

		[SerializeField]
		private CinemachineVirtualCamera mainCamera;

		[SerializeField]
		private GameObject playerPrefab;

		[Header("Configurations")]
		[SerializeField]
		private float playerRespawnDelay = 10f;

		private List<Entity> _playerEntities;
		private Match _currentMatch;
		private int _currentStageIndex = -1;

		#endregion

		#region Properties

		public bool IsThereKeyActive
		{
			get => _currentMatch.IsThereKeyActive;
			set => _currentMatch.IsThereKeyActive = value;
		}

		#endregion

		#region Unity Callbacks

		private void Start()
		{
			ProgressStage();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Adds a new player to the match
		/// </summary>
		public void AddPlayer()
		{
			Entity newPlayer = _currentMatch.CurrentStage.PlayerSpawner.SpawnPlayer(playerPrefab);

			newPlayer.OnDeath += StartPlayerRespawn;
			_playerEntities.Add(newPlayer);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Starts a player respawn coroutine
		/// </summary>
		/// <param name="entity">The entity to respawn</param>
		private void StartPlayerRespawn(Entity entity)
		{
			StartCoroutine(RespawnPlayer(entity));
		}

		/// <summary>
		/// Respawns the player after the respawn delay
		/// </summary>
		/// <param name="entity">The entity to respawn</param>
		private IEnumerator RespawnPlayer(Entity entity)
		{
			yield return new WaitForSeconds(playerRespawnDelay);
			_currentMatch.CurrentStage.PlayerSpawner.RespawnPlayer(entity);
		}

		/// <summary>
		/// Progresses the stage to the next one
		/// </summary>
		private void ProgressStage()
		{
			if (!HasNextStage())
			{
				EndGame();
				return;
			}

			_currentMatch ??= new Match();

			if (_currentMatch.CurrentStage != null)
			{
				_currentMatch.CurrentStage.OnStageCompleted -= ProgressStage;
			}

			_currentStageIndex++;
			_currentMatch.TransitionToStage(stages[_currentStageIndex]);
			mainCamera.Follow = _currentMatch.CurrentStage.CameraFocus;

			_currentMatch.CurrentStage.OnStageCompleted += ProgressStage;
		}

		/// <summary>
		/// Checks whether a next stage exists
		/// </summary>
		/// <returns>Whether a next stage exists</returns>
		private bool HasNextStage()
		{
			return _currentStageIndex + 1 < stages.Length;
		}

		/// <summary>
		/// Ends the game
		/// </summary>
		private void EndGame()
		{
			Debug.Log("GAME OVER!");
		}

		#endregion
	}
}