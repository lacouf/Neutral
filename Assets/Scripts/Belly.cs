using UnityEngine;
using System.Collections;

[RequireComponent (typeof(LineRenderer))]
public class Belly : MonoBehaviour {

	private LineRenderer lineRenderer;

	GameObject leftShoulder;
	GameObject rightShoulder;
	GameObject rightHip;
	GameObject leftHip;
	GameObject belly;
	GameObject head;

	// Use this for initialization
	void Start () {
		if (lineRenderer == null) {
			lineRenderer = GetComponent<LineRenderer>();
		}
		//Get references to other objects
		leftShoulder = GameObject.Find("LeftShoulder");
		rightShoulder = GameObject.Find("RightShoulder");
		rightHip = GameObject.Find("RightHip");
		leftHip = GameObject.Find("LeftHip");
		head = GameObject.Find ("Head");

		belly = GameObject.Find("Belly");

		//calcAngles ();
		createBellyLines ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void calcAngles() {
		Vector3 targetDir = leftShoulder.transform.position - rightShoulder.transform.position;

		float angle1 = Vector3.Angle( targetDir, transform.right );
		float angle2 = Vector3.Angle( targetDir, transform.up );
		float angle3 = Vector3.Angle( targetDir, transform.forward );
		print ("leftShoulder to rightShoulder : (r, u, f) : (" + angle1 + ", " + angle2 + ", " + angle3 + ")");	

		targetDir = rightShoulder.transform.position - leftShoulder.transform.position;
		angle1 = Vector3.Angle( targetDir, transform.right );
		angle2 = Vector3.Angle( targetDir, transform.up );
		angle3 = Vector3.Angle( targetDir, transform.forward );
		//print ("rightShoulder to leftShoulder : (r, u, f) : (" + angle1 + ", " + angle2 + ", " + angle3 + ")");	
	}

	void createBellyLines() {

		CreateLineRenderers ();
		lineRenderer.SetVertexCount (6);

		int i = 0;

		lineRenderer.enabled = true;

		lineRenderer.SetPosition (i++, head.transform.position);
		lineRenderer.SetPosition (i++, Vector3.Lerp(leftShoulder.transform.position, rightShoulder.transform.position, 0.5F));

		lineRenderer.SetPosition (i++, Vector3.Lerp(leftShoulder.transform.position, rightShoulder.transform.position, 0.5F));
		lineRenderer.SetPosition (i++, belly.transform.position);

		lineRenderer.SetPosition (i++, belly.transform.position); 
		lineRenderer.SetPosition (i++, Vector3.Lerp(leftHip.transform.position, rightHip.transform.position, 0.5F));

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
