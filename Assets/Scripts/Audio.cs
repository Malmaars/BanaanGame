using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio instance;

    public AudioSource source;
    public AudioSource PitchedSource;
    public AudioSource ambientSource;
    public AudioSource ambientSourceGlobal;
    public AudioSource player;

    public Animator animator;
    public bool gliding;
    public bool grounded;


    private void Awake()
    {
        instance = this;
    }

    public void FixedUpdate()
    {
        gliding = animator.GetBool("Glide");
        grounded = animator.GetBool("Grounded");

        if (grounded)
        {
            StopAudio();
        }
    }

    public void Play(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void PlayRandomPitch(AudioClip clip)
    {
        float pitch = Random.Range(.8f, 1.2f);
        PitchedSource.pitch = pitch;
        PitchedSource.PlayOneShot(clip);
    }

    public void PlayWithRangeOnLoop(AudioClip clip)
    {
        if (!PitchedSource.isPlaying)
        {
            PitchedSource.Stop();
            PitchedSource.loop = true;
            PitchedSource.clip = clip;
            PitchedSource.Play();
        }
    }

    public void PlayOnLoop(AudioClip clip)
    {
        if (!ambientSourceGlobal.isPlaying)
        {
            ambientSourceGlobal.Stop();
            ambientSourceGlobal.loop = true;
            ambientSourceGlobal.clip = clip;
            ambientSourceGlobal.Play();
        }
    }

    public void WindPlayer(AudioClip clip)
    {
        if (!player.isPlaying && !grounded)
        {
            player.Stop();
            player.loop = true;
            player.clip = clip;
            player.Play();
        }
    }

    public void StopAudio()
    {
        player.loop = false;
        player.Stop();
    }
}



