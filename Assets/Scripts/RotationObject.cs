using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RotationObject  {

	public string name;
	public GameObject goToRotateAround; // GameObject to rotate around
	public GameObject pointForAngleCalc;// GameObject that rotates 
	public bool sub90;					// Substract 90;
	public Vector3 rotationAxis;		// Axis on which to rotate
	public Vector3 axisForAngleCalc;	// Axis for angle calc
	public Vector3 signAxis;			// Axis that determines sign of angle
	public List<GameObject> dependantObjects = new List<GameObject>(); // List of object that need to be rotated.

	public RotationObject() {
	}

}