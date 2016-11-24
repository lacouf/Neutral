using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[RequireComponent (typeof(LineRenderer))]
public class GameControl : MonoBehaviour {

	public static GameControl control;
	public PlayerData playerData;

	RotationObject rotationObject = null;
	List<GameObject> coloredObjects = new List<GameObject> ();

	// UI
	DisplayCanvas displayCanvas;
	UnityEngine.UI.Text rotationObjectName;
	UnityEngine.UI.Text angle1;
	UnityEngine.UI.Text angle2;
	UnityEngine.UI.Text angle3;
    UnityEngine.UI.Text angle4;
    UnityEngine.UI.Text angle5;
    UnityEngine.UI.Text angle6;
    UnityEngine.UI.Text angle7;
    UnityEngine.UI.Text angle8;
    UnityEngine.UI.Text angle9;
    UnityEngine.UI.Text angle10;
    UnityEngine.UI.Text angle11;
    UnityEngine.UI.Text angle12;
    UnityEngine.UI.Text angle13;
    UnityEngine.UI.Text angle14;
    UnityEngine.UI.Text angle15;
    UnityEngine.UI.Text angle16;
    UnityEngine.UI.Text angle17;
    UnityEngine.UI.Text angle18;
    UnityEngine.UI.Text angle19;
    UnityEngine.UI.Text angle20;
    UnityEngine.UI.Text angle21;
    UnityEngine.UI.Text angle22;
    UnityEngine.UI.Text angle23;
    private List<UnityEngine.UI.Text> angles = new List<UnityEngine.UI.Text>();

    // GameObjects
    GameObject belly;
	GameObject head;
	GameObject betweenShoulders;
	GameObject leftShoulder;
	GameObject leftElbow;
	GameObject leftHand;
	GameObject rightShoulder;
	GameObject rightElbow;
	GameObject rightHand;
	GameObject betweenHips;
	GameObject leftHip;
	GameObject leftKnee;
	GameObject leftFoot;
	GameObject rightHip;
	GameObject rightKnee;
	GameObject rightFoot;

	private LineRenderer lineRenderer;

	void Awake () {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
		} else if (control != this) {
			Destroy (gameObject);
		}

		FindGameObjects ();

        FindAngleUI();
			
		RotationStruct.Add (CreateRotationObject ("Head & Shoulders around belly", belly, betweenShoulders, Vector3.right,  Vector3.forward, Vector3.up, head, betweenShoulders, leftShoulder, leftElbow, leftHand, rightShoulder, rightElbow, rightHand));
		RotationStruct.Add (CreateRotationObject ("Hips and legs around belly", belly, betweenHips, Vector3.right, Vector3.back, Vector3.up, betweenHips, leftHip, leftKnee, leftFoot, rightHip, rightKnee, rightFoot));
		RotationStruct.Add (CreateRotationObject ("Left shoulder around spine", betweenShoulders, leftShoulder, Vector3.forward, Vector3.left, Vector3.up, leftShoulder, leftElbow, leftHand));
		RotationStruct.Add (CreateRotationObject ("Right shoulder around spine", betweenShoulders, rightShoulder, Vector3.forward, Vector3.right, Vector3.up, rightShoulder, rightElbow, rightHand));
		RotationStruct.Add (CreateRotationObject ("Shoulders around Y axis", betweenShoulders, head, Vector3.up, Vector3.forward, Vector3.right, head, leftShoulder, leftElbow, leftHand, rightShoulder, rightElbow, rightHand));

