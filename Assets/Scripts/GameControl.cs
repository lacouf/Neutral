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

	private LineRenderer lineRenderer;

	void Awake() {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
		} else if (control != this) {
			Destroy (gameObject);
		}

		RotationStruct.Add (CreateRotationObject(GameObject.Find("Belly").transform.position, Vector3.right, "Head", "LeftShould", "LeftElbow", "LeftHand", "RightShoulder", "RightElbow", "RightHand"));
		Vector3 position = Vector3.Lerp(GameObject.Find ("LeftShoulder").transform.position, GameObject.Find ("RightShoulder").transform.position, 0.5F);
		RotationStruct.Add (CreateRotationObject(position, Vector3.forward, "Head", "LeftShould", "LeftElbow", "LeftHand", "RightShoulder", "RightElbow", "RightHand"));
		RotationStruct.Add (CreateRotationObject(position, Vector3.up, "Head", "LeftShould", "LeftElbow", "LeftHand", "RightShoulder", "RightElbow", "RightHand"));

	}

	void Update() {
		
		if (Input.GetButton ("XBoxBt7") || Input.GetKeyDown(KeyCode.A)) {
			rotationObject = RotationStruct.Next ();
		} else	if (Input.GetButton("XBoxBt8") || Input.GetKeyDown(KeyCode.B)) {
			rotationObject = RotationStruct.Previous ();
		}
		if (rotationObject != null) {
			UnColorObjects ();
			DrawAxisAndColorObjects (rotationObject);
		}
	}

	public void Save() {
		BinaryFormatter bf = new BinaryFormatter ();
		print ("Path: " + Application.persistentDataPath);
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

	RotationObject CreateRotationObject(Vector3 rotationFrom, Vector3 axis, params string[] values) {
		RotationObject rotObject = new RotationObject ();
		rotObject.rotationFrom = rotationFrom;
		rotObject.rotationAxisX = axis;
		foreach (String gameObject in values) {
			rotObject.dependantObjects.Add (GameObject.Find (gameObject));
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
		int i = 0;
		lineRenderer.enabled = true;
		lineRenderer.material = new Material (Shader.Find ("Particles/Additive"));
		lineRenderer.SetColors (Color.green, Color.green);
		lineRenderer.SetWidth (0.2f, 0.2f);
		lineRenderer.useWorldSpace = false;
		Vector3 point1 = rot.rotationFrom + Vector3.right * 3;
		Vector3 point2 = rot.rotationFrom + Vector3.right * 3;
		lineRenderer.SetPosition (i++, point1);
		lineRenderer.SetPosition (i++, point2);
	}

	void ColorObjectsRed(RotationObject rot) {
		foreach (GameObject go in rot.dependantObjects) {
			Material mat = go.GetComponent<Renderer>().material;
			mat.color = Color.red;
		}
	}

	void UnColorObjects() {
		if (coloredObjects.Count == 0) return;
		foreach (GameObject go in coloredObjects) {
			Material mat = go.GetComponent<Renderer>().material;
			mat.color = Color.white;
		}
		coloredObjects.Clear ();
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