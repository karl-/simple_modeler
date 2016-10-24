using UnityEngine;
using System.Collections.Generic;

namespace Modeler
{
	public static class ResourceUtility
	{
		private static Dictionary<string, Object> loaded = new Dictionary<string, Object>();

		public static T Load<T>(string path) where T : Object
		{
			Object value = null;

			if(loaded.TryGetValue(path, out value))
				return (T) value;

			value = Resources.Load(path, typeof(T));

			loaded.Add(path, value);

			return (T) value;
		}
	}
}
