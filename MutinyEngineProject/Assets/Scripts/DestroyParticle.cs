using UnityEngine;
using System.Collections;

public class DestroyParticle : MonoBehaviour {

    public float duration;
    private float timer = 0;
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
	    if(duration == 0)
        {
            var p = GetComponent<ParticleSystem>();
            if (p != null) duration = p.duration;
        }

        if (timer >= duration) Destroy(gameObject);
	}
}
