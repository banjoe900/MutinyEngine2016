using UnityEngine;
using System.Collections;

public class PieSpawner : MonoBehaviour {

    public GameObject minX, maxX, minZ, maxZ;
    public GameObject piePrefab;
    private bool _isEnabled = false;
    public bool isEnabled
    {
        get
        {
            return _isEnabled;
        }

        set
        {
            _isEnabled = value;
            if (_isEnabled)
            {
                timer = 0;
            }
        }
    }
    public float spawnRate = 17f;
    private float timer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isEnabled)
        {
            if(timer <= spawnRate)
            {
                timer += Time.deltaTime;
            }
            else
            {
                Instantiate(piePrefab, new Vector3(Random.Range(minX.transform.position.x, maxX.transform.position.x), 50, Random.Range(minZ.transform.position.z, maxZ.transform.position.z)), piePrefab.transform.rotation);
                timer = 0;
            }
        }
	}


}
