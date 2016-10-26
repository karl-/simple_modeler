using UnityEngine;

namespace Modeler
{
	/**
	 * Stores two indices of vertex positions.
	 */
	[System.Serializable]
	public struct Edge
	{
		public int x, y;

		public Edge(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}
}
