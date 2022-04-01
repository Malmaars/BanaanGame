using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clipsFootsteps;

    [SerializeField]
    private AudioClip[] otherAudioClips;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Step()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
        Debug.Log("step bugged");
    }

    private void Landing()
    {
        AudioClip clip = GetLandingClip();
        audioSource.PlayOneShot(clip);
        
    }

    private AudioClip GetRandomClip()
    {
        return clipsFootsteps[UnityEngine.Random.Range(0, clipsFootsteps.Length)];
    }

    private AudioClip GetLandingClip()
    {
        return otherAudioClips[0];
    }
}
