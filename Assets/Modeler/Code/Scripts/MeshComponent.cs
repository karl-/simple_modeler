using UnityEngine;
using UMesh = UnityEngine.Mesh;

namespace Modeler
{
	[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
	public class MeshComponent : MonoBehaviour
	{
		private Mesh _source = null;
		private MeshFilter _meshFilter = null;
		public MeshFilter meshFilter
		{
			get
			{
				if(_meshFilter == null)
					_meshFilter = gameObject.TryAddComponent<MeshFilter>();
				return _meshFilter;
			}
		}

		public Mesh source
		{
			get
			{
				return _source;
			}

			set
			{
				_source = value;
				Rebuild();
			}
		}

		public void Rebuild()
		{
			UMesh m = meshFilter.sharedMesh;
			MeshCompiler.CompileUnityMesh(_source, ref m);
			meshFilter.sharedMesh = m;
		}
	}
}
