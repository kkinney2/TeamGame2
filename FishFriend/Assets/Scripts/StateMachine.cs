using System.Collections;
using System.Collections.Generic;

// Interface structure sourced from:
// https://forum.unity.com/threads/c-proper-state-machine.380612/ (Unity Forum)
// https://github.com/libgdx/gdx-ai/wiki/State-Machine (Mat Buckland)

public interface IState
{
    void Enter();
    void Execute();
    void Exit();
}
public class StateMachine
{
    IState currentState;

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    // Returns Current State's class name
    public IState getCurrentState()
    {
        return currentState;
    }

    public bool isInState(IState state)
    {
        if (currentState == state)
        {
            return true;
        }
        else return false;
    }

    public void Update()
    {
        // <<-----------------------------------------------------------------------------**
        // Executes the current state's Execute()
        // **---------------------------------------------**

        if (currentState != null) currentState.Execute();

        // **----------------------------------------------------------------------------->>
    }
}

