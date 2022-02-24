using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateMachine
{
    public MovementState CurrentState { get; private set; }

    public void Initialize(MovementState startingState)
    {
        CurrentState = startingState;
        startingState.Enter(Vector3.zero);
    }

    public void ChangeState(MovementState newState)
    {
        Vector3 velocityTransition = CurrentState.Exit();

        CurrentState = newState;
        newState.Enter(velocityTransition);
    }
}
