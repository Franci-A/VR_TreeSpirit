using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    ControllerManager controllerManager;

    private void Start()
    {
        controllerManager = ControllerManager.Instance;
    }

    public void ChangeToBuilding()
    {
        Debug.Log("building");
        controllerManager.leftController.CurrentState = ControllerState.BUILDING;
        controllerManager.rightController.CurrentState = ControllerState.BUILDING;
    }
    
    public void ChangeToIdle()
    {
        Debug.Log("idling");
        controllerManager.leftController.CurrentState = ControllerState.IDLE;
        controllerManager.rightController.CurrentState = ControllerState.IDLE;
    }
}
