using UnityEngine;
using System.Collections;

public class CharacterAudioManager : MonoBehaviour {

    public AudioSourceManager lightFootsteps;
    public AudioSourceManager heavyFootsteps;
    public AudioSourceManager impact;
    public AudioSourceManager throwCookie;
    public AudioSourceManager throwCake;
    public AudioSourceManager throwCroissant;
    public AudioSourceManager pickup;
    public AudioSourceManager death;
    public SpeechAudioSourceManager speech;

    public void PlayTakeDamageAudio()
    {
        PlayRandomFromClips(impact);
    }

    public void PlayThrowCookieAudio()
    {
        PlayRandomFromClips(throwCookie);
    }

    public void PlayThrowCakeAudio()
    {
        PlayRandomFromClips(throwCake);
    }

    public void PlayThrowCroissantAudio()
    {
        PlayRandomFromClips(throwCroissant);
    }

    public void PlayFootstepAudio()
    {
        var behavior = GetComponentInParent<PlayerBehavior>();
        if (behavior.IsAlive)
        {
            if (behavior.sugarLevel >= behavior.sugarLimit / 2)
            {
                PlayRandomFromClips(heavyFootsteps);
            }
            else
            {
                PlayRandomFromClips(lightFootsteps);
            }
        }
    }

    public void PlayPickupAudio()
    {
        PlayRandomFromClips(pickup);
    }

    public void PlayDeathAudio()
    {
        PlayRandomFromClips(death);
    }

    public void PlaySpeechPhrase(Phrase.PhraseType phraseType)
    {
        PlayRandomPhraseOfType(phraseType);
    }

    protected void PlayRandomFromClips(AudioSourceManager sourceManager)
    {
        if (!sourceManager.source.isPlaying)
        {
            sourceManager.source.PlayOneShot(sourceManager.audioClips[Random.Range(0, sourceManager.audioClips.Length - 1)]);
        }
    }

    protected void PlayRandomPhraseOfType(Phrase.PhraseType phraseType)
    {
        if (!speech.source.isPlaying)
        {
            foreach (var phrase in speech.phrases)
            {
                if (!speech.source.isPlaying)
                {
                    if(phrase.phraseType == phraseType)
                    {
                        speech.source.PlayOneShot(phrase.audioClips[Random.Range(0, phrase.audioClips.Length - 1)]);
                    }
                }
            }
        }
    }
}
