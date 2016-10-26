using UnityEngine;
using UMesh = UnityEngine.Mesh;
using System.Linq;
using System.Collections.Generic;

namespace Modeler
{
	/**
	 * Render face, edge, or vertex selection.
	 */
	public class ElementRenderer : MonoBehaviour
	{
		private UMesh _mesh = null;

		private static Material faceHighlightMaterial
		{
			get
			{
				return ResourceUtility.Load<Material>("Material/FaceAndEdgeHighlight");
			}
		}

		public void SetSelectedFaces(Mesh mesh, Face[] selection)
		{
			CreateFaceMesh(mesh, selection, ref _mesh);
		}

		void Update()
		{
			Graphics.DrawMesh(_mesh, transform.localToWorldMatrix, faceHighlightMaterial, 0);
		}

		void OnDestroy()
		{
			if( _mesh != null )
				Object.Destroy( _mesh );
		}

		void CreateFaceMesh(Mesh mesh, Face[] faces, ref UMesh umesh)
		{
			if(umesh == null)
				umesh = new UMesh();

			umesh.Clear();

			int[] tris = faces.SelectMany(x => x.indices).ToArray();
			int vertexCount = tris.Length;

			Vector3[] vertices = new Vector3[vertexCount];
			int[] indices = new int[vertexCount];

			for(int i = 0; i < vertexCount; i++)
			{
				vertices[i] = mesh.positions[tris[i]];
				indices[i] = i;
			}

			umesh.vertices = vertices;
			umesh.triangles = indices;
		}
	}
}
