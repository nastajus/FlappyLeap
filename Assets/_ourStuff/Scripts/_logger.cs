using UnityEngine;
using System.Collections;

public class _logger : MonoBehaviour {

    Vector3 min, max;

	// Use this for initialization
	void Start () {
        min = transform.position;
        max = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x > max.x) set(max, transform.position.x, max.y, max.z);
        if (transform.position.y > max.y) set(max, max.x, transform.position.y, max.z);
        if (transform.position.z > max.z) set(max, max.x, max.y, transform.position.z);

        if (transform.position.x < min.x) set(min, transform.position.x, min.y, min.z);
        if (transform.position.y < min.y) set(min, min.x, transform.position.y, min.z);
        if (transform.position.z < min.z) set(min, min.x, min.y, transform.position.z);
        
        Debug.Log("MIN: " + min + ", MAX: " + max);
	}

    private void set(Vector3 vector, float x, float y, float z)
    {
        vector = new Vector3(x,y,z);
    }
}
