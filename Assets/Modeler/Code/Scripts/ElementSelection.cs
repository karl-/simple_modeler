using UnityEngine;
using System.Collections.Generic;

namespace Modeler
{
		[System.Serializable]
		public class ElementSelection
		{
			List<Face> faces;
			List<Edge> edges;
			List<int> indices;
		}
}
