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

		RotationStruct.Add (CreateRotationObject("Head & Shoulders around belly", GameObject.Find("Belly").transform.position, Vector3.right, "Head", "LeftShoulder", "LeftElbow", "LeftHand", "RightShoulder", "RightElbow", "RightHand"));
		Vector3 position = Vector3.Lerp(GameObject.Find ("LeftShoulder").transform.position, GameObject.Find ("RightShoulder").transform.position, 0.5F);
		RotationStruct.Add (CreateRotationObject("Shoulders around spine", position, Vector3.forward, "Head", "LeftShoulder", "LeftElbow", "LeftHand", "RightShoulder", "RightElbow", "RightHand"));
		RotationStruct.Add (CreateRotationObject("Shoulders around Y axis", position, Vector3.up, "Head", "LeftShoulder", "LeftElbow", "LeftHand", "RightShoulder", "RightElbow", "RightHand"));
		RotationStruct.Add (CreateRotationObject("Left Elbows around shoulders X axis", GameObject.Find ("LeftShoulder").transform.position, Vector3.right, "LeftElbow", "LeftHand"));
		RotationStruct.Add (CreateRotationObject("Right Elbows around shoulders X axis", GameObject.Find ("RightShoulder").transform.position, Vector3.right, "RightElbow", "RightHand"));
		RotationStruct.Add (CreateRotationObject("Left Elbows around shoulders Y axis", GameObject.Find ("LeftShoulder").transform.position, Vector3.up, "LeftElbow", "LeftHand"));
		RotationStruct.Add (CreateRotationObject("Right Elbows around shoulders Y axis", GameObject.Find ("RightShoulder").transform.position, Vector3.up, "RightElbow", "RightHand"));
		position = Vector3.Lerp(GameObject.Find ("LeftHip").transform.position, GameObject.Find ("RightHip").transform.position, 0.5F);
		RotationStruct.Add (CreateRotationObject("Hips around spine", position, Vector3.forward, "LeftHip", "LeftKnee", "LeftFoot", "RightHip", "RightKnee", "RightFoot"));
		RotationStruct.Add (CreateRotationObject("Hips around Y axis", position, Vector3.up, "LeftHip", "LeftKnee", "LeftFoot", "RightHip", "RightKnee", "RightFoot"));
		RotationStruct.Add (CreateRotationObject("Hips and legs around belly", GameObject.Find("Belly").transform.position, Vector3.right, "LeftHip", "LeftKnee", "LeftFoot", "RightHip", "RightKnee", "RightFoot"));
	}

	void Update() {
		bool needRedraw = false;
		
		if (Input.GetButton ("XBoxBt7") || Input.GetKeyDown(KeyCode.Q)) {
			print ("Letter Q");
			rotationObject = RotationStruct.Next ();
			needRedraw = true;
		} else	if (Input.GetButton("XBoxBt8") || Input.GetKeyDown(KeyCode.W)) {
			print ("Letter W");
			rotationObject = RotationStruct.Previous ();
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

	RotationObject CreateRotationObject(string name, Vector3 rotationFrom, Vector3 axis, params string[] values) {
		RotationObject rotObject = new RotationObject ();
		rotObject.name = name;
		rotObject.rotationFrom = rotationFrom;
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
		//print ("Drawing Axis " + rot.rotationAxisX + " for object at pos: " + rot.rotationFrom);

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