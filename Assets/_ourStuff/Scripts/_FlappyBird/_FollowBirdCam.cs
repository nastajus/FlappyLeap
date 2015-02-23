using UnityEngine;
using System.Collections;

public class _FollowBirdCam : MonoBehaviour {

	public GameObject bird; 
	private float initDistance;

	// Use this for initialization
	void Start () {
		if (bird == null)
		{
			Debug.LogWarning("No bird attached, please attach for Camera to follow");
		}
		initDistance = (bird.transform.position.z - this.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, bird.transform.position.z - initDistance );
	}
}
