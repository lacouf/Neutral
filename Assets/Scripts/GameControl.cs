using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[RequireComponent (typeof(LineRenderer))]
public class GameControl : MonoBehaviour {

	public static GameControl control;
	public PlayerData playerData;

	RotationObject rotationObject = null;
	List<GameObject> coloredObjects = new List<GameObject> ();

	// UI
	DisplayCanvas displayCanvas;
	UnityEngine.UI.Text rotationObjectName;
	UnityEngine.UI.Text angleX;
	UnityEngine.UI.Text angleY;
	UnityEngine.UI.Text angleZ;

	// GameObjects
	GameObject belly;
	GameObject head;
	GameObject betweenShoulders;
	GameObject leftShoulder;
	GameObject leftElbow;
	GameObject leftHand;
	GameObject rightShoulder;
	GameObject rightElbow;
	GameObject rightHand;
	GameObject betweenHips;
	GameObject leftHip;
	GameObject leftKnee;
	GameObject leftFoot;
	GameObject rightHip;
	GameObject rightKnee;
	GameObject rightFoot;

	private LineRenderer lineRenderer;

	void Awake() {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
		} else if (control != this) {
			Destroy (gameObject);
		}

		FindGameObjects ();

		if (displayCanvas == null) {
			displayCanvas = (DisplayCanvas) FindObjectOfType(typeof(DisplayCanvas));
			//print ("Display Canvas " + displayCanvas);
			if (displayCanvas != null) {
				
				foreach (Transform child in displayCanvas.transform) {
					//print ("Child name: " + child.gameObject.name);
					if (child.gameObject.name.Equals ("Panel")) {
						RectTransform panel = child.gameObject.GetComponent<RectTransform> ();
						foreach (Transform child2 in panel.transform) {
							//print ("Child2 " + child2.gameObject.name);
							if (child2.name.Equals("RotationObjectText")) {
								rotationObjectName = child2.gameObject.GetComponent<UnityEngine.UI.Text> ();
								rotationObjectName.text = "Rotation Object";
							} else if (child2.name.Equals("AngleX")) {
								angleX = child2.gameObject.GetComponent<UnityEngine.UI.Text> ();
								angleX.text = "AngleX";
							} else if (child2.name.Equals("AngleY")) {
								angleY = child2.gameObject.GetComponent<UnityEngine.UI.Text> ();
								angleY.text = "AngleY";
							} else if (child2.name.Equals("AngleZ")) {
								angleZ = child2.gameObject.GetComponent<UnityEngine.UI.Text> ();
								angleZ.text = "AngleZ";
							}
						}
					}
//					if (child.gameObject.name.Equals (SHOTSTEXT)) {
//						shotsText = child.gameObject.GetComponent<UnityEngine.UI.Text> ();
//						shotsText.text = "2000";
//					}
				}
			}
		}

