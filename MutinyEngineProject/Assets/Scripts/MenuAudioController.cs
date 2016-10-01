using UnityEngine;
using System.Collections;

public class MenuAudioController : MonoBehaviour {

	public AudioClip[] aClips;
	public AudioClip validSound;
	public AudioClip invalidSound;

	public AudioSource aSource;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void PlayMenuSound(){

		int index = Random.Range(0, 2);

		aSource.clip = aClips[index];
		aSource.Play();

	}

	public void PlayNegativeSound(){

		aSource.clip = invalidSound;
		aSource.Play();

	}

	public void PlayPositiveSound(){

		aSource.clip = validSound;
		aSource.Play();

	}
}
