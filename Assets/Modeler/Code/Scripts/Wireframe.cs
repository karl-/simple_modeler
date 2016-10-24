using UnityEngine;

namespace Modeler
{
	/**
	 *	Draws a wireframe over the GameObject.
	 */
	public class Wireframe : MonoBehaviour
	{
		Mesh wireframeMesh = null;

		static Material _wireframeMaterial = null;

		static Material wireframeMaterial
		{
			get
			{
				if(_wireframeMaterial == null)
					_wireframeMaterial = (Material) Resources.Load("Material/Wireframe", typeof(Material));
				return _wireframeMaterial;
			}
		}

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
		Mesh GenerateBarycentric(Mesh m)
		{
			int[] tris = m.triangles;
			int triangleCount = tris.Length;

			Vector3[] mesh_vertices		= m.vertices;
			Vector3[] mesh_normals		= m.normals;
			Vector2[] mesh_uv			= m.uv;

			Vector3[] vertices 	= new Vector3[triangleCount];
			Vector3[] normals 	= new Vector3[triangleCount];
			Vector2[] uv 		= new Vector2[triangleCount];
			Color[] colors 		= new Color[triangleCount];

			for(int i = 0; i < triangleCount; i++)
			{
				vertices[i] = mesh_vertices[tris[i]];
				normals[i] 	= mesh_normals[tris[i]];
				uv[i] 		= mesh_uv[tris[i]];

				colors[i] = i % 3 == 0 ? new Color(1, 0, 0, 0) : (i % 3) == 1 ? new Color(0, 1, 0, 0) : new Color(0, 0, 1, 0);

				tris[i] = i;
			}

			Mesh res = new Mesh();

			res.Clear();
			res.vertices = vertices;
			res.triangles = tris;
			res.normals = normals;
			res.colors = colors;
			res.uv = uv;

			return res;
		}
	}
}