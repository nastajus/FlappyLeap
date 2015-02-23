using UnityEngine;
using System.Collections;

public class _TestInstanOri : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Instantiate(this.gameObject, this.transform.position + new Vector3(0, 0, 4f), this.transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 rEuler = this.transform.rotation.ToEuler();
        Vector3 rEulerAngles = this.transform.rotation.eulerAngles;

        Debug.Log(rEuler);
        //Debug.Log(rEulerAngles);



        //Vector3 rEulerAngles = this.transform.rotation.ToEulerAngles();
        //this.transform.rotation =  Quaternion.Euler(this.transform.rotation.)
	}
}
