using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class _FlapPathing : MonoBehaviour {

	public GameObject controllerObject;

	
	private GameObject bird;
	private float magnitude = .7f;
	private HandModel[] models;
	private HandController controller;
	private Vector3 lastPosition = Vector3.zero;
	private Vector3 currentPosition;
    private Vector3 currentAngle; 
	private const float ceiling = 10f;
	private const float flooring = 0f;
	private const float moveForwardMag = 1f;

    private const float Rad2Deg = 180 / Mathf.PI;


	// Use this for initialization
	void Start () {
		controller = (HandController)controllerObject.GetComponent<HandController>();
		bird = this.gameObject;
	}


	// Update is called once per frame
	void Update()
	{
		models = null;
		models = controller.GetAllGraphicsHands();

		if (models.Length > 0)
		{
			currentPosition = models[0].GetPalmPosition();
            currentAngle = models[0].GetPalmDirection();
            //Debug.Log(currentAngle);

            Vector3 handRotation = (models[0].GetPalmRotation().ToEulerAngles() * Rad2Deg);
            //Debug.Log(handRotation);

            //bird.transform.eulerAngles = handRotation;
            bird.transform.rotation = Quaternion.Euler(new Vector3(0f, handRotation.y, 0f));


            //Debug.Log(bird.transform.eulerAngles.x + ", " + bird.transform.eulerAngles.y + ", " + bird.transform.eulerAngles.z);

            //rotate accordingly

             
            
		}

		//flap to elevate, spread fingers to glide
		if (lastPosition != Vector3.zero && bird != null)
		{

			var fingers = controller.GetFrame().Hands[0].Fingers;
			float angleSum = 0;
			for(int i =1; i < fingers.Count - 1; i++)
			{
				angleSum += (fingers[i].Direction - fingers[i + 1].Direction).Magnitude;
			}
			
			angleSum -= 0.2f;
			angleSum /= 0.8f;
			angleSum = Mathf.Clamp(angleSum, 0f, 1f);
			//Debug.Log("anglesum: " + angleSum);

            //drag?
			bird.rigidbody.AddForce(-Physics.gravity * angleSum);
			bird.rigidbody.velocity = bird.rigidbody.velocity * 0.99f;


			Vector3 direction = lastPosition - currentPosition;
			float velocity = direction.magnitude / Time.deltaTime;

            //upward force
			bird.rigidbody.AddForce(Vector3.up * velocity * magnitude);
		}

		//clamp position
		if (bird.transform.position.y > ceiling)
		{
			bird.transform.position = new Vector3(bird.transform.position.x, ceiling, bird.transform.position.z);
		}
		else if (bird.transform.position.y < flooring)
		{
			bird.transform.position = new Vector3(bird.transform.position.x, flooring, bird.transform.position.z);
		}


		//advance forward
		bird.transform.position = new Vector3(bird.transform.position.x, bird.transform.position.y, bird.transform.position.z + moveForwardMag * Time.deltaTime);

		//must be last
		lastPosition = currentPosition;
			
	}
}
