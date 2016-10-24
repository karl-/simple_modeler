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
					_root = new GameObject();
					_root.name = "SceneGraph Root";
					_root.transform.position = Vector3.zero;
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
	}
}
