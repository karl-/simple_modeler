using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Modeler
{
	[System.Serializable]
	public class Face
	{
		public int[] indices;

		public Face(params int[] indices)
		{
			this.indices = indices;
		}

		public HashSet<Edge> GetBorderEdges()
		{
			if(indices == null)
				return null;

			// probably could do better than 2 collections
			// for this task
			HashSet<Edge> distinct = new HashSet<Edge>();
			List<Edge> dup = new List<Edge>();

			for(int i = 0; i < indices.Length; i+=3)
			{
				Edge a = new Edge(indices[i+0], indices[i+1]);
				Edge b = new Edge(indices[i+1], indices[i+2]);
				Edge c = new Edge(indices[i+2], indices[i+0]);

				if(!distinct.Add(a)) dup.Add(a);
				if(!distinct.Add(b)) dup.Add(b);
				if(!distinct.Add(c)) dup.Add(c);
			}

			distinct.ExceptWith(dup);

			return distinct;
		}
	}
}
