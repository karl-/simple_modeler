using UnityEngine;

namespace Modeler
{
	public static class ObjectUtility
	{
		public static float CalcMinDistanceToBounds(Camera cam, Bounds bounds)
		{
			float frustumHeight = Mathf.Max(Mathf.Max(bounds.size.x, bounds.size.y), bounds.size.z);
			float distance = frustumHeight * .5f / Mathf.Tan(cam.fieldOfView * .5f * Mathf.Deg2Rad);

			return distance;
		}
	}
}
