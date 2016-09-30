using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {

    public GameObject targetCamera;
	
	// Update is called once per frame
	void Update () {
        Vector3 v = targetCamera.transform.position - transform.position;
        transform.LookAt(targetCamera.transform.position - v);
        transform.rotation = (targetCamera.transform.rotation);
    }
}
