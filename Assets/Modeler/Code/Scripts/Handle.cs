using UnityEngine;
using UMesh = UnityEngine.Mesh;

namespace Modeler
{
	public static class Handle
	{
		private static UMesh _translateMesh = null;

		private static UMesh translateMesh
		{
			get
			{
				if( _translateMesh == null )
				{
					UMesh m = new UMesh();

					m.vertices = new Vector3[]
					{
						Vector3.zero,
						Vector3.up,
						Vector3.right,
						Vector3.forward
					};

					m.colors = new Color[]
					{
						Color.white,
						Color.green,
						Color.red,
						Color.blue
					};

					m.subMeshCount = 1;
					m.SetIndices(new int[] { 0, 1, 0, 2, 0, 3 }, MeshTopology.Lines, 0);
					_translateMesh = m;
				}

				return _translateMesh;
			}
		}

		private static Material _handleMaterial = null;

		private static Material handleMaterial
		{
			get
			{
				if(_handleMaterial == null)
					_handleMaterial = (Material) Resources.Load("Material/Handle", typeof(Material));

				return _handleMaterial;
			}
		}

		private static Matrix4x4 translateMatrix = Matrix4x4.identity;

		public static Vector3 Translate(Vector3 position)
		{
			translateMatrix.SetTRS(position, Quaternion.identity, Vector3.one);
			Graphics.DrawMesh(translateMesh, translateMatrix, handleMaterial, 0);
			return position;
		}
	}
}
