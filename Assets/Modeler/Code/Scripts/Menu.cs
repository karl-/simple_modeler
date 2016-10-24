using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Modeler
{
	public class Menu : MonoBehaviourSingleton<Menu>
	{
		public Color onColor = Color.green;

		private List<Rect> usedRects = new List<Rect>();
		private Vector2 scrollMesh = Vector2.zero;

		class InfoExpando
		{
			public bool isVisible = false;
			public bool showPositions = false;
			public bool showNormals = false;
			public bool showUVs = false;
			public bool showTriangles = false;
		}

		Dictionary<GameObject, InfoExpando> expandoDictionary = new Dictionary<GameObject, InfoExpando>();

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
			 *	Mesh Information
			 */
			if( Selection.gameObjects.Count > 0 )
			{
				GUILayout.BeginVertical("Group", GUILayout.MaxWidth(300f));

					GUILayout.Label("Mesh Information", "Header");

					scrollMesh = GUILayout.BeginScrollView(scrollMesh);

					InfoExpando expando;

					foreach(GameObject go in Selection.gameObjects)
					{
						if( !expandoDictionary.TryGetValue(go, out expando) )
						{
							expando = new InfoExpando();
							expandoDictionary.Add(go, expando);
						}

						if( GUILayout.Button(go.name))
							expando.isVisible = !expando.isVisible;

						if(expando.isVisible)
						{
							GUILayout.BeginVertical("Group");
								DrawMeshData(go, expando);
							GUILayout.EndVertical();
						}
					}

					GUILayout.EndScrollView();

				GUILayout.EndVertical();

				usedRects.Add(GUILayoutUtility.GetLastRect());
			}
		}

		private void DrawMeshData(GameObject go, InfoExpando expando)
		{
			MeshFilter mf = go.GetComponent<MeshFilter>();

			if(mf == null || mf.sharedMesh == null)
			{
				GUILayout.Label("null");
				return;
			}

			if( GUILayout.Button("positions") )
				expando.showPositions = !expando.showPositions;

			if(expando.showPositions)
				DrawEnumerable(mf.sharedMesh.vertices, DrawVec3);

			if( GUILayout.Button("normals") )
				expando.showNormals = !expando.showNormals;

			if(expando.showNormals)
				DrawEnumerable(mf.sharedMesh.normals, DrawVec3);

			if( GUILayout.Button("uvs") )
				expando.showUVs = !expando.showUVs;

			if(expando.showUVs)
				DrawEnumerable(mf.sharedMesh.uv, DrawVec2);
		}

		private string DrawVec2(Vector2 v)
		{
			return string.Format("{0,5:f2}, {1,5:f2}", v.x, v.y);
		}

		private string DrawVec3(Vector3 v)
		{
			return string.Format("{0,5:f2}, {1,5:f2}, {2,5:f2}", v.x, v.y, v.z);
		}

		private void DrawEnumerable<T>(IEnumerable<T> array, System.Func<T, string> stringify)
		{
			int index = 0;

			foreach(T t in array)
				GUILayout.Label( string.Format("{0,3}: {1}", index++, stringify(t) ) );
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
