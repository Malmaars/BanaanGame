using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement
{
    public float moveSpeed;
    public float jumpForce;
    //Rigidbody rb;

    Transform playerParent;
    Transform playerObject;
    BoxCollider playerCol;
    Animator playerAnimator;
    List<GameObject> swingables;
    CinemachineFreeLook myCamera;

    ParticleSystem chargingFrogP, frogLevelP, frogLastLevelP;

    float myCameraFov;

    Transform groundCheckTransform;
    Vector3 velocity;
    float gravity = -9.81f * 2;
    Vector3 collisionCheck;

    float frogJumpCharge;
    float frogJumpFollowTimer = 1;
    float frogJumpTimerMax = 0.5f;

    bool isGrounded;

    Vector3 newPlayerPosition;

    public PlayerMovement(Transform playerparent, Transform playerobject, Transform groundChecker, CinemachineFreeLook myCam, ParticleSystem charge, ParticleSystem pop, ParticleSystem popper, float speed, float jump)
    {
        playerParent = playerparent;
        playerObject = playerobject;
        playerAnimator = playerobject.GetComponent<Animator>();
        moveSpeed = speed;
        jumpForce = jump;
        groundCheckTransform = groundChecker;
        myCamera = myCam;
        chargingFrogP = charge;
        frogLevelP = pop;
        frogLastLevelP = popper;
    }
    // Start is called before the first frame update
    public void Initialize()
    {
        //rb = playerParent.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        myCameraFov = myCamera.m_Lens.FieldOfView;
        newPlayerPosition = playerParent.position;
        playerCol = playerParent.GetComponent<BoxCollider>();

        collisionCheck = playerParent.position;
    }

    public void UpdateVariables(List<GameObject> swinga)
    {
        swingables = swinga;
    }

    // Update is called once per frame
    public void InputUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheckTransform.position, 0.03f);
        playerAnimator.SetBool("Grounded", isGrounded);

        //move based on the camera and input values

        //PRONE TO BUGS. CHECK THIS IF PLAYER CAN OR CANT MOVE WHEN THEY SHOULD
        MovePlayer();

        if(Input.GetKey(KeyCode.Space) && isGrounded && frogJumpFollowTimer >= frogJumpTimerMax)
        {
            ChargeFrogJump();
        }
        else
        {
            playerAnimator.SetBool("Charging", false);
            chargingFrogP.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            frogLevelP.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            frogLastLevelP.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        if (Input.GetKeyUp(KeyCode.Space) && velocity.y < 0 && isGrounded)
        {
            Jump();
        }

        playerAnimator.SetFloat("YVelocity", velocity.y);

        if(velocity.y < 0)
        {
            playerAnimator.SetBool("Jump", false);
        }

        if (Input.GetMouseButton(0))
        {
            Swing();
        }


        if (isGrounded && frogJumpFollowTimer < frogJumpTimerMax)
        {
            frogJumpFollowTimer += Time.deltaTime;
            //Debug.Log(frogJumpFollowTimer);

            if (frogJumpFollowTimer >= frogJumpTimerMax)
                frogJumpCharge = 0;
        }

        //Debug.Log(velocity.x + ", " + velocity.y + ", " + velocity.z);
    }

    public void PhysicsUpdate()
    {
        //gravity
        if (!isGrounded)
        {
            velocity.y += gravity * Time.fixedDeltaTime;
            myCamera.m_Lens.FieldOfView = Mathf.Lerp(myCamera.m_Lens.FieldOfView, myCameraFov, Time.deltaTime * 10);
        }

        if (isGrounded && velocity.y <= 0)
        {
            playerAnimator.SetBool("FrogJump", false);
            velocity.x = 0;
            velocity.z = 0;
            velocity.y = gravity * Time.fixedDeltaTime * 10;
        }


        //replace newplayerPosition with velocity
        //playerParent.position = newPlayerPosition;
        //Solution was part editing the physics project settings

        //I need to make it so when the player collides with something, their velocity is hindered

        Vector3 moveDirection = (newPlayerPosition - playerParent.position).normalized * Time.fixedDeltaTime * moveSpeed;
        playerParent.position += velocity * Time.fixedDeltaTime + moveDirection;
    }

    void MovePlayer()
    {
        Vector3 forwardMoveDirection = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
        Vector3 sideMoveDirection = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;


        //float movementSpeed = moveSpeed;
        //if(frogJumpCharge > 0)
        //    movementSpeed /= 2;

        newPlayerPosition = playerParent.position + ((forwardMoveDirection * Input.GetAxis("Vertical") + sideMoveDirection * Input.GetAxis("Horizontal")));
        newPlayerPosition = new Vector3(newPlayerPosition.x, playerParent.position.y, newPlayerPosition.z);

        if ((forwardMoveDirection * Input.GetAxis("Vertical") + sideMoveDirection * Input.GetAxis("Horizontal")) != Vector3.zero)
            playerObject.forward = (forwardMoveDirection * Input.GetAxis("Vertical") + sideMoveDirection * Input.GetAxis("Horizontal"));

        if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            playerAnimator.SetBool("Running", true);
        }

        else
        {
            playerAnimator.SetBool("Running", false);
        }
    }

    void ChargeFrogJump()
    {
        frogJumpCharge += Time.deltaTime;
        //Debug.Log(frogJumpCharge);
        if(frogJumpCharge > 0.5f)
        {
            playerAnimator.SetBool("Charging", true);
            if (!chargingFrogP.isPlaying)
                chargingFrogP.Play();
        }

        if(frogJumpCharge > 1.5)
        {
            if (!frogLevelP.isPlaying)
                frogLevelP.Play();
        }

        if (frogJumpCharge > 3)
        {
            frogJumpCharge = 3;
            if (!frogLastLevelP.isPlaying)
                frogLastLevelP.Play();
        }

        myCamera.m_Lens.FieldOfView = myCameraFov - 20 / 3 * frogJumpCharge;
    }

    void Jump()
    {
        if (frogJumpCharge > 0.5f || frogJumpFollowTimer < frogJumpTimerMax)
        {
            FrogJump();
        }
        else
        {
            velocity.y = -gravity * Time.fixedDeltaTime * 20;
            frogJumpCharge = 0;
        }
    }

    void FrogJump()
    {
        //I want to be able to follow up without charging
        //Just hit jump again when you land for a giving time frame
        Debug.Log(frogJumpCharge);
        velocity = playerObject.forward * Time.fixedDeltaTime * 400 * frogJumpCharge;
        velocity.y = -gravity * Time.fixedDeltaTime * 40 * frogJumpCharge;

        playerAnimator.SetBool("FrogJump", true);
        playerAnimator.SetBool("Charging", false);
        playerAnimator.SetBool("Jump", true);
        frogJumpFollowTimer = 0;
    }

    public void Swing()
    {
        //Swinging is going to take a couple of steps, so I'm going to write them down

        //Check mouse input
        //connect arm to aimed location
        //set the location as a pivot point
        //add force to make the player swing
        //Let the player go when the let the mouse go (maybe add some extra force here)

            //raycast to check for swingable objects
            //I want to search in the area where a player aims, not the pixel perfect location
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            if (hit.collider != null && swingables.Contains(hit.collider.gameObject))
            {
                Debug.Log("we can swing");
            }
        
    }

    public void ResetVelocity()
    {
        velocity = Vector3.zero;
    }
}
