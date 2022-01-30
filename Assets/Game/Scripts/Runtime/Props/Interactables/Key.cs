using Game.Runtime.Entities;
using Game.Runtime.Matches;
using Game.Runtime.Systems;
using UnityEngine;

namespace Game.Runtime.Props.Interactables
{
	public class Key : BaseInteractable
	{
		protected override void Start()
		{
			base.Start();
			MatchManager.Instance.IsThereKeyActive = true;
		}

		protected override void OnTriggerEnter2D(Collider2D collider)
		{
			if (!collider.TryGetComponent(out KeyHolder keyHolder)) return;
			
			PlayInteractionFeedback();
			
			keyHolder.GetKey();
			keyHolder.GetComponent<Entity>().OnDeath += keyHolder.LoseKey;

			GetComponent<Renderer>().enabled = false;
			GetComponent<Collider>().enabled = false;

			if (audioClip != null)
			{
				Destroy(gameObject, audioClip.length);
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}
}
