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
		// If this were a more serious project it would make sense to
		// pool UMesh resources.
		private UMesh _mesh = null;

		private static Material faceHighlightMaterial
		{
			get
			{
				return ResourceUtility.Load<Material>("Material/FaceAndEdgeHighlight");
			}
		}

		public void SetSelectedFaces(Mesh mesh, IEnumerable<Face> selection)
		{
			CreateFaceMesh(mesh, selection, ref _mesh);
		}

		public void SetSelectedEdges(Mesh mesh, IEnumerable<Edge> selection)
		{
			CreateEdgeMesh(mesh, selection, ref _mesh);
		}

		public void Clear()
		{
			if(_mesh != null)
				Object.Destroy(_mesh);
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

		void CreateFaceMesh(Mesh mesh, IEnumerable<Face> faces, ref UMesh umesh)
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

		void CreateEdgeMesh(Mesh mesh, IEnumerable<Edge> edges, ref UMesh umesh)
		{
			if(umesh == null)
				umesh = new UMesh();

			umesh.Clear();

			int vertexCount = edges.Count() * 2;

			Vector3[] vertices = new Vector3[vertexCount];
			int[] indices = new int[vertexCount];
			int index = 0;

			foreach(Edge e in edges)
			{
				vertices[index    ] = mesh.positions[e.x];
				vertices[index + 1] = mesh.positions[e.y];

				indices[index    ] = index;
				indices[index + 1] = index + 1;
				index += 2;
			}

			umesh.vertices = vertices;
			umesh.subMeshCount = 1;
			umesh.SetIndices(indices, MeshTopology.Lines, 0);
		}
	}
}
