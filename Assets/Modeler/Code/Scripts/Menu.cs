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
			public bool showFaces = false;
			public bool showTriangles = false;
		}

		Dictionary<GameObject, InfoExpando> expandoDictionary = new Dictionary<GameObject, InfoExpando>();

		void OnGUI()
		{
			GUI.skin = GUIUtility.skin;

			if( Event.current.type == EventType.Repaint )
				usedRects.Clear();

			/**
			 *	Toolbar
			 */
			GUILayout.BeginHorizontal("Group", GUILayout.MinWidth(Screen.width));

				if(GUILayout.Button("New Cube"))
				{
					GameObject go = SceneUtility.AddShape(Shape.Cube);
					MeshComponent mc = go.GetComponent<MeshComponent>();
					Face face = mc.source.faces[1];
					var edges = face.GetBorderEdges();
					ElementRenderer er = go.AddComponent<ElementRenderer>();
					er.SetSelectedEdges(mc.source, edges);
				}

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
				GUILayout.BeginVertical("Group", GUILayout.MaxWidth(300f), GUILayout.MaxHeight(Screen.height - 40));

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
			MeshComponent mf = go.GetComponent<MeshComponent>();

			if(mf == null || mf.source == null)
			{
				GUILayout.Label("null");
				return;
			}

			Mesh m = mf.source;

			if( m.positions != null && GUILayout.Button("positions") )
				expando.showPositions = !expando.showPositions;

			if( expando.showPositions )
				DrawEnumerable(m.positions, DrawVec3);

			if( m.normals != null && GUILayout.Button("normals") )
				expando.showNormals = !expando.showNormals;

			if( expando.showNormals )
				DrawEnumerable(m.normals, DrawVec3);

			if( m.uvs != null && GUILayout.Button("uvs") )
				expando.showUVs = !expando.showUVs;

			if( expando.showUVs )
				DrawEnumerable(m.uvs, DrawVec2);

			if( m.faces != null && GUILayout.Button("faces") )
				expando.showFaces = !expando.showFaces;

			if( expando.showFaces )
				DrawEnumerable(m.faces, DrawFace);
		}

		private string DrawFace(Face face)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			int[] indices = face.indices;

			for(int i = 0; i < indices.Length; i += 3)
			{
				// indent lines after first to account for index
				if(i > 2)
					sb.AppendLine(string.Format("{0,8}, {1,3}, {2,3}",
						indices[i  ],
						indices[i+1],
						indices[i+2] ));
				else
					sb.AppendLine(string.Format("{0,3}, {1,3}, {2,3}",
						indices[i  ],
						indices[i+1],
						indices[i+2] ));
			}

			return sb.ToString();
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

			GUIStyle gstyle = em == mode ? GUIUtility.GetStyleOn(style) : GUI.skin.GetStyle(style);

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
