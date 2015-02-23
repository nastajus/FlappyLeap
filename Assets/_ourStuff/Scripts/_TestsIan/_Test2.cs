using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class _Test2 : MonoBehaviour {

    enum DirPos { Dir, Pos, DirPos };
    enum VectorType { toUnity, toUnityScaled, Vector3 }


	Controller controller;
    List<GameObject> drawingCylindars;
    List<GameObject> drawingCubes;
    Vector3 cylindarScale;
    Vector3 cubeScale;
    GameObject cubey;

	// Use this for initialization
	void Start () {
		controller = new Controller();
        drawingCylindars = new List<GameObject>();
        cylindarScale = new Vector3(1,20,1);
        cubeScale = new Vector3(1, 1, 1);

        cubey = GameObject.CreatePrimitive(PrimitiveType.Cube);


	}
	
	// Update is called once per frame
	void Update () {
		if (controller.HasFocus)
		{
			Frame frame = controller.Frame();
			HandList hands = frame.Hands;
			PointableList pointables = frame.Pointables;
			FingerList fingers = frame.Fingers;
			ToolList tools = frame.Tools;


            int appWidth = 20;
            int appHeight = 20;
            int appDepth = 20;

            InteractionBox iBox = frame.InteractionBox;
            Pointable pointable = frame.Pointables.Frontmost;

            Leap.Vector leapPoint = pointable.StabilizedTipPosition;
            Leap.Vector normalizedPoint = iBox.NormalizePoint(leapPoint, false);

            float appX = normalizedPoint.x * appWidth;
            float appY = (1 - normalizedPoint.y) * appHeight;
            float appZ = normalizedPoint.z * appDepth;



            cubey.transform.position = new Vector3(appX, appY, appZ);


            //logAllPalmPos(hands);
            //logAllFingerDir(fingers, "not leap coords");

            //VectorType vt = VectorType.toUnity;
            //drawRays(pointables, 10, .01f, vt);
            //drawCubes(pointables, cubeScale, vt);


            //drawCylindars(fingers, cylindarScale );

            //drawInteractionBox(frame.InteractionBox);

        }
	}



    private void drawInteractionBox(InteractionBox interactionBox)
    {
        Vector3 c = interactionBox.Center.ToUnity();
        float h = interactionBox.Height;
    }

    private void drawCylindars(FingerList fingers, Vector3 cylindarScale)
    {
        int whichFinger = 0;
        foreach (Finger finger in fingers)
        {
            //create just once for now
            if (drawingCylindars.Count < fingers.Count)
            {
                GameObject cylindar = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                drawingCylindars.Add(cylindar);
                drawingCylindars[whichFinger].transform.localScale = cylindarScale;

            }

            //Vector3 pos = Nastajus.Util.getVector3(finger.TipPosition);
            //Vector3 pos = finger.TipPosition.ToUnityScaled();
            //drawingCylindars[whichFinger].transform.position = pos;

            //Vector3 dir = Nastajus.Util.getVector3(finger.Direction);
            //drawingCylindars[whichFinger].transform.rotation = Quaternion.Euler(dir);
            //drawingCylindars[whichFinger].transform.eulerAngles = finger.Direction.ToUnity();

            whichFinger++;

        }
    }


    private void drawCubes(PointableList pointables, Vector3 cubeScale, VectorType vt)
    {
        int whichFinger = 0;
        foreach (Pointable pointable in pointables)
        {




            //Vector3 pos = Nastajus.Util.getVector3(finger.TipPosition);
            //Vector3 pos = finger.TipPosition.ToUnityScaled();
            //drawingCylindars[whichFinger].transform.position = pos;

            //Vector3 dir = Nastajus.Util.getVector3(finger.Direction);
            //drawingCylindars[whichFinger].transform.rotation = Quaternion.Euler(dir);
            //drawingCylindars[whichFinger].transform.eulerAngles = finger.Direction.ToUnity();

            whichFinger++;

        }
    }

    private static void drawRays(PointableList pointables, float length, float durationSeconds, VectorType vt)
    {
        Vector3 pos, dir;

        foreach (Pointable pointable in pointables)
        {
            if (vt == VectorType.toUnity)
            {
                pos = pointable.TipPosition.ToUnity()/100;
                dir = pointable.Direction.ToUnity();
            }
            else if (vt == VectorType.toUnityScaled)
            {
                pos = pointable.TipPosition.ToUnityScaled()/100; 
                dir = pointable.Direction.ToUnityScaled();   
            }
            else// if (vt == VectorType.Vector3)
            {
                pos = Nastajus.Util.getVector3(pointable.TipPosition)/100;
                dir = Nastajus.Util.getVector3(pointable.Direction);
            }

            Debug.DrawRay(pos, dir * length, Color.green, durationSeconds, true);
        }
    }


    private static void logAllFingerEulers(FingerList fingers)
    {
        string s = "";
        Quaternion dir;

        foreach (Finger finger in fingers)
        {
            dir = Quaternion.Euler(Nastajus.Util.getVector3(finger.Direction));
            s += finger.Id + ": " + dir; //Util.RoundToDec(dir, 2) ;
        }
        Debug.Log(s);

    }


    /// <summary>
    /// Accepts a list of fingers, and outputs PRY: Pitch, Roll, Yaw
    /// </summary>
    private static void logAllFingerDir(FingerList fingers)
    {
        string s = "";
        string dir;

        foreach (Finger finger in fingers)
        {
            dir = Nastajus.Util.getVector3(finger.Direction).ToString();
            s += finger.Id + ": " + dir; //Util.RoundToDec(dir, 2) ;
        }
        Debug.Log(s);
    }

    /// <summary>
    /// Accepts a list of fingers, and outputs either Unity's Vector3, or Leap's Vector
    /// </summary>
    /// <param name="fingers"></param>
    /// <param name="magicString"></param> for now will suffice, any other string than "Leap" results in Vector3
    private static void logAllFingerDir(FingerList fingers, string magicString)
    {
        string s = "";
        string dir;

        foreach (Finger finger in fingers)
        {
            if (magicString == "Leap")
            {
                dir = finger.Direction.ToString();
            }
            else 
            {
                dir = Nastajus.Util.getVector3(finger.Direction).ToString();
            }
            s += finger.Id + ": " + dir; //Util.RoundToDec(dir, 2) ;
        }
        Debug.Log(s);
    }

    private static void logAllFingerPos(FingerList fingers)
    {
        string s = "";
        string dir;

        foreach (Finger finger in fingers)
        {
            dir = finger.Direction.ToUnityScaled().ToString();
            s += finger.Id + ": " + dir; //Util.RoundToDec(dir, 2) ;
        }
        Debug.Log(s);
    }

    private static void logAllPalmPos(HandList hands)
    {
        string s = "";
        foreach (Hand hand in hands)
        {
            Vector position = Nastajus.Util.RoundToInt(hand.PalmPosition);
            s += hand.Id + ": " + position + ", ";
        }
        Debug.Log(s);
        //Vector positionRight = Util.RoundToInt(hands.Rightmost.PalmPosition);
        //Vector positionLeft = Util.RoundToInt(hands.Leftmost.PalmPosition);

    }
}
