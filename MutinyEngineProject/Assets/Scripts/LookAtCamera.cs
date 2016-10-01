using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {

    public GameObject targetCamera;
	
	// Update is called once per frame
	void Update () {
        if(targetCamera == null)
        {
            targetCamera = FindObjectOfType<Camera>().gameObject;
        }
        Vector3 v = targetCamera.transform.position - transform.position;
        transform.LookAt(targetCamera.transform.position - v);
        transform.rotation = (targetCamera.transform.rotation);
    }
}
