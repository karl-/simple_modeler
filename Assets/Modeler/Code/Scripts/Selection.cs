using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Modeler
{
	/**
	 *	Static class holds list of selected objects.
	 */
	public static class Selection
	{
		public static HashSet<GameObject> gameObjects = new HashSet<GameObject>();

		public static bool Add(GameObject go)
		{
			if( gameObjects.Add(go) )
			{
				go.AddComponent<Wireframe>();
				return true;
			}

			return false;
		}

		public static void Remove(GameObject go)
		{
			go.TryRemoveComponent<Wireframe>();
			gameObjects.Remove(go);
		}

		public static void Clear()
		{
			foreach(GameObject go in gameObjects)
				go.TryRemoveComponent<Wireframe>();

			gameObjects.Clear();
		}

		public static bool AppendModifier()
		{
			return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftControl);
		}

		/**
		 *	Pick a GameObject in the Scene.  Returns true if an object was selected.
		 */
		public static bool PickGameObject(Vector2 mousePosition)
		{
			if( !AppendModifier() )
				Clear();

			GameObject hit = null;

			if( Picker.PickGameObject(mousePosition, out hit, null) )
			{
				if( AppendModifier() )
				{
					if(!Add(hit))
						Remove(hit);
				}
				else
				{
					Add(hit);
				}

				return true;
			}

			return false;
		}
	}
}
