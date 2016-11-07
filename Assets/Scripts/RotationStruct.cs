using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


public class RotationStruct : MonoBehaviour {

	static List<RotationObject> rotationObjects = new List<RotationObject>(); 
	static int currentObject = 0;
	
	public RotationStruct () {
	}

	public static void Add(RotationObject rotationObject) {
		rotationObjects.Add (rotationObject);
		print ("Rotation Objects count: " + rotationObjects.Count);
	}

	public static IEnumerable <RotationObject> RotationObjectsValues() {
		return rotationObjects;
	}

	public static RotationObject Next() {
		if (rotationObjects.Count == 0) return null;
		currentObject += 1;
		if (currentObject > rotationObjects.Count-1) {
			currentObject = 0;
		}
		print ("currentObject: " + currentObject);
		return rotationObjects [currentObject];
	}

	public static RotationObject Previous() {
		if (rotationObjects.Count == 0) return null;
		currentObject -= 1;
		if (currentObject < 0) {
			currentObject = rotationObjects.Count-1;
		}
		print ("currentObject: " + currentObject);
		return rotationObjects [currentObject];
	}

}