		RotationStruct.Add (CreateRotationObject ("Left Elb around shoulders X", leftShoulder, leftElbow, Vector3.right, Vector3.forward, Vector3.up, leftElbow, leftHand));
        RotationStruct.Add (CreateRotationObject ("Right Elb around shoulders X", rightShoulder, rightElbow, Vector3.right, Vector3.forward, Vector3.up, rightElbow, rightHand));
		RotationStruct.Add (CreateRotationObject ("Left Elb around shoulders Y", leftShoulder, leftElbow, Vector3.up, Vector3.forward, Vector3.right, leftElbow, leftHand));
		RotationStruct.Add (CreateRotationObject ("Right Elb around shoulders Y", rightShoulder, rightElbow, Vector3.up, Vector3.forward, Vector3.right, rightElbow, rightHand));
        RotationStruct.Add (CreateRotationObject ("Left Hand around Elb X", leftElbow, leftHand, Vector3.right, Vector3.forward, Vector3.up, leftHand));
        RotationStruct.Add (CreateRotationObject ("Right Hand around Elb X", rightElbow, rightHand, Vector3.right, Vector3.forward, Vector3.up, rightHand));
        RotationStruct.Add (CreateRotationObject ("Left Hand around Elb Y", leftElbow, leftHand, Vector3.up, Vector3.forward, Vector3.right, leftHand));
        RotationStruct.Add (CreateRotationObject ("Right Hand around Elb Y", rightElbow, rightHand, Vector3.up, Vector3.forward, Vector3.right, rightHand));

