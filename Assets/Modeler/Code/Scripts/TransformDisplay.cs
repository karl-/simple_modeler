using UnityEngine;
using System.Collections;
using System.Linq;

namespace Modeler
{
	public class TransformDisplay : MonoBehaviour
	{
		private float width { get { return GUIUtility.ScalePoint(300f); } }
		private float pad { get { return GUIUtility.ScalePoint(4f); } }

		void OnGUI()
		{
			GUI.skin = GUIUtility.skin;

			GameObject go = Selection.gameObjects.FirstOrDefault();

			if(go != null)
				GUI.Label( new Rect( Screen.width - width - pad, GUIUtility.ScalePoint(30f), width, width),
					string.Format("translation  {0,5:f2}, {1,5:f2}, {2,5:f2}\nrotation     {3,5:f2}, {4,5:f2}, {5,5:f2}\nscale        {6,5:f2}, {7,5:f2}, {8,5:f2}",
						go.transform.position.x,
						go.transform.position.y,
						go.transform.position.z,
						go.transform.localRotation.eulerAngles.x,
						go.transform.localRotation.eulerAngles.y,
						go.transform.localRotation.eulerAngles.z,
						go.transform.localScale.x,
						go.transform.localScale.y,
						go.transform.localScale.z
					),
					"RightAlignLabel");
		}
	}
}
