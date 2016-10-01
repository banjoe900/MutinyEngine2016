using UnityEngine;
using System.Collections;

[System.Serializable]
[RequireComponent(typeof (AudioSource))]
public class AudioSourceManager : MonoBehaviour
{    
    public enum Type { Footsteps, Impact, Death, Throw, Pickup };
    public Type type;
    public AudioSource source;
    public AudioClip[] audioClips;
}
