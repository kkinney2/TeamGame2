using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject cameraObj;

    bool isHoldingObj = false;
    StateMachine stateMachine;
    Animator animator;
    ThirdPersonCamera camController;

    // Use this for initialization
    void Start () {
        stateMachine = new StateMachine();
        camController = cameraObj.GetComponent<ThirdPersonCamera>();

        // Give stateMachine a state so that coroutine StateSwitch has something
        // to compare with
        stateMachine.ChangeState(new DefaultPlayer(this));

        StartCoroutine(StateSwitch(stateMachine));

        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        // <<-----------------------------------------------------------------------------**
        // Quits application on Unity defined "Cancel" key(s)
        // **-------------------------------------------------**

        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
        // **----------------------------------------------------------------------------->>



        // Performs current state's logic
        stateMachine.Update();

        // <<HERE AFTER>>
        // Performs overarching player logic

        // <<-----------------------------------------------------------------------------**
        // Rotates player to match camera rotation
        // **----------------------------------------**
        Quaternion rotation = gameObject.transform.rotation;
        rotation.eulerAngles = cameraObj.transform.rotation.eulerAngles;
        rotation.x = 0;
        rotation.z = 0;
        transform.rotation = rotation;
        // **----------------------------------------------------------------------------->>


        if (IsHoldingObj())
        {
            
        }
	}

    IEnumerator StateSwitch(StateMachine stateMachine)
    {
        for(;;)
        {
            string currentState = stateMachine.getCurrentState().ToString();
            // <<-----------------------------------------------------------------------------**
            // Enter "Aiming" state on right click press
            // Else: Exit "Aiming" and Enter "DefaultPlayer"
            // **---------------------------------------------**

            if (Input.GetMouseButton(1) == true)
            {
                if ( currentState != "Aiming")
                {
                    stateMachine.ChangeState(new Aiming(this));
                    camController.SetCameraPos(2, new Vector3(1, 1, 0));
                }
            }
            if (Input.GetMouseButton(1) == false && currentState != "DefaultPlayer")
            {
                stateMachine.ChangeState(new DefaultPlayer(this));
                camController.ResetCameraPos();
            }

            // **----------------------------------------------------------------------------->>

            yield return new WaitForSeconds(.1f);
        }
    }

    public bool IsHoldingObj()
    {
        return isHoldingObj == true ? true : false;
    }

    /*
    public void SetCameraPosition(float distToTarget, Vector3 newOffset)
    {
        camController.SetCameraPos(distToTarget, newOffset);
    }

    public void ResetCameraPosition()
    {
        camController.ResetCameraPos();
    }
    */
}

/*public class SampleState : IState
{
    OwnerClass owner;

    public SampleState(OwnerClass newOwner)
    {
        this.owner = newOwner;
    }

    public void Enter()
    {

    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}
*/

