using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public abstract class Holdable : MonoBehaviour
{
    bool canPickUp;
    bool isPickedUp;
    bool rightController;
    public Rigidbody rb;
    Quaternion rotationOffset;
    Vector3 positionOffset;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (canPickUp)
        {
            if (!isPickedUp && 
                ((!rightController && VRInputManager.Instance.GripInput(Hand.LEFT) && ControllerManager.Instance.leftController.CurrentState == ControllerState.IDLE) ||
                (rightController && VRInputManager.Instance.GripInput(Hand.RIGHT) && ControllerManager.Instance.rightController.CurrentState == ControllerState.IDLE)))
            {
                PickUp();
            }
            else if (isPickedUp && ((!rightController && VRInputManager.Instance.GripInput(Hand.LEFT)) || 
                (rightController && VRInputManager.Instance.GripInput(Hand.RIGHT))))
            {
                FollowPosition();

            }
            else if (isPickedUp) { 
                Drop();
            }

        }
    }

    private void PickUp()
    {
        isPickedUp = true;

        if (rightController)
        {
            ControllerManager.Instance.rightController.CurrentState = ControllerState.HOLDING;
            //positionOffset = transform.position - VRInputManager.Instance.ControllerPosition(Hand.RIGHT);
            //rotationOffset = Quaternion.FromToRotation( transform.forward, VRInputManager.Instance.ControllerRotation(Hand.RIGHT) * Vector3.forward);

        }
        else
        {
            ControllerManager.Instance.leftController.CurrentState = ControllerState.HOLDING;
            //positionOffset = transform.position - VRInputManager.Instance.ControllerPosition(Hand.LEFT);
            //rotationOffset = Quaternion.FromToRotation(transform.forward, VRInputManager.Instance.ControllerRotation(Hand.LEFT) * Vector3.forward);
        }
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        
    }

    private void Drop()
    {
        isPickedUp = false;
        rb.useGravity = true;
        if (rightController)
        {
            ControllerManager.Instance.rightController.CurrentState = ControllerState.IDLE;
            rb.velocity = VRInputManager.Instance.ControllerVelocity(Hand.RIGHT);
            rb.angularVelocity = VRInputManager.Instance.ControllerAngularVelocity(Hand.RIGHT) ;
            //rb.AddForce(VRInputManager.Instance.ControllerVelocity(Hand.RIGHT));
        }
        else
        {
            ControllerManager.Instance.leftController.CurrentState = ControllerState.IDLE;
            rb.velocity = VRInputManager.Instance.ControllerVelocity(Hand.LEFT) ;
            rb.angularVelocity = VRInputManager.Instance.ControllerAngularVelocity(Hand.LEFT);
            // rb.AddForce(VRInputManager.Instance.ControllerVelocity(Hand.LEFT));
        }
        
        // rb.velocity = Vector3.zero;
    }

    private void FollowPosition()
    {
        if (rightController)
        {
            transform.position = VRInputManager.Instance.ControllerPosition(Hand.RIGHT) + positionOffset;
            transform.rotation = VRInputManager.Instance.ControllerRotation(Hand.RIGHT)/* * rotationOffset*/;
        }
        else
        {
            transform.position = VRInputManager.Instance.ControllerPosition(Hand.LEFT) + positionOffset;
            transform.rotation = VRInputManager.Instance.ControllerRotation(Hand.LEFT)/* * rotationOffset*/;
        }

    }

    public virtual void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("LeftController") && !canPickUp)
        {
            rightController = false;
            canPickUp = true;

        }else if (other.gameObject.CompareTag("RightController") && !canPickUp)
        {
            rightController = true;
            canPickUp = true;

        }

    }

    public virtual void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.CompareTag("RightController") || other.gameObject.CompareTag("LeftController")) && !isPickedUp)
        {

            canPickUp = false;
        }
    }

    private void OnDestroy()
    {
        if (isPickedUp)
        {
            Drop();
        }
    }
}
