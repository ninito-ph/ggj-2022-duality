using System;
using Game.Runtime.Entities;
using Game.Runtime.Systems;
using UnityEngine;

namespace Game.Runtime.Props.Interactables
{
	/// <summary>
	/// A class that manages an unlockable door
	/// </summary>
	public sealed class Door : BaseInteractable
	{
		#region Fields

		public Action OnDoorOpened; 
		
		private Animator animator;

		#endregion

		#region Unity Callbacks

		protected override void Start()
		{
			base.Start();
			animator = GetComponent<Animator>();
		}

		protected override void OnTriggerEnter2D(Collider2D collider)
		{
			if (!collider.TryGetComponent(out KeyHolder keyHolder)) return;
			if (!keyHolder.HasKey()) return;

			PlayInteractionFeedback();
			animator.Play("Unlock");
			keyHolder.LoseKey(keyHolder.GetComponent<Entity>());
			
			OnDoorOpened?.Invoke();
		}

		#endregion
	}
}
