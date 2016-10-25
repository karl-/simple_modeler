using UnityEngine;
using UMesh = UnityEngine.Mesh;
namespace Modeler
{
	/**
	 *	Draws a wireframe over the GameObject.
	 */
	public class Wireframe : MonoBehaviour
	{
		UMesh wireframeMesh = null;

		static Material wireframeMaterial { get { return ResourceUtility.Load<Material>("Material/Wireframe"); } }

		void Awake()
		{
			MeshFilter mf = GetComponent<MeshFilter>();

			if(mf == null || mf.sharedMesh == null)
				Object.Destroy(this);

			wireframeMesh = GenerateBarycentric(mf.sharedMesh);
		}

		void Update()
		{
			Graphics.DrawMesh(wireframeMesh, transform.localToWorldMatrix, wireframeMaterial, 0);
		}

		/**
		 * Rebuild mesh with individual triangles, adding barycentric coordinates
		 * in the colors channel.
		 */
		UMesh GenerateBarycentric(UMesh m)
		{
			int[] tris = m.triangles;
			int triangleCount = tris.Length;

			Vector3[] mesh_vertices		= m.vertices;

			Vector3[] vertices 	= new Vector3[triangleCount];
			Color[] colors 		= new Color[triangleCount];

			for(int i = 0; i < triangleCount; i++)
			{
				vertices[i] = mesh_vertices[tris[i]];
				colors[i] = i % 3 == 0 ? new Color(1, 0, 0, 0) : (i % 3) == 1 ? new Color(0, 1, 0, 0) : new Color(0, 0, 1, 0);
				tris[i] = i;
			}

			UMesh res = new UMesh();

			res.Clear();
			res.vertices = vertices;
			res.triangles = tris;
			res.colors = colors;

			return res;
		}
	}
}
