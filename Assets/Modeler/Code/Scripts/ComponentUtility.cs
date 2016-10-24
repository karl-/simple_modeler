using UnityEngine;

namespace Modeler
{
	public static class ComponentUtility
	{
		/**
		 *	Checks if the GameObject already contains component, returning the existing 
		 *	instance if so.  If not, a new component is added and returned.
		 */
		public static T TryAddComponent<T>(this GameObject go) where T : Component
		{
			T t = go.GetComponent<T>();

			if(t != null)
				return t;

			return go.AddComponent<T>();
		}

		/**
		 *	Removes a component if the GameObject has an instance.
		 */
		public static bool TryRemoveComponent<T>(this GameObject go) where T : Component
		{
			T t = go.GetComponent<T>();

			if(t == null)
				return false;

			Object.Destroy(t);

			return true;
		}
	}
}
