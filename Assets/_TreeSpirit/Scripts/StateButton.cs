using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateButton : MonoBehaviour
{
    [SerializeField] PlayerMenu playerMenu;
    [SerializeField] ControllerState state;
    bool isActive = false;
    [SerializeField] List<StateButton> buttons;
    public UnityEvent changeState;
    private void Start()
    {
        foreach (StateButton item in buttons)
        {
            item.changeState.AddListener(OtherButtonActive);
        }
    }

    private void OtherButtonActive()
    {
        isActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isActive && other.CompareTag("RightController"))
        {
            if(state == ControllerState.BUILDING)
            {
                playerMenu.ChangeToBuilding();
                isActive = true;
            }else if (state == ControllerState.IDLE)
            {
                playerMenu.ChangeToIdle();
                isActive = true;
            }

            changeState.Invoke();
        }
    }
}
