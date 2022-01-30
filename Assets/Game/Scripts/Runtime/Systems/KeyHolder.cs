using Game.Runtime.Entities;
using Game.Runtime.Matches;
using UnityEngine;

namespace Game.Runtime.Systems
{
	/// <summary>
	/// A class that holds a key for an entity
	/// </summary>
	public sealed class KeyHolder : MonoBehaviour
	{
		#region Fields

		private bool _hasKey;

		#endregion

		#region Public Methods

		public bool HasKey()
		{
			return _hasKey;
		}

		public void GetKey()
		{
			_hasKey = true;
		}

		public void LoseKey(Entity entity)
		{
			_hasKey = false;
			entity.OnDeath -= LoseKey;
			MatchManager.Instance.IsThereKeyActive = false;
		}

		#endregion
	}
}