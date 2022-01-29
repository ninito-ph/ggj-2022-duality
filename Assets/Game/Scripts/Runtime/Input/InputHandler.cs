using UnityEngine;

namespace Game.Runtime.Input
{
	/// <summary>
	/// A component that handles input for an entity
	/// </summary>
	public abstract class InputHandler : MonoBehaviour
	{
		#region Properties

		public bool InputSuspended { get; set; } = false;

		#endregion

		#region Unity Callbacks

		public void Update()
		{
			CollectInput();
			if (InputSuspended) return;
			DistributeInput();
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Collects input
		/// </summary>
		protected abstract void CollectInput();
		
		/// <summary>
		/// Distributes input to relevant components
		/// </summary>
		protected abstract void DistributeInput();

		#endregion
	}
}