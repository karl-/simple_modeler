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


			/**
			 *    0_____1
			 *    |   / |
			 *    | /   |
			 *    2-----3
			 */

			m.positions = new Vector3[]
			{
				// bottom
				new Vector3(-r, -r, -r), // 0
				new Vector3( r, -r, -r), // 1
				new Vector3(-r, -r,  r), // 2
				new Vector3( r, -r,  r), // 3

				// front
				new Vector3(-r,  r, -r), // 4
				new Vector3( r,  r, -r), // 5
				new Vector3(-r, -r, -r), // 0
				new Vector3( r, -r, -r), // 1

				// right
				new Vector3( r,  r, -r), // 4
				new Vector3( r,  r,  r), // 5
				new Vector3( r, -r, -r), // 0
				new Vector3( r, -r,  r), // 1

				// back
				new Vector3( r,  r,  r), // 4
				new Vector3(-r,  r,  r), // 5
				new Vector3( r, -r,  r), // 0
				new Vector3(-r, -r,  r), // 1

				// left
				new Vector3(-r,  r,  r), // 4
				new Vector3(-r,  r, -r), // 5
				new Vector3(-r, -r,  r), // 0
				new Vector3(-r, -r, -r), // 1

				// top
				new Vector3(-r,  r,  r), // 4
				new Vector3( r,  r,  r), // 5
				new Vector3(-r,  r, -r), // 0
				new Vector3( r,  r, -r), // 1
			};

			// 6 sides to a cube
			m.faces = new Face[6];

			for(int i = 0; i < 6; i++)
			{
				int ind = 4 * i;
				m.faces[i] = new Face(ind + 0, ind + 1, ind + 2, ind + 1, ind + 3, ind + 2);
			}

			return m;
		}
	}
}
