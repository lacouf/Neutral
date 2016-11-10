using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour {

	GameObject belly;
	int speed = 2;

	// Use this for initialization
	void Start () {
		belly = GameObject.Find ("Belly");
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			transform.position += Vector3.back * speed;
		} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			transform.position += Vector3.forward * speed;
		} else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			transform.position += Vector3.right * speed;
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			transform.position += Vector3.left * speed;
		} else if (Input.GetKeyDown(KeyCode.A)) {
			transform.position += Vector3.up * speed;
		} else if (Input.GetKeyDown(KeyCode.Z)) {
			transform.position += Vector3.down * speed;
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

		transformWithLookAt (mouseX, mouseY, mouseZplus, mouseZminus);

	}

	void transformWithLookAt (float mouseX, float mouseY, float mouseZplus, float mouseZminus) {
		transform.LookAt (belly.transform.position);
		transform.Translate (Vector3.right * mouseX);
		transform.Translate (Vector3.up * mouseY);
		transform.Translate (Vector3.forward * mouseZplus);
		transform.Translate (Vector3.forward * -mouseZminus);
		transform.LookAt (belly.transform.position);
	}
}
