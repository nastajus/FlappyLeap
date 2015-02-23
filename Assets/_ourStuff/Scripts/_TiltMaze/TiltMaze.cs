using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class TiltMaze : MonoBehaviour
{

    const float Rad2Deg = 180 / Mathf.PI;

    public GameObject controllerObject;
    public GameObject tiltMaze;

    void Start()
    {
    }

    void Update()
    {
        HandModel[] models = null;
        HandController hand = (HandController)controllerObject.GetComponent<HandController>();
        models = hand.GetAllGraphicsHands();

        if (models.Length == 1)
        {
            Vector3 handRotation = (models[0].GetPalmRotation().ToEulerAngles() * Rad2Deg);
            //Debug.Log(handRotation.z);
            if (handRotation.x > 30 && handRotation.x < 330)
            {
                handRotation.x = (tiltMaze.transform.rotation.ToEulerAngles() * Rad2Deg).x;
            }
            if (handRotation.z > 30 && handRotation.z < 330)
            {
                handRotation.z =  (tiltMaze.transform.rotation.ToEulerAngles() * Rad2Deg).z;
            }
            tiltMaze.transform.rotation =  Quaternion.Euler( new Vector3(handRotation.x,0f,handRotation.z) );

            //Debug.Log(tiltMaze.transform.rotation.ToEulerAngles() * Rad2Deg);
            FingerModel[] fingers = models[0].fingers;
            for (int x = 0; x < fingers.Length; x++)
            {
                Debug.DrawRay(fingers[x].GetRay().origin, fingers[x].GetRay().direction, Color.green);
            }
        }
    }
}