using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FrogMovement : MovementState
{
    CinemachineFreeLook playerCam;
    float myCameraFov = 40;

    List<ParticleSystem> particleLevels;

    float frogJumpCharge;
    float frogJumpFollowTimer = 1;
    float frogJumpTimerMax = 0.5f;

    public FrogMovement(Transform playerTransform, Transform playerObject, Animator playerAnimator, Transform groundCheckTransform, Vector3 velocity, CinemachineFreeLook playerCam, List<ParticleSystem> particleLevels) : base(playerTransform, playerObject, playerAnimator, groundCheckTransform, velocity)
    {
        this.playerCam = playerCam;
        this.particleLevels = particleLevels;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Input.GetKey(KeyCode.Space) && isGrounded && frogJumpFollowTimer >= frogJumpTimerMax)
        {
            ChargeFrogJump();
        }
        else
        {
            playerAnimator.SetBool("Charging", false);
            particleLevels[0].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            particleLevels[1].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            particleLevels[2].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        if (isGrounded && frogJumpFollowTimer < frogJumpTimerMax)
        {
            frogJumpFollowTimer += Time.deltaTime;
            //Debug.Log(frogJumpFollowTimer);

            if (frogJumpFollowTimer >= frogJumpTimerMax)
                frogJumpCharge = 0;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!isGrounded)
        {
            playerCam.m_Lens.FieldOfView = Mathf.Lerp(playerCam.m_Lens.FieldOfView, myCameraFov, Time.deltaTime * 10);
        }

        if (isGrounded && velocity.y <= 0)
        {
            playerAnimator.SetBool("FrogJump", false);
        }
    }

    public override void Jump()
    {
        if (frogJumpCharge > 0.5f || frogJumpFollowTimer < frogJumpTimerMax)
        {
            FrogJump();
        }
        else
        {
            base.Jump();
            frogJumpCharge = 0;
        }
    }

    void ChargeFrogJump()
    {
        frogJumpCharge += Time.deltaTime;
        //Debug.Log(frogJumpCharge);
        if (frogJumpCharge > 0f)
        {
            playerAnimator.SetBool("Charging", true);
            if (!particleLevels[0].isPlaying)
                particleLevels[0].Play();
        }

        if (frogJumpCharge > 1.5)
        {
            if (!particleLevels[1].isPlaying)
                particleLevels[1].Play();
        }

        if (frogJumpCharge > 3)
        {
            frogJumpCharge = 3;
            if (!particleLevels[2].isPlaying)
                particleLevels[2].Play();
        }

        playerCam.m_Lens.FieldOfView = myCameraFov - 20 / 3 * frogJumpCharge;
    }

    void FrogJump()
    {
        //I want to be able to follow up without charging
        //Just hit jump again when you land for a giving time frame
        Debug.Log(frogJumpCharge);
        velocity = playerObject.forward * Time.fixedDeltaTime * 300 * frogJumpCharge;
        velocity.y = -gravity * Time.fixedDeltaTime * 55 * frogJumpCharge;

        playerAnimator.SetBool("FrogJump", true);
        playerAnimator.SetBool("Charging", false);
        playerAnimator.SetBool("Jump", true);
        frogJumpFollowTimer = 0;
    }
}
