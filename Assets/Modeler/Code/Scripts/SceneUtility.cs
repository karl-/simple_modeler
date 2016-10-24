using UnityEngine;

namespace Modeler
{
	/**
	 *	SceneGraph utility.
	 */
	public static class SceneUtility
	{
		private static GameObject _root = null;

		public static GameObject root
		{
			get
			{
				if(_root == null)
				{
					GameObject go = GameObject.Find("SceneGraph Root");

					if(go != null)
					{
						_root = go;
					}
					else
					{
						_root = new GameObject();
						_root.name = "SceneGraph Root";
						_root.transform.position = Vector3.zero;
					}
				}
				return _root;
			}
		}

		/**
		 *	Add a GameObject to the SceneGraph.
		 */
		public static GameObject Add(GameObject go)
		{
			go.transform.SetParent(root.transform, true);
			return go;
		}

		public static void SetWireframeEnabled(bool wireframeEnabled)
		{
			foreach(Transform t in root.transform)
			{
				if(wireframeEnabled)
					t.TryAddComponent<Wireframe>();
				else
					t.TryRemoveComponent<Wireframe>();
			}
		}
	}
}
