using Game.Entities.Input.Player;
using Game.Runtime.Input;
using UnityEngine;

namespace Game.Runtime.UI
{
	/// <summary>
	/// A class that manages an arrow pointer
	/// </summary>
	public sealed class AimArrow : MonoBehaviour
	{
		#region Fields

		[Header("Dependencies")]
		[SerializeField]
		public Transform arrow;

		[Header("Configuration")]
		[SerializeField]
		public Transform centerTransform;

		[SerializeField]
		public float arrowAreaRadius = 0.5f;

		[SerializeField]
		public float arrowRotationOffset = 90f;

		private Camera _mainCamera;

		#endregion

		#region Properties

		public FrameInput Input { get; set; }
		private Camera MainCamera => _mainCamera ? _mainCamera : _mainCamera = Camera.main;
		private float Distance => Vector2.Distance(arrow.position, centerTransform.position);

		#endregion

		#region Unity Callbacks

		private void LateUpdate()
		{
			AdjustArrowPosition();
			AlignArrow();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Limits the arrow's position to the area radius
		/// </summary>
		private void AdjustArrowPosition()
		{
			Vector3 centerPosition = centerTransform.position;

			if(Input.IsGamepad) {
				Vector3 analogPosition = new Vector3(Input.Aim.x, Input.Aim.y, 0f);

				arrow.position = centerPosition + analogPosition.normalized * arrowAreaRadius;
			} else {
				Vector3 screenPointCenter = MainCamera.WorldToScreenPoint(centerPosition);
				Vector2 mousePosition = Input.Aim;

				arrow.position = centerPosition +
				(new Vector3(mousePosition.x, mousePosition.y, 0) - screenPointCenter).normalized * arrowAreaRadius;
			}

		}

		/// <summary>
		/// Makes the arrow point from the center to the mouse
		/// </summary>
		private void AlignArrow()
		{
			float angle = 0f;

			if(Input.IsGamepad) {
				angle = Mathf.Atan2(-Input.Aim.y, -Input.Aim.x) * Mathf.Rad2Deg;
			} else {
				Vector2 centerPosition = MainCamera.WorldToViewportPoint(centerTransform.position);
				Vector2 mousePosition = Input.Aim;
				Vector2 viewportMousePosition = MainCamera.ScreenToViewportPoint(new Vector3(mousePosition.x, mousePosition.y, 0));

				angle = GetAngleBetweenTwoPoints(centerPosition, viewportMousePosition);
			}

			transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + arrowRotationOffset));
		}

		/// <summary>
		/// Gets an angle between two points
		/// </summary>
		/// <param name="a">The point a to get an angle from</param>
		/// <param name="b">The point b to get an angle from</param>
		/// <returns>An angle pointing from point a to point b</returns>
		private static float GetAngleBetweenTwoPoints(Vector2 a, Vector2 b)
		{
			return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
		}

		#endregion
	}
}
