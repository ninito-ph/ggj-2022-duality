using UnityEngine;

namespace Game.Runtime.Input
{
	/// <summary>
	/// A struct describing a ray
	/// </summary>
	public struct RayProfile
	{
		#region Fields

		public readonly Vector2 StartPos;
		public readonly Vector2 EndPos;
		public readonly Vector2 Direction;

		#endregion

		#region Constructos

		public RayProfile(Vector2 startPos, Vector2 endPos, Vector2 direction)
		{
			StartPos = startPos;
			EndPos = endPos;
			Direction = direction;
		}

		#endregion
	}
}