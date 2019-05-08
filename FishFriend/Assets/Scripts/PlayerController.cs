using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject CameraObj;
    public GameObject Reticle;
    public GameObject Probe;
    public GameObject PickUpPosRef;
    public GameObject UI_Toggle;

    bool isHoldingObj = false;
    StateMachine stateMachine;
    Animator animator;
    ThirdPersonCamera camController;
    CharacterController charController;
    ProbeBehavior probeController;
    GameObject heldObj;

    // Use this for initialization
    void Start () {

        stateMachine = new StateMachine();

        if (CameraObj != null)
        {
            camController = CameraObj.GetComponent<ThirdPersonCamera>();
        }
        else
        {
            camController = Camera.main.GetComponent<ThirdPersonCamera>();
        }

        
        probeController = Probe.GetComponent<ProbeBehavior>();
        charController = GetComponent<CharacterController>();


        // Give stateMachine a state so that coroutine StateSwitch has something
        // to compare with
        stateMachine.ChangeState(new DefaultPlayer(this));

        StartCoroutine(StateSwitch(stateMachine));

        Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void Update () {

        // Performs current state's logic
        stateMachine.Update();

        // <<HERE AFTER>>
        // Performs overarching player logic

        // <<-----------------------------------------------------------------------------**
        // Rotates player to match camera rotation
        // **----------------------------------------**

        if (!Input.GetMouseButton(2))
        {
            Quaternion rotation = gameObject.transform.rotation;
            rotation.eulerAngles = CameraObj.transform.rotation.eulerAngles;
            rotation.x = 0;
            rotation.z = 0;
            transform.rotation = rotation;
        }

        // **----------------------------------------------------------------------------->>


        // <<-----------------------------------------------------------------------------**
        // Pickup Section
        // **----------------------------------------**

        if (probeController.GetObj() != null && !IsHoldingObj())
        {
            if (!UI_Toggle.gameObject.activeSelf)
            {
                UI_Toggle.gameObject.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                heldObj = probeController.GetObj();
                isHoldingObj = true;
                UI_Toggle.gameObject.SetActive(false);
            }
        }

        // Makes sure Pickup UI is turned off when there
        // is no object to pickup
        if (probeController.GetObj() == null || IsHoldingObj())
        {
            UI_Toggle.gameObject.SetActive(false);
        }

        if (IsHoldingObj())
        {
            heldObj.transform.position = PickUpPosRef.transform.position;
        }

        // **----------------------------------------------------------------------------->>
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
                    camController.SetTargetCameraPos(2, new Vector3(0, 2, 0));
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

    public void ToggleReticle()
    {
        Reticle.gameObject.SetActive(!Reticle.gameObject.activeSelf);
    }

    public bool IsHoldingObj()
    {
        return isHoldingObj == true ? true : false;
    }

    public GameObject GetHeldObj()
    {
        return heldObj;
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

