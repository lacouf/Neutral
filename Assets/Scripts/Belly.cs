using UnityEngine;
using System.Collections;

[RequireComponent (typeof(LineRenderer))]
public class Belly : MonoBehaviour {

	private LineRenderer lineRenderer;

	GameObject betweenShoulders;
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
		betweenShoulders = GameObject.Find("BetweenShoulders");
		leftShoulder = GameObject.Find("LeftShoulder");
		rightShoulder = GameObject.Find("RightShoulder");
		rightHip = GameObject.Find("RightHip");
		leftHip = GameObject.Find("LeftHip");
		head = GameObject.Find ("Head");

		belly = GameObject.Find("Belly");

		//calcAngles ();
	}
	
	// Update is called once per frame
	void Update () {
		createBellyLines ();
	}

	void createBellyLines() {

		CreateLineRenderers ();
		lineRenderer.SetVertexCount (6);

		int i = 0;

		lineRenderer.enabled = true;
		lineRenderer.SetWidth (0.5f, 0.5f);

		lineRenderer.SetPosition (i++, head.transform.position);
		lineRenderer.SetPosition (i++, betweenShoulders.transform.position);
		//lineRenderer.SetPosition (i++, Vector3.Lerp(leftShoulder.transform.position, rightShoulder.transform.position, 0.5F));

		//lineRenderer.SetPosition (i++, Vector3.Lerp(leftShoulder.transform.position, rightShoulder.transform.position, 0.5F));
		lineRenderer.SetPosition (i++, betweenShoulders.transform.position);
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
