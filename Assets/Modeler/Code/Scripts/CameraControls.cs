using UnityEngine;
using System.Collections;

namespace Modeler
{
	/**
	 *	Simple FPS flythrough style camera controls.
	 *	
	 *	Note - This uses hardcoded keycodes and is generally not robust.
	 *	Would not recommend using for anything serious.
	 */
	public class CameraControls : MonoBehaviour
	{
		const int RIGHT_MOUSE_BUTTON = 1;

		public float lookSpeed = 15f;
		public float moveSpeed = 15f;

		float rotationX = 45.0f;
		float rotationY = -35.0f;

		private Vector2 previousMousePosition = Vector2.zero;
		private bool mouseDown = false;

		void Start()
		{
			previousMousePosition = Input.mousePosition;
		}

		void FixedUpdate()
		{
			if(!Input.GetMouseButton(RIGHT_MOUSE_BUTTON))
			{
				mouseDown = false;
				return;
			}

			if(!mouseDown)
			{
				mouseDown = true;
				previousMousePosition = Input.mousePosition;
			}

			Vector2 mouseDelta = ((Vector2)Input.mousePosition) - previousMousePosition;
			previousMousePosition = Input.mousePosition;

			Vector2 screenSize = new Vector2(Screen.width, Screen.height);

			rotationX += (mouseDelta.x / screenSize.x) * lookSpeed;
			rotationY += (mouseDelta.y / screenSize.y) * lookSpeed;

			rotationY = Mathf.Clamp (rotationY, -90f, 90f);

			transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up) * Quaternion.AngleAxis(rotationY, Vector3.left);

			transform.position += Input.GetAxis("Vertical") * transform.forward * moveSpeed * Time.deltaTime;
			transform.position += Input.GetAxis("WorldVertical") * transform.up * moveSpeed * Time.deltaTime;
			transform.position += Input.GetAxis("Horizontal") * transform.right * moveSpeed * Time.deltaTime;
		}
	}
}
