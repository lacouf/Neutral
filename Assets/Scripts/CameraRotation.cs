using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour {

	GameObject belly;

	// Use this for initialization
	void Start () {
		belly = GameObject.Find ("Belly");
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
			
		float rotationSpeed = 1.0f;
		float mouseX = Input.GetAxis ("XBoxRStickX") * rotationSpeed;
		float mouseY = -Input.GetAxis ("XBoxRStickY") * rotationSpeed;
		float mouseZplus = Input.GetAxis ("XBoxRTrigger");
		float mouseZminus = Input.GetAxis ("XBoxLTrigger");
		if (mouseZplus < 0F) {
			mouseZplus = mouseZplus + 1;
		}
		if (mouseZminus < 0F) {
			mouseZminus = mouseZminus + 1;
		}
		//print ("Right Stick (x,y): " + mouseX + ", " + mouseY);
		//transform.localRotation = Quaternion.Euler (0, mouseX, 0) * transform.localRotation;
		transform.Translate(Vector3.right * mouseX);
		transform.Translate(Vector3.up * mouseY);
		transform.Translate (Vector3.forward * mouseZplus);
		transform.Translate (Vector3.forward * -mouseZminus);
		transform.LookAt (belly.transform.position);

		//Camera camera = GetComponentInChildren<Camera> ();
		//camera.transform.localRotation = Quaternion.Euler (mouseY, 0, 0) * camera.transform.localRotation;
		//camera.transform.LookAt (belly.transform.position);
	}
}
