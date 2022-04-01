using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTimer;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private AudioClip glidingAudio;
 
    [SerializeField] public SquirrelMovement squirrelMovement;

    [SerializeField] private bool gliding;

    [SerializeField] private bool jumping;

    [SerializeField] private bool grounded;

    [SerializeField] private bool charging;

    [SerializeField] private AudioClip frogJump;

    [SerializeField] private AudioClip chargingBuildup;
    [SerializeField] private AudioClip chargingLoop;

    private AudioSource audioSource;

    private float audioClipLength;

    private bool audioIsPlaying = false;

    private bool jumpAudioIsPlaying = false;

    private bool inChargingBuildup = false;
    private bool chargingFinished = false;

    public Animator animator;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        gliding = animator.GetBool("Glide");
        jumping = animator.GetBool("FrogJump");
        grounded = animator.GetBool("Grounded");
        charging = animator.GetBool("Charging");

        if (grounded)
        {
            jumpAudioIsPlaying = false;
        }

        if (gliding)
        {
            if (!audioIsPlaying)
            {
                GetAudioClipLength(glidingAudio);
                Audio.instance.WindPlayer(glidingAudio);
                audioIsPlaying = true;
                Timer.Register(audioClipLength - 1f, () => audioIsPlaying = false);
            }
        }

     /*   if (charging)
        {
            if (!audioIsPlaying)
            {
                GetAudioClipLength(chargingLoop);
                Audio.instance.WindPlayer(chargingLoop);
                audioIsPlaying = true;
                Timer.Register(audioClipLength, () => audioIsPlaying = false);
            }
        }
       */
     
        if (jumping)
        {
            if (charging)
            {
                Audio.instance.Play(frogJump);
                jumpAudioIsPlaying = true;
            }
            
        }
    }

    void GetAudioClipLength(AudioClip clip)
    {
        audioClipLength = clip.length;
    } 
}
