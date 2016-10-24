using UnityEngine;
using System.Collections;

namespace Modeler
{
	public static class Picker
	{

		/**
		 *	Performs a raycast against all GameObjects in the scene, returning the first object hit. 
		 *	Optionally ignore a set of GameObjects (for deep-clicking).
		 */
		public static bool PickGameObject(Ray worldRay, out GameObject hit, GameObject[] ignore = null)
		{
			hit = null;

			foreach(Transform t in SceneUtility.root.transform)
			{
				MeshRenderer mr = t.GetComponent<MeshRenderer>();

				if(mr == null)
					continue;

				if(mr.bounds.IntersectRay(worldRay))
				{
					hit = t.gameObject;
					return true;
				}
			}

			return false;
		}
	}
}
