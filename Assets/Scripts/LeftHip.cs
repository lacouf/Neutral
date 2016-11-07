using UnityEngine;
using System.Collections;

public class LeftHip : MonoBehaviour {

	LineRenderer lineRenderer;

	GameObject leftHip;
	GameObject leftKnee;
	GameObject leftFoot;
	GameObject rightHip;
	GameObject rightKnee;
	GameObject rightFoot;

	// Use this for initialization
	void Start () {
		//Get references to other objects
		leftHip = GameObject.Find("LeftHip");
		leftKnee = GameObject.Find("LeftKnee");
		leftFoot = GameObject.Find("LeftFoot");
		rightHip = GameObject.Find("RightHip");
		rightKnee = GameObject.Find("RightKnee");
		rightFoot = GameObject.Find("RightFoot");

	}
	
	// Update is called once per frame
	void Update () {
		createHipLineRenderer ();
	}

	void createHipLineRenderer() {
		CreateLineRenderers ();
		//lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.SetVertexCount (10);

		int i = 0;

		lineRenderer.enabled = true;

		lineRenderer.SetPosition (i++, rightFoot.transform.position);
		lineRenderer.SetPosition (i++, rightKnee.transform.position);

		lineRenderer.SetPosition (i++, rightKnee.transform.position);
		lineRenderer.SetPosition (i++, rightHip.transform.position);

		lineRenderer.SetPosition (i++, rightHip.transform.position); 
		lineRenderer.SetPosition (i++, leftHip.transform.position);

		lineRenderer.SetPosition (i++, leftHip.transform.position); 
		lineRenderer.SetPosition (i++, leftKnee.transform.position);

		lineRenderer.SetPosition (i++, leftKnee.transform.position);
		lineRenderer.SetPosition (i++, leftFoot.transform.position);
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
