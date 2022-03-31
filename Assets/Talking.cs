using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Talking : MonoBehaviour
{

    public TextMeshProUGUI textToBeEdited;
    public int currentConversation;
    public int currentLine;
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
