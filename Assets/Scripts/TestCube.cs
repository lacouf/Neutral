using UnityEngine;
using System.Collections;

[RequireComponent (typeof(LineRenderer))]
public class TestCube : MonoBehaviour {

	private LineRenderer lineRenderer;

	GameObject cube1;
	float rotationSpeed = 12.0f;

	// Use this for initialization
	void Start () {
		cube1 = GameObject.Find("Cube1");
		CreateLineRenderers ();
	}
	
	// Update is called once per frame
	void Update () { 

		transform.RotateAround (cube1.transform.position, Vector3.right, rotationSpeed * Time.deltaTime);
		DrawAxis (cube1.transform.position);
		print ("transform position: " + cube1.transform.position);
	
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

	void DrawAxis(Vector3 axis) {
		lineRenderer.SetVertexCount (2);
		lineRenderer.enabled = true;
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.SetColors (Color.green, Color.green);
		lineRenderer.SetWidth(0.2f, 0.2f);
		lineRenderer.useWorldSpace = true;
		Vector3 point1 = axis + Vector3.right * 3;
		Vector3 point2 = axis + Vector3.right * -3;
		int i = 0;
		lineRenderer.SetPosition (i++, point1);
		lineRenderer.SetPosition (i++, point2);
	}
}
