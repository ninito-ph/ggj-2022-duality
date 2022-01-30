using UnityEngine;

namespace Game.Runtime.Input
{
	/// <summary>
	/// The inputs at a given frame
	/// </summary>
	public struct FrameInput
	{
		public Vector2 AxisMovement;
		public bool JumpPress;
		public bool JumpRelease;
		public bool Fire;
		public bool SwapElement;
		public bool IsGamepad;
		public Vector2 Aim;
	}
}
