using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomSplat : MonoBehaviour {

    public List<Sprite> sprites = new List<Sprite>();
    public SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }
        sr.sprite = sprites[Random.Range(0, sprites.Count)];

        transform.localScale = Vector3.one * Random.Range(0.1f, 0.3f);
    }
}
