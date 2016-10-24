using UnityEngine;
using UMesh = UnityEngine.Mesh;
using System.Collections;

namespace Modeler
{
	public class VisualizeNormals : MonoBehaviour
	{
		public static Material unlitVertexColor { get { return ResourceUtility.Load<Material>("Material/UnlitVertexColor"); } }

		private UMesh _visualizeMesh = null;

		public UMesh visualizeMesh
		{
			get
			{
				if(_visualizeMesh == null)
				{
					_visualizeMesh = new UMesh();
				}
				return _visualizeMesh;
			}
		}

		void Update()
		{
			Graphics.DrawMesh(visualizeMesh, transform.localToWorldMatrix, unlitVertexColor, 0);
		}

	}
}
