using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement
{
    //Rigidbody rb;

    Transform playerParent;
    Transform playerObject;
    Animator playerAnimator;
    CinemachineFreeLook myCamera;

    List<ParticleSystem> frogParticles;

    float myCameraFov;

    Transform groundCheckTransform;
    Vector3 velocity;

    bool isGrounded;

    MovementStateMachine stateMachine;
    FrogMovement frog;
    SquirrelMovement squirrel;



    public PlayerMovement(Transform playerparent, Transform playerobject, Transform groundChecker, CinemachineFreeLook myCam, List<ParticleSystem> frogParticles)
    {
        playerParent = playerparent;
        playerObject = playerobject;
        playerAnimator = playerobject.GetComponent<Animator>();
        groundCheckTransform = groundChecker;
        myCamera = myCam;
        this.frogParticles = frogParticles;
    }
    // Start is called before the first frame update
    public void Initialize()
    {
        //rb = playerParent.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        myCameraFov = myCamera.m_Lens.FieldOfView;

        stateMachine = new MovementStateMachine();
        frog = new FrogMovement(playerParent, playerObject, playerAnimator, groundCheckTransform, velocity, myCamera, frogParticles);
        squirrel = new SquirrelMovement(playerParent, playerObject, playerAnimator, groundCheckTransform, velocity);

        stateMachine.Initialize(frog);
    }

    // Update is called once per frame
    public void InputUpdate()
    {
        stateMachine.CurrentState.LogicUpdate();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //switch classes
            SwitchMovement();
        }
    }

    public void PhysicsUpdate()
    {
        //gravity
        if (!isGrounded)
        {
            myCamera.m_Lens.FieldOfView = Mathf.Lerp(myCamera.m_Lens.FieldOfView, myCameraFov, Time.deltaTime * 10);
        }

        stateMachine.CurrentState.PhysicsUpdate();
    }


    public void ResetVelocity()
    {
        stateMachine.CurrentState.ResetVelocity();
    }

    void SwitchMovement()
    {
        if (stateMachine.CurrentState == frog)
        {
            stateMachine.ChangeState(squirrel);
            return;
        }

        if (stateMachine.CurrentState == squirrel)
        {
            stateMachine.ChangeState(frog);
            return;
        }
    }

    //public void UpdateVariables(List<GameObject> swinga)
    //{
    //    swingables = swinga;
    //}

    //public void Swing()
    //{
    //    //Swinging is going to take a couple of steps, so I'm going to write them down

    //    //Check mouse input
    //    //connect arm to aimed location
    //    //set the location as a pivot point
    //    //add force to make the player swing
    //    //Let the player go when the let the mouse go (maybe add some extra force here)

    //        //raycast to check for swingable objects
    //        //I want to search in the area where a player aims, not the pixel perfect location
    //        RaycastHit hit;
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        Physics.Raycast(ray, out hit);
    //        if (hit.collider != null && swingables.Contains(hit.collider.gameObject))
    //        {
    //            Debug.Log("we can swing");
    //        }

    //}
}
