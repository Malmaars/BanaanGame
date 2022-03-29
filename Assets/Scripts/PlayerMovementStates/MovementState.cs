using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementState
{
    protected Transform groundCheckTransform;
    protected bool isGrounded;

    protected float gravity = -19.62f;
    protected float moveSpeed = 30;

    protected Transform playerTransform;
    protected Transform playerObject;
    protected Animator playerAnimator;
    protected Vector3 velocity;

    Vector3 newPlayerPosition;

    public MovementState(Transform playerTransform, Transform playerObject, Animator playerAnimator, Transform groundCheckTransform, Vector3 velocity)
    {
        this.playerTransform = playerTransform;
        this.playerAnimator = playerAnimator;
        this.groundCheckTransform = groundCheckTransform;
        this.velocity = velocity;
        this.playerObject = playerObject;
    }
    public virtual void Enter(Vector3 currentVelocity)
    {
        velocity = currentVelocity;
    }

    public virtual void LogicUpdate()
    {
        //we have to change the way the ground is detected
        //a raycast won't detect a collider if it start from within said collider
        RaycastHit groundHit;
        Physics.Raycast(groundCheckTransform.position, Vector3.down, out groundHit, 1f);
        if (groundHit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }


        //isGrounded = Physics.CheckSphere(groundCheckTransform.position, 0.06f);

        playerAnimator.SetBool("Grounded", isGrounded);

        //if (isGrounded)
        //{
        //    gravity = -2f;
        //}

        //else
        //{
        //    gravity = -19.62f;
        //}

        MovePlayer();

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

    public virtual void PhysicsUpdate()
    {
        //groundCheckTransform.position = playerTransform.position - Vector3.up * 1.05f;
        playerTransform.localRotation = Quaternion.Euler(0, playerTransform.localRotation.eulerAngles.y, 0);
        if (!isGrounded)
        {
            velocity.y += gravity * Time.fixedDeltaTime;
        }

        if (isGrounded && velocity.y <= 0)
        {
            Debug.Log("GROUNDED");
            velocity.x = 0;
            velocity.z = 0;
            velocity.y = gravity * Time.fixedDeltaTime * 10;
        }

        Vector3 moveDirection = (newPlayerPosition - playerTransform.position).normalized * Time.fixedDeltaTime * moveSpeed;
        playerTransform.position += velocity * Time.fixedDeltaTime + moveDirection;
    }

    public virtual void MovePlayer()
    {
        Vector3 forwardMoveDirection = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
        Vector3 sideMoveDirection = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;


        //float movementSpeed = moveSpeed;
        //if(frogJumpCharge > 0)
        //    movementSpeed /= 2;

        newPlayerPosition = playerTransform.position + ((forwardMoveDirection * Input.GetAxis("Vertical") + sideMoveDirection * Input.GetAxis("Horizontal")));
        newPlayerPosition = new Vector3(newPlayerPosition.x, playerTransform.position.y, newPlayerPosition.z);

        if ((forwardMoveDirection * Input.GetAxis("Vertical") + sideMoveDirection * Input.GetAxis("Horizontal")) != Vector3.zero)
            playerTransform.forward = (forwardMoveDirection * Input.GetAxis("Vertical") + sideMoveDirection * Input.GetAxis("Horizontal"));

        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            playerAnimator.SetBool("Running", true);
        }

        else
        {
            playerAnimator.SetBool("Running", false);
        }
    }

    public virtual void Jump()
    {
        velocity.y = -gravity * Time.fixedDeltaTime * 20;
    }

    public virtual void ResetVelocity()
    {
        velocity = Vector3.zero;
    }

    public virtual Vector3 Exit()
    {
        return velocity;
    }
}
