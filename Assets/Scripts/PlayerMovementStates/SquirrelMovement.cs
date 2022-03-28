using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelMovement : MovementState
{
    bool gliding;

    Vector3 savedVelocity;

    public SquirrelMovement(Transform playerTransform, Transform playerObject, Animator playerAnimator, Transform groundCheckTransform, Vector3 velocity) : base(playerTransform, playerObject, playerAnimator, groundCheckTransform, velocity)
    {

    }

    public override void LogicUpdate()
    {
        //groundCheckTransform.position = playerTransform.position - Vector3.up * 1.05f;
        //isGrounded = Physics.CheckSphere(groundCheckTransform.position, 0.03f);

        RaycastHit groundHit;
        Physics.Raycast(groundCheckTransform.position, Vector3.down, out groundHit, 1);
        if (groundHit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        Debug.Log(groundCheckTransform.position);
        Debug.Log(playerTransform.position);

        playerAnimator.SetBool("Grounded", isGrounded);

        if (Input.GetKey(KeyCode.Space) && !isGrounded)
        {
            gliding = true;
            Glide();
        }

        else
        {
            if (gliding)
            {
                velocity = savedVelocity;
                gliding = false;
            }
            MovePlayer();
        }
        playerAnimator.SetBool("Glide", gliding); 

        if (Input.GetKeyUp(KeyCode.Space) && velocity.y < 0 && isGrounded)
        {
            Jump();
        }

        playerAnimator.SetFloat("YVelocity", velocity.y);

        if (velocity.y < 0)
        {
            playerAnimator.SetBool("Jump", false);
        }


    }

    public override void PhysicsUpdate()
    {
        if (!gliding)
        {
            base.PhysicsUpdate();
        }

        else
        {
            //velocity *= verticalDrag * Time.fixedDeltaTime;
            //velocity = Vector3.Lerp(velocity, velocity * verticalDrag * Time.fixedDeltaTime, Time.fixedDeltaTime * 10);
            //velocity *= verticalDrag;
            //add movement forward

            //playerTransform.forward = velocity.normalized;
            Vector3 gravityVector = Vector3.zero;
            float newGravity = -5f;
            gravityVector.y += newGravity;

            if (Vector3.Magnitude(velocity * (1 - (Vector3.Dot(playerTransform.forward, Vector3.up) * Time.fixedDeltaTime))) < 100)
                velocity *= 1 - (Vector3.Dot(playerTransform.forward, Vector3.up) * Time.fixedDeltaTime);
            //velocity.y += gravity;
            float velocityMagnitude = Vector3.Magnitude(velocity);
            if(velocityMagnitude > 100)
            {
                velocityMagnitude = 100;
            }


            playerTransform.position += playerTransform.forward * velocityMagnitude * Time.fixedDeltaTime + gravityVector * Time.fixedDeltaTime;
            savedVelocity = playerTransform.forward * velocityMagnitude + gravityVector;
            //just add drag now


            //gravity shouldn't work like it usually does, we're not falling, we're gliding

            //dv = lastVelocity - velocity;
            //lastVelocity = velocity;
        }
        //Debug.Log(velocity);
    }
    public override void MovePlayer()
    {
        base.MovePlayer();
    }

    void Glide()
    {
        //move forward more, move down less. Move based on angle and momentum.
        //velocity = playerTransform.forward * 10;

        //Plane controls. Should make it an option to invert this as well

        //vertical input tilts the player up and down
        //x rotation maybe
        float xRotation = Input.GetAxis("Vertical") * Time.deltaTime * 40;


        ////horizontal rotation skewers them, like a plane
        float zRotation = Input.GetAxis("Horizontal") * Time.deltaTime * 200;


        playerTransform.Rotate(new Vector3(xRotation, 0, zRotation));
        //playerTransform.localRotation = Quaternion.Euler(xRotation, playerTransform.localRotation.eulerAngles.y, zRotation);

        //velocity = Quaternion.AngleAxis(Input.GetAxis("Vertical"), playerTransform.right) * velocity;

    }

    //void TestInput()
    //{
    //    float roll = Input.GetAxis("Horizontal");
    //    float tilt = Input.GetAxis("Vertical");

    //    float tip = Vector3.Magnitude(playerTransform.right + Vector3.up) - 1.414214f;

    //    if (Vector3.Magnitude(playerTransform.forward + velocity.normalized) < 1.4f)
    //        tilt += 0.3f;

    //    if (tilt != 0)
    //        playerTransform.Rotate(playerTransform.right, tilt * Time.fixedDeltaTime * 10, Space.World);
    //    if(roll != 0)
    //        playerTransform.Rotate(playerTransform.right, roll * Time.fixedDeltaTime * 10, Space.World);
    //    if (tip != 0)
    //        playerTransform.Rotate(Vector3.up, tip * Time.fixedDeltaTime * 15, Space.World);

    //    velocity.y += gravity * Time.fixedDeltaTime;

    //    Vector3 vertvel = velocity - Vector3.ProjectOnPlane(playerTransform.up, velocity);
    //    float fall = Vector3.Magnitude(vertvel);
    //    velocity -= vertvel * Time.fixedDeltaTime;
    //    velocity += fall * playerTransform.forward * Time.fixedDeltaTime * 10;

    //    Vector3 forwardDrag = velocity - Vector3.ProjectOnPlane(playerTransform.up, velocity);
    //    velocity += -forwardDrag * Vector3.Magnitude(forwardDrag) * Time.fixedDeltaTime;

    //    Vector3 sideDrag = velocity - Vector3.ProjectOnPlane(playerTransform.right, velocity);
    //    velocity += -sideDrag * Vector3.Magnitude(sideDrag) * Time.fixedDeltaTime;

    //    Debug.Log(velocity);

    //    playerTransform.position += velocity * Time.fixedDeltaTime * 10;
    //}
}
