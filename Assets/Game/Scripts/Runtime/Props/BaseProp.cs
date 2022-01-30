using UnityEngine;

namespace Game.Runtime.Props
{
	[RequireComponent(typeof(AudioSource))]
	public class BaseProp : MonoBehaviour
	{
		[SerializeField]
		protected AudioClip audioClip;

		protected AudioSource audioSource;

		protected virtual void Start()
		{
			audioSource = GetComponent<AudioSource>();
		}
	}
}