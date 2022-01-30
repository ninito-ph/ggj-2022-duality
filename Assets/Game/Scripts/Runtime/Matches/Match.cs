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
		public bool IsThereKeyActive;

		#endregion
	}
}