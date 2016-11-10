using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RotationObject  {

	public string name;
	public GameObject goToRotateAround;
	public GameObject pointForAngleCalc;
	public Vector3 rotationAxis;
	public List<GameObject> dependantObjects = new List<GameObject>();

	public RotationObject() {
	}

}