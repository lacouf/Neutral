using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	GameObject gameControlObject;
	GameControl gameControl;

	// Use this for initialization
	void Start () {
		gameControlObject = GameObject.Find ("GameControl");
		gameControl = gameControlObject.GetComponent<GameControl> ();
		gameControl.Save ();
	}
	
	// Update is called once per frame
	void Update () {
		//float xaxis = Input.GetAxis ("Horizontal");
		//float yaxis = Input.GetAxis ("Vertical");
		//print ("Axis (x,y): " + xaxis + ", " + yaxis);

		//float xaxis2 = Input.GetAxis ("XBoxRStickX");
		//float yaxis2 = Input.GetAxis ("XBoxRStickY");
		//print ("Right Stick (x,y): " + xaxis2 + ", " + yaxis2);

		float xaxis3 = Input.GetAxis ("XBoxLTrigger");
		float yaxis3 = Input.GetAxis ("XBoxRTrigger");
		if (xaxis3 < 0F) {
			xaxis3 = xaxis3 + 1;
		}
		if (yaxis3 < 0F) {
			yaxis3 = yaxis3 + 1;
		}
		//print ("Left, Right Trigger (x,y): " + xaxis3 + ", " + yaxis3);
//		print ("button " + (Input.GetButton("XBoxBt1") ? "Bt1" : ".") + ", "
//			+ (Input.GetButton("XBoxBt2") ? "Bt2" : ".") + ", "
//			+ (Input.GetButton("XBoxBt3") ? "Bt3" : ".") + ", "
//			+ (Input.GetButton("XBoxBt4") ? "Bt4" : ".") + ", "
//			+ (Input.GetButton("XBoxBt5") ? "Bt5" : ".") + ", "
//			+ (Input.GetButton("XBoxBt6") ? "Bt6" : ".") + ", "
//			+ (Input.GetButton("XBoxBt7") ? "Bt7" : ".") + ", "
//			+ (Input.GetButton("XBoxBt8") ? "Bt8" : ".") + ", "
//			+ (Input.GetButton("XBoxBt9") ? "Bt9" : ".") + ", "
//			+ (Input.GetButton("XBoxBt10") ? "Bt10" : ".") + ", "
//			+ (Input.GetButton("XBoxBt11") ? "Bt11" : ".") + ", "
//			+ (Input.GetButton("XBoxBt12") ? "Bt12" : ".") + ", "
//			+ (Input.GetButton("XBoxBt13") ? "Bt13" : ".") + ", "
//			+ (Input.GetButton("XBoxBt14") ? "Bt14" : ".") + ", "
//			+ (Input.GetButton("XBoxBt15") ? "Bt15" : ".") + ", "
//			+ (Input.GetButton("XBoxBt16") ? "Bt16" : ".") + ", "
//			+ (Input.GetButton("XBoxBt17") ? "Bt17" : ".") + ", "
//			+ (Input.GetButton("XBoxBt18") ? "Bt18" : ".") + ", "
//			+ (Input.GetButton("XBoxBt19") ? "Bt19" : ".") + ", "
//
//		);
	}
}
