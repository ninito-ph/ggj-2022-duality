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
		
		private Match _currentMatch;

		#endregion

		#region Properties

		public bool IsThereKeyActive => _currentMatch.IsThereKeyActive;

		#endregion

		#region



		#endregion
	}
}