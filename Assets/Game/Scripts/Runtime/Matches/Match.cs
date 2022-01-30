using System;
using UnityEngine;

namespace Game.Runtime.Matches
{
	/// <summary>
	/// A class that controls a match
	/// </summary>
	[Serializable]
	public sealed class Match
	{
		#region Fields

		[Header("Progress")]
		[SerializeField]
		public Stage CurrentStage;

		[SerializeField]
		public bool IsThereKeyActive = false;

		#endregion

		#region Public Methods

		/// <summary>
		/// Transitions to a new stage
		/// </summary>
		/// <param name="newStage">The stage to transition to</param>
		public void TransitionToStage(Stage newStage)
		{
			CurrentStage?.End();
			CurrentStage = newStage;
			CurrentStage.Initialize();
		}

		#endregion
	}
}