using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllAudioClips : MonoBehaviour
{
    [SerializeField] public AudioClip alert;
    [SerializeField] public AudioClip birdsTrafficAmbience;
    [SerializeField] public AudioClip birdsTrainAmbience;
    [SerializeField] public AudioClip busyIndoorsAmbience;
    [SerializeField] public AudioClip buttonInteraction1;
    [SerializeField] public AudioClip buttonInteraction2;
    [SerializeField] public AudioClip flying;
    [SerializeField] public AudioClip frogAmbience1;
    [SerializeField] public AudioClip frogAmbience2;
    [SerializeField] public AudioClip frogAmbience3;
    [SerializeField] public AudioClip frogJumpCharge;
    [SerializeField] public AudioClip frogJumpHold;
    [SerializeField] public AudioClip frogJumpLandMed;
    [SerializeField] public AudioClip frogJump1;
    [SerializeField] public AudioClip frogJump2;
    [SerializeField] public AudioClip frogJump3;
    [SerializeField] public AudioClip interaction;
    [SerializeField] public AudioClip step;
    [SerializeField] public AudioClip walking;

    private bool scriptRunning = false;

    void FixedUpdate()
    {
        if (scriptRunning)
        {
            Audio.instance.Play(alert);
            Audio.instance.PlayWithRangeOnLoop(birdsTrafficAmbience);
            Audio.instance.PlayWithRangeOnLoop(birdsTrainAmbience);
            Audio.instance.PlayWithRangeOnLoop(busyIndoorsAmbience);
            Audio.instance.Play(buttonInteraction1);
            Audio.instance.Play(buttonInteraction2);
            Audio.instance.PlayOnLoop(flying);
            Audio.instance.PlayWithRangeOnLoop(frogAmbience1);
            Audio.instance.PlayWithRangeOnLoop(frogAmbience2);
            Audio.instance.PlayWithRangeOnLoop(frogAmbience3);
            Audio.instance.Play(frogJumpCharge);
            Audio.instance.PlayOnLoop(frogJumpHold);
            Audio.instance.PlayRandomPitch(frogJumpLandMed);
            Audio.instance.PlayRandomPitch(frogJump1);
            Audio.instance.PlayRandomPitch(frogJump2);
            Audio.instance.PlayRandomPitch(frogJump3);
            Audio.instance.Play(interaction);
            Audio.instance.PlayRandomPitch(step);
            Audio.instance.PlayOnLoop(walking);

        }
    }
}
