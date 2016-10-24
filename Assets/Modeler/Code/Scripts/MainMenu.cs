using UnityEngine;
using System.Collections;

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
			Debug.Log("boogers");

		if(GUILayout.Button("File/Boogesr Cube"))	
			Debug.Log("boogers");

		GUILayout.FlexibleSpace();

		GUILayout.EndHorizontal();
	}
}
