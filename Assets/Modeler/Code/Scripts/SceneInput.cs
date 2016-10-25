using UnityEngine;
using System.Collections;
using System.Linq;

namespace Modeler
{
	public class SceneInput : MonoBehaviourSingleton<SceneInput>
	{
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


		Vector3 SnapAxis(Vector3 dir)
		{
			float x = Mathf.Abs(dir.x), y = Mathf.Abs(dir.y), z = Mathf.Abs(dir.z);

			if(x > y && x > z)
				return dir.x < 0 ? -Vector3.right : Vector3.right;

			if(y > x && y > z)
				return dir.y < 0 ? -Vector3.up : Vector3.up;

			return dir.z < 0 ? -Vector3.forward : Vector3.forward;
		}

		void Update ()
		{
			if( CameraControls.instance.IsUsingMouse() || CameraControls.instance.IsUsingKey() )
				return;

			if( Input.GetMouseButtonDown(LEFT_MOUSE_BUTTON) )
			{
				if( Menu.instance.IsScreenPointOverGUI(Input.mousePosition) )
					return;

				if( editMode == EditMode.Object )
					Selection.PickGameObject(Input.mousePosition);
				else
					ElementPicker();
			}

			Transform camTrs = Camera.main.transform;

			if( Input.GetKey(KeyCode.W) )
				TranslateSelection( SnapAxis(camTrs.forward) * translationStep );
			else if( Input.GetKey(KeyCode.S) )
				TranslateSelection(  SnapAxis(-camTrs.forward) * translationStep );
			else if( Input.GetKey(KeyCode.D) )
				TranslateSelection( SnapAxis(camTrs.right) * translationStep );
			else if( Input.GetKey(KeyCode.A) )
				TranslateSelection(  SnapAxis(-camTrs.right) * translationStep );
			else if( Input.GetKey(KeyCode.E) )
				TranslateSelection( SnapAxis(camTrs.up) * translationStep );
			else if( Input.GetKey(KeyCode.Q) )
				TranslateSelection( SnapAxis(-camTrs.up) * translationStep );

			if( Input.GetKey(KeyCode.Backspace) || Input.GetKey(KeyCode.Delete) )
				SceneUtility.Delete(Selection.gameObjects);
		}

		void ElementPicker()
		{

		}

		void TranslateSelection(Vector3 direction)
		{
			foreach(GameObject go in Selection.gameObjects)
				go.transform.position += direction;
		}

	}
}