        RotationStruct.Add (CreateRotationObject ("Hips around X axis", betweenHips, leftHip, Vector3.forward, Vector3.left, Vector3.up, leftHip, leftKnee, leftFoot, rightHip, rightKnee, rightFoot));
		RotationStruct.Add (CreateRotationObject ("Hips around Y axis", betweenHips, leftHip, Vector3.up, Vector3.left, Vector3.forward, leftHip, leftKnee, leftFoot, rightHip, rightKnee, rightFoot));
        RotationStruct.Add (CreateRotationObject ("Left knee around hips Y", leftHip, leftKnee, Vector3.up, Vector3.back, Vector3.left, leftKnee, leftFoot));
        RotationStruct.Add (CreateRotationObject ("Right knee around hips Y", rightHip, rightKnee, Vector3.up, Vector3.back, Vector3.right, rightKnee, rightFoot));
        RotationStruct.Add (CreateRotationObject ("Left knee around hips X", leftHip, leftKnee, Vector3.right, Vector3.back, Vector3.up, leftKnee, leftFoot));
        RotationStruct.Add (CreateRotationObject ("Right knee around hips X", rightHip, rightKnee, Vector3.right, Vector3.back, Vector3.up, rightKnee, rightFoot));
        RotationStruct.Add (CreateRotationObject ("Left foot around knee Y", leftKnee, leftFoot, Vector3.up, Vector3.back, Vector3.left, leftFoot));
        RotationStruct.Add (CreateRotationObject ("Right foot around knee Y", rightKnee, rightFoot, Vector3.up, Vector3.back, Vector3.right, rightFoot));
        RotationStruct.Add (CreateRotationObject ("Left foot around knee X", leftKnee, leftFoot, Vector3.right, Vector3.back, Vector3.up, leftFoot));
        RotationStruct.Add (CreateRotationObject ("Right foot around knee X", rightKnee, rightFoot, Vector3.right, Vector3.back, Vector3.up, rightFoot));
    }

	void Update () {
		bool needRedraw = false;
		
		if (Input.GetButton ("XBoxBt7") || Input.GetKeyDown (KeyCode.Q)) {
			rotationObject = RotationStruct.Next ();
			needRedraw = true;
		} else if (Input.GetButton ("XBoxBt8") || Input.GetKeyDown (KeyCode.W)) {
			rotationObject = RotationStruct.Previous ();
			needRedraw = true;
		}


		if (Input.GetKey (KeyCode.Equals)) {
			RotateObject (rotationObject, 1);
			needRedraw = true;
		} else if (Input.GetKey (KeyCode.Minus)) {
			RotateObject (rotationObject, -1);
			needRedraw = true;
		}

		if (needRedraw && rotationObject != null) {
			needRedraw = false;
			UnColorObjects ();
			DrawAxisAndColorObjects (rotationObject);
			DrawUI (rotationObject);
		}
	}

	RotationObject CreateRotationObject (string name, GameObject goToRotateAround, GameObject pointForAngleCalc, Vector3 rotationAxis, Vector3 axisForAngleCalc, Vector3 signAxis, params GameObject[] values) {
		RotationObject rotObject = new RotationObject ();
		rotObject.name = name;
		rotObject.goToRotateAround = goToRotateAround;
		rotObject.pointForAngleCalc = pointForAngleCalc;
		rotObject.rotationAxis = rotationAxis;
		rotObject.axisForAngleCalc = axisForAngleCalc;
		rotObject.signAxis = signAxis;
		foreach (GameObject go in values) {
			if (go == null) {
				print ("Can't get gameObject");
			}
			rotObject.dependantObjects.Add (go);
		}
		return rotObject;
	}

	void DrawAxisAndColorObjects (RotationObject rot) {
		CreateLineRenderers ();
		DrawAxisRotation (rot);
		ColorObjectsRed (rot);
	}

	void DrawAxisRotation (RotationObject rot) {
		lineRenderer.SetVertexCount (2);
		lineRenderer.enabled = true;
		lineRenderer.material = new Material (Shader.Find ("Particles/Additive"));
		lineRenderer.SetColors (Color.green, Color.green);
		lineRenderer.SetWidth (0.2f, 0.2f);
		lineRenderer.useWorldSpace = false;
		Vector3 point1 = rot.goToRotateAround.transform.position + (rot.goToRotateAround.transform.rotation * rot.rotationAxis * 3);
		Vector3 point2 = rot.goToRotateAround.transform.position + (rot.goToRotateAround.transform.rotation * rot.rotationAxis * -3);
		int i = 0;
		lineRenderer.SetPosition (i++, point1);
		lineRenderer.SetPosition (i++, point2);
	}

	void ColorObjectsRed (RotationObject rot) {
		foreach (GameObject go in rot.dependantObjects) {
			if (go != null) {
				Renderer renderer = go.GetComponent<Renderer> ();
				renderer.material.color = Color.red;
				coloredObjects.Add (go);
			}
		}
	}

	void UnColorObjects () {
		if (coloredObjects.Count == 0)
			return;
		foreach (GameObject go in coloredObjects) {
			Renderer renderer = go.GetComponent<Renderer> ();
			renderer.material.color = Color.white;
		}
		coloredObjects.Clear ();
	}

	void DrawUI (RotationObject rot) {
		rotationObjectName.text = rot.name;

		Vector3 targetDir = rot.goToRotateAround.transform.position - rot.pointForAngleCalc.transform.position;
		Vector3 targetDirForDebug = targetDir;
		targetDir = rot.goToRotateAround.transform.InverseTransformDirection (targetDir);
		Vector3 targetDir2ForDebug = targetDir;

		if (rot.rotationAxis == Vector3.right || rot.rotationAxis == Vector3.left) {
			targetDir.x = 0;
		} else if (rot.rotationAxis == Vector3.up || rot.rotationAxis == Vector3.down) {
			targetDir.y = 0;
		} else if (rot.rotationAxis == Vector3.forward || rot.rotationAxis == Vector3.back) {
			targetDir.z = 0;
		}

		bool isPositive = true;
		if (rot.signAxis == Vector3.right) {
			isPositive = targetDir.x >= 0; 
		} else if (rot.signAxis == Vector3.up) {
			isPositive = targetDir.y >= 0;
		} else if (rot.signAxis == Vector3.forward) {
			isPositive = targetDir.z >= 0;
		} else if (rot.signAxis == Vector3.left) {
            isPositive = targetDir.x <= 0;
        } else if (rot.signAxis == Vector3.down) {
            isPositive = targetDir.y <= 0;
        } else if (rot.signAxis == Vector3.back) {
            isPositive = targetDir.z <= 0;
        }

        float angle = Vector3.Angle (rot.axisForAngleCalc, targetDir) * (isPositive ? 1 : -1);
		//angle1.text = "" + angle;
        int index = FindRotationObjectPosition(rot);
        if (index != -1) {
            angles[index].text = "" + angle;
        }

		if (Input.GetKey (KeyCode.B)) {
			print ("Point1 " + rot.goToRotateAround.transform.position + " Point2 " + rot.pointForAngleCalc.transform.position + " targetDir " + targetDirForDebug + " targetDir2 " + targetDir2ForDebug);
			print (rot.name + " Angle " + angle + " rotaxis " +rot.rotationAxis + " axis for anglecalc " + rot.axisForAngleCalc + " targetDir " + targetDir);
		}

	}

    private int FindRotationObjectPosition(RotationObject rot) {
        int i = 0;
        foreach (RotationObject aRot in RotationStruct.RotationObjectsValues()) {
            if (aRot.name.Equals(rot.name)) {
                return i;
            }
            i++;
        }
        return -1;
    }

    void RotateObject (RotationObject ro, int direction) {

		foreach (GameObject go in ro.dependantObjects) {
			if (go != null) {
				//Vector3 rotationFrom = go.transform.position;
				//Quaternion actualRotation = go.transform.rotation;
				//print ("Game Object: " + go.name + " AroundObject : " + ro.goToRotateAround.name + " go pos " + go.transform.position + " axis " + ro.rotationAxis );
				go.transform.RotateAround (ro.goToRotateAround.transform.position, ro.goToRotateAround.transform.rotation * ro.rotationAxis, (0.5f * direction));
			}
		}
	}

	float GetAngleOnAxis (Quaternion fromAngle, Vector3 axis) {
		if (axis == Vector3.right) {
			return fromAngle.eulerAngles.x;
		} else if (axis == Vector3.up) {
			return fromAngle.eulerAngles.y;
		} else {
			return fromAngle.eulerAngles.z;
		}
	}

	void CreateLineRenderers () {
		
		if (lineRenderer == null) {
			lineRenderer = GetComponent<LineRenderer> ();
			if (lineRenderer == null) {
				lineRenderer = gameObject.AddComponent<LineRenderer> ();
			}
		}

	}

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        //print ("Path: " + Application.persistentDataPath);
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        data.someData = 1;
        data.someOtherData = 2;
        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            playerData = (PlayerData)bf.Deserialize(file);
            file.Close();
        }

    }

    void FindGameObjects()
    {
        belly = GameObject.Find(Constants.BELLY);
        head = GameObject.Find(Constants.HEAD);
        betweenShoulders = GameObject.Find(Constants.BETWEENSHOULDERS);
        leftShoulder = GameObject.Find(Constants.LEFTSHOULDER);
        leftElbow = GameObject.Find(Constants.LEFTELBOW);
        leftHand = GameObject.Find(Constants.LEFTHAND);
        rightShoulder = GameObject.Find(Constants.RIGHTSHOULDER);
        rightElbow = GameObject.Find(Constants.RIGHTELBOW);
        rightHand = GameObject.Find(Constants.RIGHTHAND);
        betweenHips = GameObject.Find(Constants.BETWEENHIPS);
        leftHip = GameObject.Find(Constants.LEFTHIP);
        leftKnee = GameObject.Find(Constants.LEFTKNEE);
        leftFoot = GameObject.Find(Constants.LEFTFOOT);
        rightHip = GameObject.Find(Constants.RIGHTHIP);
        rightKnee = GameObject.Find(Constants.RIGHTKNEE);
        rightFoot = GameObject.Find(Constants.RIGHTFOOT);

        if (belly == null)
            print("belly null");
        if (head == null)
            print("head null");
        if (betweenShoulders == null)
            print("betweenShoulders null");
        if (leftShoulder == null)
            print("leftShoulder null");
        if (leftElbow == null)
            print("leftElbow null");
        if (leftHand == null)
            print("leftHand null");
        if (rightShoulder == null)
            print("rightShoulder null");
        if (rightElbow == null)
            print("rightElbow null");
        if (rightHand == null)
            print("rightHand null");
        if (betweenHips == null)
            print("betweenHips null");
        if (leftHip == null)
            print("leftHip null");
        if (leftKnee == null)
            print("leftKnee null");
        if (leftFoot == null)
            print("leftFoot null");
        if (rightHip == null)
            print("rightHip null");
        if (rightKnee == null)
            print("rightKnee null");
        if (rightFoot == null)
            print("rightFoot null");
    }

    void FindAngleUI() {
        if (displayCanvas == null) {
            displayCanvas = (DisplayCanvas)FindObjectOfType(typeof(DisplayCanvas));
            //print ("Display Canvas " + displayCanvas);
            if (displayCanvas != null) {

                foreach (Transform child in displayCanvas.transform) {
                    //print ("Child name: " + child.gameObject.name);
                    if (child.gameObject.name.Equals("Panel")) {
                        RectTransform panel = child.gameObject.GetComponent<RectTransform>();
                        foreach (Transform child2 in panel.transform) {
                            //print ("Child2 " + child2.gameObject.name);
                            if (child2.name.Equals("RotationObjectText"))
                            {
                                rotationObjectName = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                                rotationObjectName.text = "Rotation Object";
                            }
                            else if (child2.name.Equals("Angle1"))
                            {
                                angle1 = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                            }
                            else if (child2.name.Equals("Angle2"))
                            {
                                angle2 = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                            }
                            else if (child2.name.Equals("Angle3"))
                            {
                                angle3 = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                            }
                            else if (child2.name.Equals("Angle4"))
                            {
                                angle4 = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                            }
                            else if (child2.name.Equals("Angle5"))
                            {
                                angle5 = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                            }
                            else if (child2.name.Equals("Angle6"))
                            {
                                angle6 = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                            }
                            else if (child2.name.Equals("Angle7"))
                            {
                                angle7 = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                            }
                            else if (child2.name.Equals("Angle8"))
                            {
                                angle8 = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                            }
                            else if (child2.name.Equals("Angle9"))
                            {
                                angle9 = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                            }
                            else if (child2.name.Equals("Angle10"))
                            {
                                angle10 = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                            }
                            else if (child2.name.Equals("Angle11"))
                            {
                                angle11 = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                            }
                            else if (child2.name.Equals("Angle12"))
                            {
                                angle12 = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                            }
                            else if (child2.name.Equals("Angle13"))
                            {
                                angle13 = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                            }
                            else if (child2.name.Equals("Angle14"))
                            {
                                angle14 = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                            }
                            else if (child2.name.Equals("Angle15"))
                            {
                                angle15 = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                            }
                            else if (child2.name.Equals("Angle16"))
                            {
                                angle16 = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                            }
                            else if (child2.name.Equals("Angle17"))
                            {
                                angle17 = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                            }
                            else if (child2.name.Equals("Angle18"))
                            {
                                angle18 = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                            }
                            else if (child2.name.Equals("Angle19"))
                            {
                                angle19 = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                            }
                            else if (child2.name.Equals("Angle20"))
                            {
                                angle20 = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                            }
                            else if (child2.name.Equals("Angle21"))
                            {
                                angle21 = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                            }
                            else if (child2.name.Equals("Angle22"))
                            {
                                angle22 = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                            }
                            else if (child2.name.Equals("Angle23"))
                            {
                                angle23 = child2.gameObject.GetComponent<UnityEngine.UI.Text>();
                            }
                        }
                    }
                    //					if (child.gameObject.name.Equals (SHOTSTEXT)) {
                    //						shotsText = child.gameObject.GetComponent<UnityEngine.UI.Text> ();
                    //						shotsText.text = "2000";
                    //					}
                }
            }
        }
        angles.Add(angle1);
        angles.Add(angle2);
        angles.Add(angle3);
        angles.Add(angle4);
        angles.Add(angle5);
        angles.Add(angle6);
        angles.Add(angle7);
        angles.Add(angle8);
        angles.Add(angle9);
        angles.Add(angle10);
        angles.Add(angle11);
        angles.Add(angle12);
        angles.Add(angle13);
        angles.Add(angle14);
        angles.Add(angle15);
        angles.Add(angle16);
        angles.Add(angle17);
        angles.Add(angle18);
        angles.Add(angle19);
        angles.Add(angle20);
        angles.Add(angle21);
        angles.Add(angle22);
        angles.Add(angle23);

    }
}