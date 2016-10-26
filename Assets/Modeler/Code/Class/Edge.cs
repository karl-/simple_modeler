using System;
using UnityEngine;

namespace Modeler
{
	/**
	 * Stores two indices of vertex positions.
	 */
	[System.Serializable]
	public struct Edge : IEquatable<Edge>
	{
		public int x, y;

		public Edge(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public bool Equals(Edge edge)
		{
			return (x == edge.x && y == edge.y) || (x == edge.y && y == edge.x);
		}

		public override bool Equals(System.Object b)
		{
			return b != null && b is Edge && this.Equals( (Edge) b );
		}

		public override int GetHashCode()
		{
			// http://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode/263416#263416
			int hash = 27;

			unchecked
			{
				hash = hash * 29 + (x < y ? x : y);
				hash = hash * 29 + (x < y ? y : x);
			}

			return hash;
		}
	}
}
