using UnityEngine;
using System.Collections;

public class LeftShoulder : MonoBehaviour {

	LineRenderer lineRenderer;

	GameObject leftShoulder;
	GameObject leftElbow;
	GameObject leftHand;
	GameObject rightShoulder;
	GameObject rightElbow;
	GameObject rightHand;

	// Use this for initialization
	void Start () {
		//Get references to other objects
		leftShoulder = GameObject.Find("LeftShoulder");
		leftElbow = GameObject.Find("LeftElbow");
		leftHand = GameObject.Find("LeftHand");
		rightShoulder = GameObject.Find("RightShoulder");
		rightElbow = GameObject.Find("RightElbow");
		rightHand = GameObject.Find("RightHand");

		createShoulderLineRenderer ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void createShoulderLineRenderer() {

		CreateLineRenderers ();
		//lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.SetVertexCount (10);

		int i = 0;

		lineRenderer.enabled = true;
		lineRenderer.SetWidth (0.5f, 0.5f);

		lineRenderer.SetPosition (i++, rightHand.transform.position);
		lineRenderer.SetPosition (i++, rightElbow.transform.position);

		lineRenderer.SetPosition (i++, rightElbow.transform.position);
		lineRenderer.SetPosition (i++, rightShoulder.transform.position);

		lineRenderer.SetPosition (i++, rightShoulder.transform.position); 
		lineRenderer.SetPosition (i++, leftShoulder.transform.position);

		lineRenderer.SetPosition (i++, leftShoulder.transform.position); 
		lineRenderer.SetPosition (i++, leftElbow.transform.position);

		lineRenderer.SetPosition (i++, leftElbow.transform.position);
		lineRenderer.SetPosition (i++, leftHand.transform.position);

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
