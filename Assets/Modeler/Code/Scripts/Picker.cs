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
		public static bool PickGameObject(Vector2 mousePosition, out GameObject hit, GameObject[] ignore = null)
		{
			return PickGameObject(Camera.main.ScreenPointToRay(mousePosition), out hit, ignore);
		}

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

		public static bool PickFace(Vector2 mousePosition, out Face face, GameObject go = null)
		{
			return PickFace( Camera.main.ScreenPointToRay(mousePosition), out face, go );
		}

		public static bool PickFace(Ray worldRay, out Face face, GameObject go = null)
		{
			face = null;

			if(go == null)
			{
				if(!PickGameObject(worldRay, out go))
					return false;
			}

			MeshComponent mc = go.GetComponent<MeshComponent>();

			if(mc == null || mc.source == null)
				return false;

			Transform transform = go.transform;

			Ray localRay = new Ray(
				transform.InverseTransformPoint(worldRay.origin),
				transform.InverseTransformDirection(worldRay.direction) );

			return HandleUtility.FaceRaycast(localRay, mc.source, out face);
		}
	}
}
