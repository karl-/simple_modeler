using UnityEngine;
using System.Collections;

namespace Modeler
{
	public class Menu : MonoBehaviour
	{
		bool wireframeEnabled = true;

		void OnGUI()
		{
			GUI.skin = ModelerGUI.skin;

			/**
			 *	Toolbar
			 */
			GUILayout.BeginHorizontal("Group", GUILayout.MinWidth(Screen.width));

				if(GUILayout.Button("New Cube"))	
					SceneUtility.Add( GameObject.CreatePrimitive(PrimitiveType.Cube) );

				GUILayout.FlexibleSpace();

			GUILayout.EndHorizontal();

			/**
			 *	Settings
			 */
			GUILayout.BeginVertical("Group", GUILayout.MaxWidth(200f));

				GUILayout.Label("Settings", "Header");

				if( GUILayout.Button("Toggle Wireframe") )
				{
					wireframeEnabled = !wireframeEnabled;
					SceneUtility.SetWireframeEnabled(wireframeEnabled);
				}

			GUILayout.EndVertical();

		}
	}
}
