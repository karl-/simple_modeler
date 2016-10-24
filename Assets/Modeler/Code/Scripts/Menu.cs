using UnityEngine;
using System.Collections;

namespace Modeler
{
	public class Menu : MonoBehaviour
	{
		public Color onColor = Color.green;

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

				if( EditModeButton(EditMode.Object, "ToolbarLeft") )
					SceneInput.instance.editMode = EditMode.Object;
				if( EditModeButton(EditMode.Face, "ToolbarMid") )
					SceneInput.instance.editMode = EditMode.Face;
				if( EditModeButton(EditMode.Edge, "ToolbarMid") )
					SceneInput.instance.editMode = EditMode.Edge;
				if( EditModeButton(EditMode.Vertex, "ToolbarRight") )
					SceneInput.instance.editMode = EditMode.Vertex;

				GUI.backgroundColor = Color.white;

			GUILayout.EndHorizontal();

			/**
			 *	Settings
			 */
			GUILayout.BeginVertical("Group", GUILayout.MaxWidth(200f));

				GUILayout.Label("Settings", "Header");

			GUILayout.EndVertical();
		}

		private bool EditModeButton(EditMode mode, string style)
		{
			EditMode em = SceneInput.instance.editMode;

			GUIStyle gstyle = em == mode ? ModelerGUI.GetStyleOn(style) : GUI.skin.GetStyle(style);

			GUI.backgroundColor = em == mode ? onColor : Color.white;

			return GUILayout.Button(mode.ToString(), gstyle);
		}
	}
}
