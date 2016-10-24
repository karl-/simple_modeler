using UnityEngine;
using System.Collections;

namespace Modeler
{
	public class MainMenu : MonoBehaviour
	{
		public GUISkin skin;

		void Awake()
		{
			if(skin == null)
				Debug.LogError("GUISkin is null!");
		}

		void OnGUI()
		{
			GUI.skin = skin;

			GUILayout.BeginHorizontal();

			if(GUILayout.Button("File/New Cube"))	
				SceneUtility.Add( GameObject.CreatePrimitive(PrimitiveType.Cube) );

			GUILayout.FlexibleSpace();

			GUILayout.EndHorizontal();
		}
	}
}
