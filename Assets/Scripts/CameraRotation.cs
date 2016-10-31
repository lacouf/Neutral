using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			transform.position += Vector3.back;
		} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			transform.position += Vector3.forward;
		} else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			transform.position += Vector3.right;
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			transform.position += Vector3.left;
		} else if (Input.GetKeyDown(KeyCode.A)) {
			transform.position += Vector3.up;
		} else if (Input.GetKeyDown(KeyCode.Z)) {
			transform.position += Vector3.down;
		} 

		float rotationSpeed = 5.0f;
		float mouseX = Input.GetAxis ("Mouse X") * rotationSpeed;
		float mouseY = -Input.GetAxis ("Mouse Y") * rotationSpeed;
		transform.localRotation = Quaternion.Euler (0, mouseX, 0) * transform.localRotation;
		Camera camera = GetComponentInChildren<Camera> ();
		camera.transform.localRotation = Quaternion.Euler (mouseY, 0, 0) * camera.transform.localRotation;
	}
}
