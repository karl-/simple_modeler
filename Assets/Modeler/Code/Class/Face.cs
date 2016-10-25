using UnityEngine;

namespace Modeler
{
	public class Face
	{
		public int[] indices;

		public Face(params int[] indices)
		{
			this.indices = indices;
		}
	}
}
