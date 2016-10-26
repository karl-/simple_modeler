using UnityEngine;
using System.Collections;

namespace Modeler
{
	/**
	 * Mesh class stores vertex position and triangle data.
	 */
	[System.Serializable]
	public class Mesh
	{
		public Vector3[] positions;
		public Vector2[] uvs;
		public Vector3[] normals;
		public Vector4[] tangents;
		public Face[] faces;

		public int[][] shared;
	}
}
