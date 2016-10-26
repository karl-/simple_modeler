using UnityEngine;

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
	}
}
