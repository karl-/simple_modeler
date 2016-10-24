using UnityEngine;
using System.Collections;

namespace Modeler
{
	public class SceneInput : MonoBehaviour
	{
		private static SceneInput _instance;
		public static SceneInput instance { get { return _instance; } }

		const int LEFT_MOUSE_BUTTON = 0;
		const int RIGHT_MOUSE_BUTTON = 1;

		public float translationStep = .1f;

		private EditMode _editMode = EditMode.Object;

		public EditMode editMode
		{
			get
			{	
				return _editMode;
			}

			set
			{
				_editMode = value;
			}
		}

		void Awake()
		{
			_instance = this;
		}

		void Update ()
		{
			if( Input.GetMouseButton(RIGHT_MOUSE_BUTTON) )
				return;

			if( Input.GetMouseButtonDown(LEFT_MOUSE_BUTTON) )
			{
				Selection.PickGameObject(Input.mousePosition);
			}

			if( Input.GetKey(KeyCode.W) )
				TranslateSelection( Vector3.forward * translationStep );
			else if( Input.GetKey(KeyCode.S) )
				TranslateSelection( -Vector3.forward * translationStep );
			else if( Input.GetKey(KeyCode.D) )
				TranslateSelection( Vector3.right * translationStep );
			else if( Input.GetKey(KeyCode.A) )
				TranslateSelection( -Vector3.right * translationStep );
			else if( Input.GetKey(KeyCode.E) )
				TranslateSelection( Vector3.up * translationStep );
			else if( Input.GetKey(KeyCode.Q) )
				TranslateSelection( -Vector3.up * translationStep );
		}

		void TranslateSelection(Vector3 direction)
		{
			foreach(GameObject go in Selection.gameObjects)
				go.transform.position += direction;
		}

	}
}
