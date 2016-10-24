using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour
{
	public float lookSpeed = 15f;
	public float moveSpeed = 15f;

	float rotationX = 0.0f;
	float rotationY = 0.0f;

	void FixedUpdate()
	{
		rotationX += Input.GetAxis("Mouse X") * lookSpeed;
		rotationY += Input.GetAxis("Mouse Y") * lookSpeed;

		rotationY = Mathf.Clamp (rotationY, -90f, 90f);

		transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up) * Quaternion.AngleAxis(rotationY, Vector3.left);

		transform.position += Input.GetAxis("Vertical") * transform.forward * moveSpeed * Time.deltaTime;
		transform.position += Input.GetAxis("Horizontal") * transform.right * moveSpeed * Time.deltaTime;
	}
}
