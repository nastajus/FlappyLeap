using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class _Test3 : MonoBehaviour
{

    public GameObject controllerObject;
    GameObject cubey;

    void Start()
    {
        cubey = GameObject.CreatePrimitive(PrimitiveType.Cube);
    }

    void Update()
    {
        HandModel[] models = null;
        HandController hand = (HandController)controllerObject.GetComponent<HandController>();
        models = hand.GetAllGraphicsHands();

        if (models.Length > 0)
        {
            cubey.transform.position = models[0].GetPalmPosition();
            cubey.transform.rotation = models[0].GetPalmRotation();
            FingerModel[] fingers = models[0].fingers;
            for (int x=0; x<fingers.Length; x++)
            {
                Debug.DrawRay(fingers[x].GetRay().origin, fingers[x].GetRay().direction,Color.green);
            }
        }
    }
}