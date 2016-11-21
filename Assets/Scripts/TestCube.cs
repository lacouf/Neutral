using UnityEngine;
using System.Collections;

[RequireComponent (typeof(LineRenderer))]
public class TestCube : MonoBehaviour {

	private LineRenderer lineRenderer;

	GameObject cube1;
	GameObject plane;
	float rotationSpeed = 10.0f;

	// Use this for initialization
	void Awake () {
		cube1 = GameObject.Find ("Cube1");
		//plane = GameObject.Find ("Plane");
		CreateLineRenderers ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			Vector3 rotAxis = Vector3.up;

			transform.RotateAround (cube1.transform.position, cube1.transform.rotation * rotAxis, rotationSpeed * 1f);
			DrawAxis (cube1.transform, rotAxis);

			Vector3 targetDir = cube1.transform.position - transform.position;
			Vector3 inverse = cube1.transform.InverseTransformDirection (targetDir);
			print ("targetDir " + targetDir + " Inverse " + inverse);

			targetDir = cube1.transform.position - transform.position;
			targetDir = cube1.transform.InverseTransformDirection (targetDir);
			targetDir.y = 0;
			//print ("angle from cube1: Rotation Axis " + rotAxis + " Axis y = 0, angle from = right) : " + Vector3.Angle (Vector3.right, targetDir));

			targetDir = cube1.transform.position - transform.position;
			targetDir = cube1.transform.InverseTransformDirection (targetDir);
			targetDir.y = 0;
			print ("pos1 " + cube1.transform.position + " pos2 " + transform.position + " targetDir1 " + (cube1.transform.position - transform.position));
			print ("angle from cube1: Axis around " + rotAxis + " Axis y = 0, angle from = forward) : " + Vector3.Angle (Vector3.forward, targetDir));

			targetDir = cube1.transform.position - transform.position;
			targetDir = cube1.transform.InverseTransformDirection (targetDir);
			targetDir.x = 0;
			//print ("angle from cube1: Axis around " + rotAxis + " Axis x = 0, angle from = up) : " + Vector3.Angle (Vector3.up, targetDir));

			targetDir = cube1.transform.position - transform.position;
			targetDir = cube1.transform.InverseTransformDirection (targetDir);
			targetDir.x = 0; // Sign of targetDir.y determines side
			//print ("angle from cube1: Axis around " + rotAxis + " Axis x = 0, angle from = forward) : " + Vector3.Angle (Vector3.forward, targetDir));

			targetDir = cube1.transform.position - transform.position;
			targetDir = cube1.transform.InverseTransformDirection (targetDir);
			targetDir.z = 0;
			//print ("angle from cube1: Axis around " + rotAxis + " Axis z = 0, angle from = up) : " + Vector3.Angle (Vector3.up, targetDir));

			targetDir = cube1.transform.position - transform.position;
			targetDir = cube1.transform.InverseTransformDirection (targetDir);
			targetDir.z = 0;
			//print ("angle from cube1: Axis around " + rotAxis + " Axis z = 0, angle from = right) : " + Vector3.Angle (Vector3.right, targetDir));
		}
	
	}

	void CreateLineRenderers () {
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

	void DrawAxis (Transform transform, Vector3 rotAxis) {
		lineRenderer.SetVertexCount (2);
		lineRenderer.enabled = true;
		lineRenderer.material = new Material (Shader.Find ("Particles/Additive"));
		lineRenderer.SetColors (Color.green, Color.green);
		lineRenderer.SetWidth (0.2f, 0.2f);
		lineRenderer.useWorldSpace = true;
		Vector3 point1 = transform.position + (transform.rotation * rotAxis * 3);
		//Vector3 point1 = axis + Vector3.right * 3;
		Vector3 point2 = transform.position + (transform.rotation * rotAxis * -3);
		//Vector3 point2 = axis + Vector3.right * -3;
		int i = 0;
		lineRenderer.SetPosition (i++, point1);
		lineRenderer.SetPosition (i++, point2);
	}
}
