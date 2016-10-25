using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UMesh = UnityEngine.Mesh;

namespace Modeler
{
	public static class MeshCompiler
	{
		public static UMesh CompileUnityMesh(Mesh mesh)
		{
			UMesh m = new UMesh();
			CompileUnityMesh(mesh, ref m);
			return m;
		}

		public static void CompileUnityMesh(Mesh mesh, ref UMesh umesh)
		{
			if(umesh == null)
				umesh = new UMesh();

			umesh.Clear();
			umesh.vertices = mesh.positions;
			umesh.normals = mesh.normals;
			umesh.uv = mesh.uvs;
			umesh.subMeshCount = 1;
			umesh.SetIndices(mesh.faces.SelectMany(x => x.indices).ToArray(), MeshTopology.Triangles, 0);
		}
	}
}
