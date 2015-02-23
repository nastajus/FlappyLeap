using UnityEngine;
using System.Collections;

public class _TiltEnd : MonoBehaviour {

    public GameObject playerSphere;

    void Start()
    {
        if (playerSphere == null)
        {
            Debug.LogError("playerSphere not attached");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (playerSphere!=null && other.gameObject == playerSphere)
            Debug.Log("WIN");
    }

}
