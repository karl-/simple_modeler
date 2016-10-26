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

		public static bool FaceRaycast(Ray localRay, Mesh mesh, out Face face)
		{
			face = null;
			Vector3[] positions = mesh.positions;

			for(int i = 0; i < mesh.faces.Length; i++)
			{
				int[] indices = mesh.faces[i].indices;

				for(int n = 0; n < indices.Length; n += 3)
				{
					Vector3 a = positions[indices[n    ]];
					Vector3 b = positions[indices[n + 1]];
					Vector3 c = positions[indices[n + 2]];

					Vector3 normal = Vector3.Cross(b-a, c-a);

					// test if face is culled by back, potentially skipping costly ray-tri intersect test
					if( Vector3.Dot(normal, localRay.direction) < 0f )
						continue;

					if( MathUtility.RayIntersectsTriangle(localRay, a, b, c) )
					{
						face = mesh.faces[i];
						return true;
					}
				}
			}

			return false;
		}
	}
}
