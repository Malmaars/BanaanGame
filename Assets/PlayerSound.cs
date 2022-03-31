using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTimer;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private AudioClip glidingAudio;
 
    [SerializeField] public SquirrelMovement squirrelMovement;

    [SerializeField] private bool gliding;

    private float audioClipLength;

    private bool audioIsPlaying = false;

    public Animator animator;

    void FixedUpdate()
    {
        gliding = animator.GetBool("Glide");

        if (gliding)
        {
            if (!audioIsPlaying)
            {
                GetAudioClipLength(glidingAudio);
                Audio.instance.Play(glidingAudio);
                audioIsPlaying = true;
                Timer.Register(audioClipLength - 1f, () => audioIsPlaying = false);
            }
                
        }
    }

    void GetAudioClipLength(AudioClip clip)
    {
        audioClipLength = clip.length;
    } 
}
