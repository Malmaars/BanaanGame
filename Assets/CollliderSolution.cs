using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollliderSolution : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            RaycastHit groundHit;
            Physics.Raycast(new Vector3(other.transform.position.x, 100, other.transform.position.z), Vector3.down, out groundHit, Mathf.Infinity);

            other.transform.position = groundHit.point  + Vector3.up;

        }
    }
}
