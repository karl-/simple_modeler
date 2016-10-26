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
		public static Dictionary<GameObject, ElementSelection> gameObjects = new Dictionary<GameObject, ElementSelection>();

		public static bool Add(GameObject go)
		{
			if( !gameObjects.ContainsKey(go) )
			{
				gameObjects.Add(go, new ElementSelection());
				go.AddComponent<Wireframe>();
				return true;
			}

			return false;
		}

		public static void Remove(GameObject go)
		{
			go.TryRemoveComponent<Wireframe>();

			if(gameObjects.ContainsKey(go))
				gameObjects.Remove(go);
		}

		public static void Clear()
		{
			foreach(var kvp in gameObjects)
				kvp.Key.TryRemoveComponent<Wireframe>();

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

		public static bool PickFace(Vector2 mousePosition)
		{
				// 			foreach(GameObject go in Selection.gameObjects)
				// {
				// 	Face face;

				// 	if( Picker.PickFace(Input.mousePosition, out face) )
				// 	{

				// 	}

			return false;
		}
	}
}
