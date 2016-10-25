using UnityEngine;

namespace Modeler
{
	public static class HandleUtility
	{
		/**
		 * Calculates a signed float delta from a current and previous mouse position.
		 * @param lhs Current mouse position.
		 * @param rhs Previous mouse position.
		 */
		public static float CalcSignedMouseDelta(Vector2 lhs, Vector2 rhs)
		{
			float delta = Vector2.Distance(lhs, rhs);
			float scale = 1f / Mathf.Min(Screen.width, Screen.height);

			// If horizontal movement is greater than vertical movement, use the X axis for sign.
			if( Mathf.Abs(lhs.x - rhs.x) > Mathf.Abs(lhs.y - rhs.y) )
				return delta * scale * ( (lhs.x-rhs.x) > 0f ? 1f : -1f );
			else
				return delta * scale * ( (lhs.y-rhs.y) > 0f ? 1f : -1f );
		}
	}
}