		RotationStruct.Add (CreateRotationObject("Head & Shoulders around belly", belly, betweenShoulders, Vector3.right, head, betweenShoulders,leftShoulder, leftElbow, leftHand, rightShoulder, rightElbow, rightHand));
		RotationStruct.Add (CreateRotationObject("Left shoulder around spine", betweenShoulders, leftShoulder, Vector3.forward, leftShoulder, leftElbow, leftHand));
		RotationStruct.Add (CreateRotationObject("right shoulder around spine", betweenShoulders, rightShoulder, Vector3.forward, rightShoulder, rightElbow, rightHand));
		RotationStruct.Add (CreateRotationObject("Shoulders around Y axis", betweenShoulders, leftShoulder, Vector3.up, head, leftShoulder, leftElbow, leftHand, rightShoulder, rightElbow, rightHand));
		RotationStruct.Add (CreateRotationObject("Left Elbows around shoulders X axis", leftShoulder, leftElbow, Vector3.right, leftElbow, leftHand));
		RotationStruct.Add (CreateRotationObject("Right Elbows around shoulders X axis", rightShoulder, rightElbow, Vector3.right, rightElbow, rightHand));
		RotationStruct.Add (CreateRotationObject("Left Elbows around shoulders Y axis", leftShoulder, leftElbow, Vector3.up, leftElbow, leftHand));
		RotationStruct.Add (CreateRotationObject("Right Elbows around shoulders Y axis", rightShoulder, rightElbow, Vector3.up, rightElbow, rightHand));
		RotationStruct.Add (CreateRotationObject("Hips around spine", betweenHips, leftHip, Vector3.forward, leftHip, leftKnee, leftFoot, rightHip, rightKnee, rightFoot));
		RotationStruct.Add (CreateRotationObject("Hips around Y axis", betweenHips, leftHip, Vector3.up, leftHip, leftKnee, leftFoot, rightHip, rightKnee, rightFoot));
		RotationStruct.Add (CreateRotationObject("Hips and legs around belly", belly, betweenHips, Vector3.right, leftHip, leftKnee, leftFoot, rightHip, rightKnee, rightFoot));
	}

	void Update() {
		bool needRedraw = false;
		
		if (Input.GetButton ("XBoxBt7") || Input.GetKeyDown(KeyCode.Q)) {
			rotationObject = RotationStruct.Next ();
			needRedraw = true;
		} else	if (Input.GetButton("XBoxBt8") || Input.GetKeyDown(KeyCode.W)) {
			rotationObject = RotationStruct.Previous ();
			needRedraw = true;
		}


		if (Input.GetKeyDown (KeyCode.Equals)) {
			RotateObject (rotationObject, 1);
			needRedraw = true;
		} else if (Input.GetKeyDown (KeyCode.Minus)) {
			RotateObject (rotationObject, -1);
			needRedraw = true;
		}

		if (needRedraw && rotationObject != null) {
			needRedraw = false;
			UnColorObjects ();
			DrawAxisAndColorObjects (rotationObject);
			DrawUI (rotationObject);
		}
	}

	void FindGameObjects() {
		belly = GameObject.Find (Constants.BELLY);
		head = GameObject.Find (Constants.HEAD);
		betweenShoulders = GameObject.Find (Constants.BETWEENSHOULDERS);
		leftShoulder = GameObject.Find (Constants.LEFTSHOULDER);
		leftElbow = GameObject.Find (Constants.LEFTELBOW);
		leftHand = GameObject.Find (Constants.LEFTHAND);
		rightShoulder = GameObject.Find (Constants.RIGHTSHOULDER);
		rightElbow = GameObject.Find (Constants.RIGHTELBOW);
		rightHand = GameObject.Find (Constants.RIGHTHAND);
		betweenHips = GameObject.Find (Constants.BETWEENHIPS);
		leftHip = GameObject.Find (Constants.LEFTHIP);
		leftKnee = GameObject.Find (Constants.LEFTKNEE);
		leftFoot = GameObject.Find (Constants.LEFTFOOT);
		rightHip = GameObject.Find (Constants.RIGHTHIP);
		rightKnee = GameObject.Find (Constants.RIGHTKNEE);
		rightFoot = GameObject.Find (Constants.RIGHTFOOT);

		if (belly == null)
			print ("belly null");
		if (head == null)
			print ("head null");
		if (betweenShoulders == null)
			print ("betweenShoulders null");
		if (leftShoulder == null)
			print ("leftShoulder null");
		if (leftElbow == null)
			print ("leftElbow null");
		if (leftHand == null)
			print ("leftHand null");
		if (rightShoulder == null)
			print ("rightShoulder null");
		if (rightElbow == null)
			print ("rightElbow null");
		if (rightHand == null)
			print ("rightHand null");
		if (betweenHips == null)
			print ("betweenHips null");
		if (leftHip == null)
			print ("leftHip null");
		if (leftKnee == null)
			print ("leftKnee null");
		if (leftFoot == null)
			print ("leftFoot null");
		if (rightHip == null)
			print ("rightHip null");
		if (rightKnee == null)
			print ("rightKnee null");
		if (rightFoot == null)
			print ("rightFoot null");
	}

	public void Save() {
		BinaryFormatter bf = new BinaryFormatter ();
		//print ("Path: " + Application.persistentDataPath);
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
		PlayerData data = new PlayerData ();
		data.someData = 1;
		data.someOtherData = 2;
		bf.Serialize (file, data);
		file.Close();
	}

	public void Load() {
		if (File.Exists(Application.persistentDataPath + "playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			playerData = (PlayerData) bf.Deserialize(file);
			file.Close ();
		}

	}

	RotationObject CreateRotationObject(string name, GameObject goToRotateAround, GameObject pointForAngleCalc, Vector3 axis, params GameObject[] values) {
		RotationObject rotObject = new RotationObject ();
		rotObject.name = name;
		rotObject.goToRotateAround = goToRotateAround;
		rotObject.pointForAngleCalc = pointForAngleCalc;
		rotObject.rotationAxis = axis;
		foreach (GameObject go in values) {
			if (go == null) {
				print ("Can't get gameObject");
			}
			rotObject.dependantObjects.Add (go);
		}
		return rotObject;
	}

	void DrawAxisAndColorObjects(RotationObject rot) {
		CreateLineRenderers ();
		DrawRotation (rot);
		ColorObjectsRed (rot);
	}

	void DrawRotation (RotationObject rot) {
		lineRenderer.SetVertexCount (2);
		lineRenderer.enabled = true;
		lineRenderer.material = new Material (Shader.Find ("Particles/Additive"));
		lineRenderer.SetColors (Color.green, Color.green);
		lineRenderer.SetWidth (0.2f, 0.2f);
		lineRenderer.useWorldSpace = true;
		Vector3 point1 = rot.goToRotateAround.transform.position + rot.rotationAxis * 3;
		Vector3 point2 = rot.goToRotateAround.transform.position + rot.rotationAxis * -3;
		int i = 0;
		lineRenderer.SetPosition (i++, point1);
		lineRenderer.SetPosition (i++, point2);
	}

	void ColorObjectsRed(RotationObject rot) {
		foreach (GameObject go in rot.dependantObjects) {
			if (go != null) {
				Renderer renderer = go.GetComponent<Renderer> ();
				renderer.material.color = Color.red;
				coloredObjects.Add (go);
			}
		}
	}

	void UnColorObjects() {
		if (coloredObjects.Count == 0) return;
		foreach (GameObject go in coloredObjects) {
			Renderer renderer = go.GetComponent<Renderer> ();
			renderer.material.color = Color.white;
		}
		coloredObjects.Clear ();
	}

	void DrawUI(RotationObject rot) {
		rotationObjectName.text = "" + rot.name;
		Vector3 targetDir = rot.goToRotateAround.transform.position - rot.pointForAngleCalc.transform.position;
		angleX.text = "AngleX " + Vector3.Angle(targetDir, Vector3.right);
		angleY.text = "AngleY " + Vector3.Angle(targetDir, Vector3.up);
		angleZ.text = "Anglez " + Vector3.Angle(targetDir, Vector3.forward);
	}

	void RotateObject(RotationObject ro, int direction) {
		
		foreach (GameObject go in ro.dependantObjects) {
			if (go != null) {
				Vector3 rotationFrom = go.transform.position;
				Quaternion actualRotation = go.transform.rotation;
				print ("Game Object: " + go.name + " AroundObject : " + ro.goToRotateAround.name + " go pos " + go.transform.position + " axis " + ro.rotationAxis );
				go.transform.RotateAround (ro.goToRotateAround.transform.position, ro.rotationAxis, (1f * direction));
			}
		}
	}

	float GetAngleOnAxis (Quaternion fromAngle, Vector3 axis) {
		if (axis == Vector3.right) {
			return fromAngle.eulerAngles.x;
		} else if (axis == Vector3.up) {
			return fromAngle.eulerAngles.y;
		} else {
			return fromAngle.eulerAngles.z;
		}
	}

	void CreateLineRenderers() {
		if (lineRenderer == null) {
			lineRenderer = GetComponent<LineRenderer> ();
			if (lineRenderer == null) {
				lineRenderer = gameObject.AddComponent<LineRenderer> ();
			}
		}
		if (lineRenderer == null) {
			lineRenderer = GetComponent<LineRenderer> ();
		}

	}

}