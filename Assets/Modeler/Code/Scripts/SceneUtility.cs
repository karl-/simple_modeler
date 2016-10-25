using UnityEngine;
using System.Collections.Generic;

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

		public static GameObject AddShape(Shape shape)
		{
			Mesh mesh = GeometryBuilder.CreateCube();
			GameObject go = new GameObject();
			go.AddComponent<MeshComponent>().source = mesh;
			go.TryAddComponent<MeshRenderer>().sharedMaterial = ResourceUtility.Load<Material>("Material/Default");
			return Add(go);
		}

		public static void Delete(IEnumerable<GameObject> gameObjects)
		{
			foreach(GameObject go in gameObjects)
				Selection.Remove(go);

			foreach(GameObject go in gameObjects)
				GameObject.Destroy(go);
		}

	}
}
