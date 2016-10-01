using UnityEngine;
using System.Collections;

public class Smear : MonoBehaviour {

    public float duration = 10f;
    private SpriteRenderer sr;
    private float timer;

	// Use this for initialization
	void Start () {
        sr = GetComponentInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        var lerp = Mathf.Lerp(sr.color.a, 0, Time.deltaTime);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, lerp);
        if(timer >= duration)
        {
            Destroy(gameObject);
        }
	}
}
