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
	DisplayCanvas displayCanvas;
	UnityEngine.UI.Text rotationObjectName;
	UnityEngine.UI.Text angleX;
	UnityEngine.UI.Text angleY;
	UnityEngine.UI.Text angleZ;

	private LineRenderer lineRenderer;

	void Awake() {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
		} else if (control != this) {
			Destroy (gameObject);
		}

		if (displayCanvas == null) {
			displayCanvas = (DisplayCanvas) FindObjectOfType(typeof(DisplayCanvas));
			print ("Display Canvas " + displayCanvas);
			if (displayCanvas != null) {
				
				foreach (Transform child in displayCanvas.transform) {
					print ("Child name: " + child.gameObject.name);
					if (child.gameObject.name.Equals ("Panel")) {
						RectTransform panel = child.gameObject.GetComponent<RectTransform> ();
						foreach (Transform child2 in panel.transform) {
							print ("Child2 " + child2.gameObject.name);
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

		Vector3 bellyPos = GameObject.Find ("Belly").transform.position;
		Vector3 leftShoulderPos = GameObject.Find ("LeftShoulder").transform.position;
		Vector3 rightShoulderPos = GameObject.Find ("RightShoulder").transform.position;
		Vector3 leftElbowPos = GameObject.Find ("LeftElbow").transform.position;
		Vector3 rightElbowPos = GameObject.Find ("RightElbow").transform.position;
		Vector3 betweenShouldersPos = GameObject.Find ("BetweenShoulders").transform.position;
		//Vector3 betweenShouldersPos = Vector3.Lerp(leftShoulderPos, rightShoulderPos, 0.5F);
		Vector3 leftHipPos = GameObject.Find ("LeftHip").transform.position;
		Vector3 rightHipPos = GameObject.Find ("RightHip").transform.position;
		Vector3 betweenHipPos = Vector3.Lerp(leftHipPos, rightHipPos, 0.5F);

		RotationStruct.Add (CreateRotationObject("Head & Shoulders around belly", bellyPos, betweenShouldersPos, Vector3.right, "Head", "BetweenShoulders","LeftShoulder", "LeftElbow", "LeftHand", "RightShoulder", "RightElbow", "RightHand"));
		RotationStruct.Add (CreateRotationObject("Left shoulder around spine", betweenShouldersPos, leftShoulderPos, Vector3.forward, "LeftShoulder", "LeftElbow", "LeftHand"));
		RotationStruct.Add (CreateRotationObject("right shoulder around spine", betweenShouldersPos, rightShoulderPos, Vector3.forward, "RightShoulder", "RightElbow", "RightHand"));
		RotationStruct.Add (CreateRotationObject("Shoulders around Y axis", betweenShouldersPos, leftShoulderPos, Vector3.up, "Head", "LeftShoulder", "LeftElbow", "LeftHand", "RightShoulder", "RightElbow", "RightHand"));
		RotationStruct.Add (CreateRotationObject("Left Elbows around shoulders X axis", leftShoulderPos, leftElbowPos, Vector3.right, "LeftElbow", "LeftHand"));
		RotationStruct.Add (CreateRotationObject("Right Elbows around shoulders X axis", rightShoulderPos, rightElbowPos, Vector3.right, "RightElbow", "RightHand"));
		RotationStruct.Add (CreateRotationObject("Left Elbows around shoulders Y axis", leftShoulderPos, leftElbowPos, Vector3.up, "LeftElbow", "LeftHand"));
		RotationStruct.Add (CreateRotationObject("Right Elbows around shoulders Y axis", rightShoulderPos, rightElbowPos, Vector3.up, "RightElbow", "RightHand"));
		RotationStruct.Add (CreateRotationObject("Hips around spine", betweenHipPos, leftHipPos, Vector3.forward, "LeftHip", "LeftKnee", "LeftFoot", "RightHip", "RightKnee", "RightFoot"));
		RotationStruct.Add (CreateRotationObject("Hips around Y axis", betweenHipPos, leftHipPos, Vector3.up, "LeftHip", "LeftKnee", "LeftFoot", "RightHip", "RightKnee", "RightFoot"));
		RotationStruct.Add (CreateRotationObject("Hips and legs around belly", bellyPos, betweenHipPos, Vector3.right, "LeftHip", "LeftKnee", "LeftFoot", "RightHip", "RightKnee", "RightFoot"));
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

	RotationObject CreateRotationObject(string name, Vector3 rotationFrom, Vector3 point2, Vector3 axis, params string[] values) {
		RotationObject rotObject = new RotationObject ();
		rotObject.name = name;
		rotObject.rotationFrom = rotationFrom;
		rotObject.rotationPoint2 = point2;
		rotObject.rotationAxis = axis;
		foreach (String gameObjectName in values) {
			GameObject go = GameObject.Find (gameObjectName);
			if (go == null) {
				print ("Can't get gameObject with name + " + gameObjectName);
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
		Vector3 point1 = rot.rotationFrom + rot.rotationAxis * 3;
		Vector3 point2 = rot.rotationFrom + rot.rotationAxis * -3;
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
		Vector3 targetDir = rot.rotationFrom - rot.rotationPoint2;
		angleX.text = "AngleX " + Vector3.Angle(targetDir, Vector3.right);
		angleY.text = "AngleY " + Vector3.Angle(targetDir, Vector3.up);
		angleZ.text = "Anglez " + Vector3.Angle(targetDir, Vector3.forward);
	}

	void RotateObject(RotationObject ro, int direction) {
		
		foreach (GameObject go in ro.dependantObjects) {
			if (go != null) {
				//Vector3 rotationFrom = FindProjection (go.transform.position, ro.rotationFrom, ro.rotationAxis);
				Vector3 rotationFrom = go.transform.position;
				Quaternion actualRotation = go.transform.rotation;
				//float angleFrom = GetAngleOnAxis (actualRotation, ro.rotationAxis);
				//print ("Angle From: " + angleFrom);
				print ("Game Object: " + go.name + " From : " + ro.rotationFrom + " go pos " + go.transform.position + " axis " + ro.rotationAxis );
				go.transform.RotateAround (ro.rotationFrom, ro.rotationAxis, (1f * direction));
			}
		}
	}

	Vector3 FindProjection(Vector3 goPos, Vector3 rotationFrom, Vector3 axis) {
		if (axis == Vector3.right) {
			return new Vector3 (rotationFrom.x, goPos.y, goPos.z);
		} else if (axis == Vector3.up) {
			return new Vector3 (goPos.x, rotationFrom.y, goPos.z);
		} else {
			return new Vector3 (goPos.x, goPos.y, rotationFrom.z);
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