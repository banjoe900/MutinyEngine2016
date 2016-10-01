using UnityEngine;
using System.Collections;

public class Projectile_Croissant_Spin : MonoBehaviour {
    private float randRotUp;
    private float randRotRight;
    private float randRotForward;

    // Use this for initialization
    void Start () {
        randRotUp = Random.Range(90f, 720f);
        randRotRight = Random.Range(90f, 720f);
        randRotForward = Random.Range(90f, 720f);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up * randRotUp * Time.deltaTime);
        transform.Rotate(Vector3.right * randRotRight * Time.deltaTime);
        transform.Rotate(Vector3.forward * randRotForward * Time.deltaTime);
    }
}
