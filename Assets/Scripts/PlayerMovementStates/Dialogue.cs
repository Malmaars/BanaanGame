using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{

    //receive an array of strings for the dialogue
    //and play them after each other. In line with the voice clips.

    bool dialogueIsPlaying;
    string[] currentdialogue;
    int dialogueCounter;
    //array of fmod data, to get the audio files and information

    bool CreateDialogue(string[] dialogue)
    {
        if (dialogueIsPlaying)
        {
            return false;
        }

        else
        {
            dialogueIsPlaying = true;
            currentdialogue = dialogue;
            dialogueCounter = 0;
            return true;
        }
    }

    //update this every frame.
    void PlayDialogue()
    {

    }
}
