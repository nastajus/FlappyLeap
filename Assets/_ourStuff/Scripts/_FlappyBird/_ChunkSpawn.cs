using UnityEngine;
using System.Collections;

public class _ChunkSpawn : MonoBehaviour {

    private bool hasOccurredOnce = false;

    void OnTriggerEnter(Collider other)
    {
        if (hasOccurredOnce == false && other.tag == "Player")
        {

            Vector3 spawnDir;
            Vector3 playerDir = other.transform.forward;
            playerDir.y = 0.0f;                     // Make this vector planar (xz-plane)
            if (Mathf.Abs(playerDir.x) > Mathf.Abs(playerDir.z))
            {
                spawnDir = new Vector3(playerDir.x, 0.0f, 0.0f);              
            }
            else {
                spawnDir = new Vector3(0.0f, 0.0f, playerDir.z);
            }

            

            Vector3 sourceSz = this.collider.bounds.size;
            Vector3 spawnPos = MultV(this.transform.position, spawnDir);
            //Quaternion sourceQt = this.transform.rotation;

            Instantiate(this.gameObject, spawnPos, Quaternion.identity);

        }
    }

    private Vector3 MultV(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
    }
}
