using UnityEngine;
using System.Collections;

namespace Modeler
{
	public static class GeometryBuilder
	{
		public static Mesh CreateCube()
		{
			float r = .5f;

			Mesh m = new Mesh();

			m.positions = new Vector3[]
			{
				// bottom
				new Vector3(-r, -r, -r), // 0
				new Vector3( r, -r, -r), // 3
				new Vector3(-r,  r, -r), // 2
				new Vector3( r, -r, -r), // 1

				// front
				new Vector3(-r, -r, -r), // 0
				new Vector3( r, -r, -r), // 1
				new Vector3(-r,  r, -r), // 4
				new Vector3( r,  r, -r), // 5

				// new Vector3(-r,  r, -r), // 4
				// new Vector3( r,  r, -r), // 5
				// new Vector3(-r,  r, -r), // 6
				// new Vector3( r,  r, -r), // 7
			};

			m.normals = new Vector3[]
			{
				new Vector3( 0f, -1f,  0f),
				new Vector3( 0f, -1f,  0f),
				new Vector3( 0f, -1f,  0f),
				new Vector3( 0f, -1f,  0f),

				new Vector3( 0f,  0f,  1f),
				new Vector3( 0f,  0f,  1f),
				new Vector3( 0f,  0f,  1f),
				new Vector3( 0f,  0f,  1f)
			};

			m.faces = new Face[]
			{
				new Face(0, 1, 2, 1, 3, 2),
				new Face(0, 1, 4, 1, 5, 4)
			};

			return m;
		}
	}
}
