using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerMovement player;

    [Header("Non Customizable Player values")]
    public Transform playerP;
    public Transform playerO;
    public Transform groundCheck;
    public CinemachineFreeLook playerCam;
    public List<ParticleSystem> frogParticleSystems;

    //[Header("Customizable Player Values")]


    //List<GameObject> swingAbleObjects;
    // Start is called before the first frame update
    void Start()
    {
        player = new PlayerMovement(playerP,playerO, groundCheck, playerCam, frogParticleSystems);
        player.Initialize();
        ObjectStats[] allObjects = FindObjectsOfType<ObjectStats>();

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
    }
}
