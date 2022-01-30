using System;
using Game.Runtime.Systems;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.UI
{
	/// <summary>
	/// A class that controls a simple mana bar
	/// </summary>
	public sealed class ManaBar : MonoBehaviour
	{
		#region Fields

		[Header("Dependencies")]
		[SerializeField]
		private Image bar;

		[SerializeField]
		private ManaWallet wallet;

		#endregion

		#region Unity Callbacks

		private void Awake()
		{
			wallet.OnManaChanged += UpdateBarFill;
		}

		private void OnDestroy()
		{
			wallet.OnManaChanged -= UpdateBarFill;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Updates the bar fill amount
		/// </summary>
		private void UpdateBarFill()
		{
			bar.fillAmount = wallet.ManaPercentage;
		}

		#endregion
	}
}