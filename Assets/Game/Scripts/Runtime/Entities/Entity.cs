using System;
using UnityEngine;

namespace Game.Editor.Entities
{
	/// <summary>
	/// An entity that can be killed
	/// </summary>
	public sealed class Entity : MonoBehaviour
	{
		#region Fields

		[Header("Dependencies")]
		[SerializeField]
		private InputHandler inputHandler;

		private event Action<Entity> _onDeath;

		#endregion

		#region Properties

		public InputHandler InputHandler => inputHandler;

		public event Action<Entity> OnDeath
		{
			add => _onDeath += value;
			remove => _onDeath -= value;
		}

		#endregion

		#region Public Methods

		public void Kill()
		{
			
		}

		#endregion
	}
}