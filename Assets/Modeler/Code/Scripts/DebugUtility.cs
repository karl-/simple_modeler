using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Modeler
{
	/**
	 * Various helper methods for debugging.
	 */
	public static class DebugUtility
	{
		/**
		 * Returns a formatted string from an array.
		 */
		public static string ToString<T>(this IEnumerable<T> enumerable, string separator = "\n")
		{
			return string.Join(separator, enumerable.Select(x => x.ToString()).ToArray());
		}
	}
}
