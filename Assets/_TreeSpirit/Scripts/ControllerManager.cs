using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
#region Singleton
    public static ControllerManager Instance { get { return _instance; } }
    private static ControllerManager _instance;
#endregion


#region Controller class
    public class Controller
    {
        private ControllerState _currentState;
        public ControllerState CurrentState
        {
            get { return _currentState; }
            set { _currentState = value; }
        }

        public Controller()
        {
            _currentState = ControllerState.IDLE;
        }

        public bool IsHolding { get => CurrentState == ControllerState.HOLDING; }
    }
#endregion

    public Controller rightController;
    public Controller leftController;

    [SerializeField] private LineRenderer lr;

    bool isBuilding = false;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(this);

        rightController = new Controller();
        leftController = new Controller();
    }

    public Controller Get(Hand hand) => (hand == Hand.LEFT) ? leftController : rightController;

    private void Update()
    {
        if (VRInputManager.Instance.GripInput(Hand.LEFT) && leftController.CurrentState != ControllerState.HOLDING)
        {
            CastRay(Hand.LEFT);
        }
        else if (VRInputManager.Instance.GripInput(Hand.RIGHT) && rightController.CurrentState != ControllerState.HOLDING)
        {
            CastRay(Hand.RIGHT);
        }
        else
        {
            lr.enabled = false;
        }
    }

    public void CastRay(Hand hand)
    {
        lr.enabled = true;
        Vector3 originPoint = VRInputManager.Instance.ControllerPosition(hand);
        Vector3 direction = VRInputManager.Instance.ControllerRotation(hand) * Vector3.forward;
        Vector3 endPoint = originPoint + direction * 10;
        Ray rayray = new Ray(originPoint, direction);


        int layerMask = ~LayerMask.GetMask("Controller", "Buildable");
        if (Physics.Raycast(rayray,out RaycastHit hit, 10,layerMask))
        {
            if ((hand == Hand.LEFT && leftController.CurrentState == ControllerState.IDLE) || (hand == Hand.RIGHT && rightController.CurrentState == ControllerState.IDLE))
            {
                if (hit.collider.CompareTag("Tree"))
                {
                    hit.collider.gameObject.transform.position = originPoint;
                }
            }
            else if (leftController.CurrentState == ControllerState.BUILDING)
            {
                if (hit.collider.CompareTag("BuildArea"))
                {
                    BuildManager.Instance.Building(hit.point, hit.normal, hand);
                    isBuilding = true;
                }
                else if (isBuilding)
                {
                    isBuilding = false;
                    BuildManager.Instance.ResetBuilding();
                }
            }
            endPoint = hit.point;
        }
        
        Debug.DrawRay(originPoint, direction * 10, Color.green, Time.deltaTime);
        lr.SetPositions(new Vector3[] {originPoint , endPoint});
    }
}

public enum ControllerState
{
    IDLE,
    HOLDING,
    BUILDING
}
