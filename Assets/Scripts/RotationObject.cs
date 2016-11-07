using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RotationObject  {

	public Vector3 rotationFrom;
	public Vector3 rotationAxisX;
	public List<GameObject> dependantObjects = new List<GameObject>();

	public RotationObject() {
	}

}