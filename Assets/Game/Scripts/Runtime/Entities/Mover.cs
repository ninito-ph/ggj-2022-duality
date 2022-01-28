using UnityEngine;

namespace Game.Editor.Entities
{
	/// <summary>
	/// A simple class that moves an entity
	/// </summary>
	public abstract class Mover : MonoBehaviour
	{
		#region Fields

		private Vector2 _queuedMovement;

		#endregion

		#region Private Methods

		public void SetQueuedMovement(Vector2 movement)
		{
			_queuedMovement = movement;
		}

		public abstract void Move();

		#endregion
	}
}