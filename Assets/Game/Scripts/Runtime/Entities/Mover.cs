using Game.Runtime.Input;
using UnityEngine;

namespace Game.Runtime.Entities
{
	/// <summary>
	/// A simple class that moves an entity
	/// </summary>
	public abstract class Mover : MonoBehaviour
	{
		#region Properties

		public FrameInput Input { get; set; }

		#endregion
		
		#region Unity Callbacks

		protected virtual void FixedUpdate()
		{
			Move();
		}

		#endregion

		#region Public Methods

		public abstract void Move();

		#endregion
	}
}