using UnityEngine;

namespace Game.Runtime.Entities
{
	/// <summary>
	/// A component that shoots
	/// </summary>
	public abstract class Shooter : MonoBehaviour
	{
		#region Public Methods

		public abstract void Shoot();

		#endregion
	}
}