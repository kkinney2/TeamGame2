using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject ThrowingDogo;
    public GameObject AIDogo;

    [Header("(On) Camera")]
    public GameObject CameraObj;
    public GameObject Reticle;
    public GameObject Probe;

    [Header("Object References")]
    public GameObject Ref_PickUpPos;
    public GameObject Ref_CamDefault;
    public GameObject UI_Toggle;

    public float throwPower;

    [Header("Default Camera Settings")]
    public Vector3 defOffset = new Vector3(0, 0, 0);
    public float defCamDist = 10f;

    [Header("Aiming Camera Settings")]
    public Vector3 aimingOffset = new Vector3(2, 0, 0);
    public float camAimingDist = 7f;

    [Header("Additional Settings")]
    public bool LerpCamera = false;
    public Vector3 AdditionalThrowHeight;
    public float JumpHeight;

    bool isHoldingObj = false;
    StateMachine stateMachine;
    StateMachine animMachine;
    Animator animator;
    ThirdPersonCamera camController;
    CharacterController charController;
    [HideInInspector]
    public ProbeBehavior probeController;
    PickupBehavior pickupBehaviour;
    DogBehavior dogBehaviour;
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

        camController.SetTarget(Ref_CamDefault.transform);
        camController.SetTargetCameraPos(defCamDist, defOffset);
        //camController.camTargetRef = Ref_CamDefault.transform;
        //camController.camAimingTargetRef = Ref_CamAiming.transform;

        probeController = Probe.GetComponent<ProbeBehavior>();
        charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        dogBehaviour = AIDogo.GetComponent<DogBehavior>();
        pickupBehaviour = ThrowingDogo.GetComponent<PickupBehavior>();


        // Give stateMachine a state so that coroutine StateSwitch has something
        // to compare with
        stateMachine.ChangeState(new DefaultPlayer(this, animator));

        StartCoroutine(StateSwitch(stateMachine));
        StartCoroutine(DogSwitcher());

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

        animator.SetBool("shouldPickup", false);

        if (probeController.GetObj() != null && !IsHoldingObj())
        {
            if (!UI_Toggle.gameObject.activeSelf)
            {
                UI_Toggle.gameObject.SetActive(true);
                Debug.Log("Pickup UI turned on");
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetBool("shouldPickup", true);
                heldObj = probeController.GetObj();

                if (heldObj.CompareTag("Borko"))
                {
                    heldObj = ThrowingDogo;
                }

                heldObj.transform.SetParent(gameObject.transform);
                heldObj.GetComponent<PickupBehavior>().ToggleBeingHeld();
                isHoldingObj = true;

            }
        }

        // Makes sure Pickup UI is turned off when there
        // is no object to pickup
        if (probeController.GetObj() == null || IsHoldingObj())
        {
            if (UI_Toggle.gameObject.activeSelf)
            {
                UI_Toggle.gameObject.SetActive(false);
                Debug.Log("Pickup UI turned off");
            }
            
        }

        if (IsHoldingObj())
        {
            heldObj.transform.position = Ref_PickUpPos.transform.position;
            heldObj.transform.rotation = Ref_PickUpPos.transform.rotation;
            heldObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            animator.SetBool("isHolding", true);
            
        }
        else
        {
            animator.SetBool("isHolding", false);
        }

        // **----------------------------------------------------------------------------->>
    }

    IEnumerator DogSwitcher()
    {
        int t = 0;   
            
        while (true)
        {
            if (isHoldingObj)
            {
                dogBehaviour.Warp(Vector3.zero);
                AIDogo.SetActive(false);
            }

            if ( pickupBehaviour.IsGrounded() && t > 3 && !AIDogo.activeSelf )
            {
                //dogBehaviour.Warp(ThrowingDogo.transform.position);
                AIDogo.transform.position = ThrowingDogo.transform.position;
                AIDogo.transform.rotation = ThrowingDogo.transform.rotation;

                AIDogo.SetActive(true);
                ThrowingDogo.transform.position = Vector3.zero;
                ThrowingDogo.GetComponent<Rigidbody>().velocity = Vector3.zero;
                t = 0;
            }

            yield return new WaitForSeconds(1);
            t++;
        }

        yield return new WaitForEndOfFrame();
    }

    IEnumerator StateSwitch(StateMachine stateMachine)
    {
        for(;;)
        {
            string currentState = stateMachine.getCurrentState().ToString();

            // <<-----------------------------------------------------------------------------**
            // Enter "Aiming" state on right click press
            // Else: Exit "Aiming" and Enter "DefaultPlayer"
            // Bool Controls if Camera Lerps Positions between states
            // **---------------------------------------------**

            if (Input.GetMouseButton(1) == true)
            {
                //Debug.Log("StateMachine: Right Mouse Pressed");
                if ( currentState != "Aiming")
                {
                    // TODO: camController.SetCamTarget(Ref_CamAiming.transform.position);
                    if (LerpCamera)
                    {
                        camController.SetTargetCameraPos(camAimingDist, aimingOffset);
                    }
                    else camController.SetPos(camAimingDist, aimingOffset);

                    stateMachine.ChangeState(new Aiming(this, animator));
                    animator.SetBool("isAiming", true);
                }
            }

            if (Input.GetMouseButton(1) == false)
            {
                if (currentState != "DefaultPlayer")
                {
                    // TODO: camController.SetCamTarget(Ref_CamDefault.transform.position);

                    if (LerpCamera)
                    {
                        camController.SetTargetCameraPos(defCamDist, defOffset);
                    }
                    else camController.SetPos(defCamDist, defOffset);

                    stateMachine.ChangeState(new DefaultPlayer(this, animator));
                    animator.SetBool("isAiming", false);

                    //Debug.Log("StateMachine: Right Mouse NOT Pressed");
                }
            }

            // **----------------------------------------------------------------------------->>

            yield return new WaitForSeconds(.1f);
        }
    }

    public void ToggleReticle()
    {
        Reticle.gameObject.GetComponent<LerpMatAlpha>().ToggleVisible();
    }

    public void IsHoldingObj(bool newState)
    {
        isHoldingObj = newState;
    }

    public bool IsHoldingObj()
    {
        return isHoldingObj == true ? true : false;
    }

    public GameObject GetHeldObj()
    {
        return heldObj;
    }
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

