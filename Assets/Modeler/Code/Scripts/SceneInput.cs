using UnityEngine;
using System.Collections;

namespace Modeler
{
	public class SceneInput : MonoBehaviour
	{
		const int LEFT_MOUSE_BUTTON = 0;

		void Update ()
		{

			if( Input.GetMouseButtonDown(LEFT_MOUSE_BUTTON) ) 
			{
				Selection.PickGameObject(Input.mousePosition);
			}
		
		}
	}
}
