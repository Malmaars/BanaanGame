using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Talking conversationScript;
    public PlayerMovement player;

    [Header("Non Customizable Player values")]
    public Transform playerP;
    public Transform playerO;
    public Transform groundCheck;
    public CinemachineFreeLook playerCam;
    public List<ParticleSystem> frogParticleSystems;

    public GameObject radialmenu;

    string currentSentence;
    int currentSentenceNumber;
    bool isConversationGoing;


    public float timeBetweenLetters;
    public float timeBetweenSentences;
    public float timeBetweenConversations;
    float dialogueTimer;
    float sentenceTimer;
    float conversationTimer;

    //[Header("Customizable Player Values")]


    //List<GameObject> swingAbleObjects;
    // Start is called before the first frame update
    void Start()
    {
        player = new PlayerMovement(playerP,playerO, groundCheck, playerCam, frogParticleSystems, radialmenu);
        player.Initialize();
        ObjectStats[] allObjects = FindObjectsOfType<ObjectStats>();
        conversationScript = FindObjectOfType<Talking>();

        InitiateConversation();

        //swingAbleObjects = new List<GameObject>();
        //foreach(ObjectStats stats in allObjects)
        //{
        //    if (stats.swingable)
        //    {
        //        swingAbleObjects.Add(stats.gameObject);
        //    }
        //}

        // player.UpdateVariables(swingAbleObjects);
    }

    // Update is called once per frame
    void Update()
    {
        player.InputUpdate();
    }

    private void FixedUpdate()
    {
        player.PhysicsUpdate();


        if(conversationTimer >= timeBetweenConversations)
        {
            isConversationGoing = true;
            InitiateConversation();
            conversationTimer = 0;
        }

        if (isConversationGoing)
        {
            if (sentenceTimer > 0)
            {
                sentenceTimer += Time.deltaTime;
                if (sentenceTimer > timeBetweenSentences)
                {
                    if (conversationScript.currentLine == conversationScript.DifferentLines[conversationScript.currentConversation].oneConversation.Length)
                    {
                        currentSentence = " ";
                    }

                    conversationScript.currentLine++;
                    InitiateConversation();
                    currentSentenceNumber = 0;
                    dialogueTimer = 0;
                    sentenceTimer = 0;
                }
            }

            if (dialogueTimer >= timeBetweenLetters && sentenceTimer == 0)
            {
                if (ReadOutText() == false)
                {

                    if (conversationScript.currentLine == conversationScript.DifferentLines[conversationScript.currentConversation].oneConversation.Length - 1)
                    {
                        //conversation came to an end, wait for the next one
                        isConversationGoing = false;
                    }

                    sentenceTimer += Time.deltaTime;
                }
                else
                {
                    dialogueTimer = 0;
                }
            }

            dialogueTimer += Time.deltaTime;
        }

        else
        {
            conversationTimer += Time.deltaTime;
        }
    }

    void InitiateConversation()
    {
        currentSentence = conversationScript.DifferentLines[conversationScript.currentConversation].oneConversation[conversationScript.currentLine].line;

        if(conversationScript.currentLine > conversationScript.DifferentLines[conversationScript.currentConversation].oneConversation.Length - 1)
        {
            conversationScript.currentConversation++;
            conversationScript.currentLine = 0;
        }
    }

    bool ReadOutText()
    {
        char[] fullSentence;
        fullSentence = currentSentence.ToCharArray();

        Debug.Log(currentSentenceNumber);
        char[] partOfTheSentence = new char[currentSentenceNumber + 1];
        for(int i = 0; i <= currentSentenceNumber; i++)
        {
            partOfTheSentence[i] = fullSentence[i];
        }

        string newText = new string(partOfTheSentence);
        Debug.Log(newText);
        conversationScript.textToBeEdited.text = newText;

        currentSentenceNumber++;

        if(currentSentenceNumber > partOfTheSentence.Length)
        {
            currentSentenceNumber = 0;
        }

        if (currentSentenceNumber >= fullSentence.Length)
        {
            return false;
        }

        return true;
    }
}
