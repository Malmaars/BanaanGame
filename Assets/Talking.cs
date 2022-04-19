using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Talking : MonoBehaviour
{

    public TextMeshProUGUI textToBeEdited;
    public TextMeshProUGUI whosTalking;
    public int currentConversation;
    public int currentLine;

    public bool noOneTalking;
    [Serializable]
    public class PossibleLines
    {
        public int speaker;
        public string line;
    }

    [Serializable]
    public class Conversation
    {
        public PossibleLines[] oneConversation;
    }
    public Conversation[] DifferentLines;

    // Update is called once per frame
    void Update()
    {
        if (noOneTalking)
        {
            whosTalking.text = " ";
            textToBeEdited.text = " ";
        }

        else
        {
            if (DifferentLines[currentConversation].oneConversation[currentLine].speaker == 0)
            {
                whosTalking.text = "You";
                textToBeEdited.color = new Color(0.4946155f, 0.8962264f, 0.5157735f);
            }

            if (DifferentLines[currentConversation].oneConversation[currentLine].speaker == 1)
            {
                whosTalking.text = "Instructor";
                textToBeEdited.color = new Color(0.8980392f, 0.6814728f, 0.4941176f);
            }
        }
    }
}
