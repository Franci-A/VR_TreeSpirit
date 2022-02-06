using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum Hand
{
    LEFT,
    RIGHT,
    ANY
}

public class VRInputManager : MonoBehaviour
{
    #region Singleton
    private static VRInputManager _instance;
    public static VRInputManager Instance { get => _instance; }
    #endregion


    private List<InputDevice> leftController;
    private List<InputDevice> rightController;


    private void Awake()
    {
        if (Instance == null) _instance = this;
        else Destroy(this);
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        leftController = new List<InputDevice>();
        rightController = new List<InputDevice>();
        var desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftController);
        desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightController);
    }


    private List<List<InputDevice>> GetController(Hand handInput)
    {
        switch (handInput)
        {
            case Hand.LEFT:
                return new List<List<InputDevice>> { leftController };
            case Hand.RIGHT:
                return new List<List<InputDevice>> { rightController };
            default: // ANY
                return new List<List<InputDevice>> { leftController, rightController };
        }
    }

    #region Controller Input

    public Vector3 ControllerPosition(Hand handInput)
    {
        var devices = GetController(handInput);

        foreach(var device in devices)
        {
            device[0].TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
            return position;
        }
        return Vector3.zero;
    }
    
    public Quaternion ControllerRotation(Hand handInput)
    {
        var devices = GetController(handInput);

        foreach (var device in devices)
        {
            device[0].TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);
            return rotation;
        }
        return Quaternion.identity;
    }
    
    public Vector3 ControllerVelocity(Hand handInput)
    {
        var devices = GetController(handInput);

        foreach (var device in devices)
        {
            device[0].TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);
            return velocity;
        }
        return Vector3.zero;
    }
    
    public Vector3 ControllerAcceleration(Hand handInput)
    {
        var devices = GetController(handInput);

        foreach (var device in devices)
        {
            device[0].TryGetFeatureValue(CommonUsages.deviceAcceleration, out Vector3 acceleration);
            return acceleration;
        }
        return Vector3.zero;
    }
    
    public Vector3 ControllerAngularVelocity(Hand handInput)
    {
        var devices = GetController(handInput);

        foreach (var device in devices)
        {
            device[0].TryGetFeatureValue(CommonUsages.deviceAngularVelocity, out Vector3 velocity);
            return velocity;
        }
        return Vector3.zero;
    }
    
    public Vector3 ControllerAngularAcceleration(Hand handInput)
    {
        var devices = GetController(handInput);

        foreach (var device in devices)
        {
            device[0].TryGetFeatureValue(CommonUsages.deviceAngularAcceleration, out Vector3 acceleration);
            return acceleration;
        }
        return Vector3.zero;
    }


    public Vector2 JoystickAxis(Hand handInput)
    {
        var devices = GetController(handInput);

        foreach (var device in devices)
        {
            device[0].TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 input);
            return input;
        }
        return Vector2.zero;
    }

    public bool PrimaryButton(Hand handInput)
    {
        var devices = GetController(handInput);

        foreach (var device in devices)
        {
            device[0].TryGetFeatureValue(CommonUsages.primaryButton, out bool input);
            return input;
        }
        return false;
    }


    public bool SecondaryInput(Hand handInput)
    {
        var devices = GetController(handInput);

        foreach (var device in devices)
        {
            device[0].TryGetFeatureValue(CommonUsages.secondaryButton, out bool input);
            return input;
        }
        return false;
    }


    public bool TriggerInput(Hand handInput)
    {
        var devices = GetController(handInput);

        foreach (var device in devices)
        {
            device[0].TryGetFeatureValue(CommonUsages.triggerButton, out bool input);
            return input;
        }
        return false;
    }


    public bool GripInput(Hand handInput)
    {
        var devices = GetController(handInput);

        foreach (var device in devices)
        {
            device[0].TryGetFeatureValue(CommonUsages.gripButton, out bool input);
            return input;
        }
        return false;
    }
    #endregion
}
