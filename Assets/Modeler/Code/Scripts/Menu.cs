using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Modeler
{
	public class Menu : MonoBehaviourSingleton<Menu>
	{


		public Color onColor = Color.green;

		private List<Rect> usedRects = new List<Rect>();

		void OnGUI()
		{
			GUI.skin = ModelerGUI.skin;

			if( Event.current.type == EventType.Repaint )
				usedRects.Clear();

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

			if( Event.current.type == EventType.Repaint )
				usedRects.Add(GUILayoutUtility.GetLastRect());

			/**
			 *	Settings
			 */
			GUILayout.BeginVertical("Group", GUILayout.MaxWidth(200f));

				GUILayout.Label("Settings", "Header");

			GUILayout.EndVertical();

			usedRects.Add(GUILayoutUtility.GetLastRect());
		}

		private bool EditModeButton(EditMode mode, string style)
		{
			EditMode em = SceneInput.instance.editMode;

			GUIStyle gstyle = em == mode ? ModelerGUI.GetStyleOn(style) : GUI.skin.GetStyle(style);

			GUI.backgroundColor = em == mode ? onColor : Color.white;

			return GUILayout.Button(mode.ToString(), gstyle);
		}

		public bool IsScreenPointOverGUI(Vector2 point)
		{
			Vector2 p = new Vector2(point.x, Screen.height - point.y);
				
			foreach(Rect r in usedRects)
				if(r.Contains(p))
					return true;

			return false;
		}
	}
}
