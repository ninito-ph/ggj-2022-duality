using System;
using UnityEngine;

namespace Game.Runtime.Systems
{
	/// <summary>
	/// A class that manages mana
	/// </summary>
	public sealed class ManaWallet : MonoBehaviour
	{
		#region Fields

		public event Action OnManaChanged;

		[Header("Configurations")]
		[SerializeField]
		private float maxMana = 100f;

		[SerializeField]
		private float manaRegenPerSecond = 3f;

		[SerializeField]
		[Min(0f)]
		private float manaRegenDelay = 3f;

		[SerializeField]
		[Range(0f, 1f)]
		private float manaRegenThreshold = 0.5f;

		private float _lastManaUseTime = Single.MinValue;

		#endregion

		#region Properties

		public bool CanRegenMana =>
			Time.time > _lastManaUseTime + manaRegenDelay && ManaPercentage < manaRegenThreshold;

		public float Mana { get; private set; }

		public float ManaPercentage => Mana / maxMana;

		#endregion

		#region Unity Callbacks

		private void Awake()
		{
			Mana = maxMana;
		}

		private void Update()
		{
			RegenMana();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Attempts to expend mana
		/// </summary>
		/// <param name="manaCost">The cost to attempt to expend</param>
		/// <returns>Whether it was possible to expend the manaCost</returns>
		public bool TryExpendMana(float manaCost)
		{
			if (manaCost > Mana) return false;
			Mana -= manaCost;
			_lastManaUseTime = Time.time;
			OnManaChanged?.Invoke();
			return true;
		}

		/// <summary>
		/// Restores the specified amount of mana
		/// </summary>
		/// <param name="amount">The amount of mana to restore</param>
		public void RestoreMana(float amount)
		{
			Mana = Mathf.Min(Mana + amount, maxMana);
			OnManaChanged?.Invoke();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Regenerates the current mana
		/// </summary>
		private void RegenMana()
		{
			if (!CanRegenMana) return;
			RestoreMana(manaRegenPerSecond * Time.deltaTime);
		}

		#endregion
	}
}