using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get { return _instance; } }
    private static BuildManager _instance;

    [SerializeField] BuildingData buildingData;
    private BuildingData.BuildingInfo currentInfo = null;
    BuildingCollisions currentBuilding;

    public bool IsBuilding;
    Hand buildingHand;

    float timer;
    [SerializeField] float inputCooldown = 1;

    float currentRotation;


    private void Start()
    {
        _instance = this;
        SetBuildingInfo("Platform");
    }
    private void Update()
    {
        if (timer < inputCooldown)
        {
            timer += Time.deltaTime;
        }
        else if(currentBuilding != null)
        {
            if (!VRInputManager.Instance.GripInput(buildingHand))
            {
                ResetBuilding();
            }

            if (VRInputManager.Instance.PrimaryButton(buildingHand) && currentBuilding.CanBePlaced)
            {
                PlaceBuilding();
                timer = 0;
            }
        }

        if (VRInputManager.Instance.SecondaryInput(Hand.LEFT))
        {
            SetBuildingInfo("Platform");
        }
        else if (VRInputManager.Instance.SecondaryInput(Hand.RIGHT))
        {
            SetBuildingInfo("House");
        }
    }

    public void SetBuildingInfo(string searchName)
    {
        var info = buildingData.GetByName(searchName);
        if (info == null)
            return;
        else
            currentInfo = info;
    }

    public void Building(Vector3 hitPoint, Vector3 upDirection, Hand hand)
    {
        if (timer > inputCooldown)
        {
            if (currentBuilding == null)
            {
                buildingHand = hand;
                currentBuilding = Instantiate(currentInfo.prefab).GetComponent<BuildingCollisions>();
                currentBuilding.Init();
                currentRotation = 0;
            }
            currentBuilding.transform.position = hitPoint;
            currentBuilding.transform.up = upDirection;

            currentRotation += VRInputManager.Instance.JoystickAxis(hand).x * 2;
            currentBuilding.transform.Rotate(0, currentRotation, 0, Space.Self);
        }
    }

    public void ResetBuilding()
    {
        Destroy(currentBuilding?.gameObject);
        currentBuilding = null;
    }

    public void PlaceBuilding()
    {
        if (!PlayerManager.Instance.HasResource(currentInfo.cost))
        {
            Debug.Log("<color=red>" + currentInfo.cost + "</color> / " + PlayerManager.Instance.Wood);
            return;
        }

        currentBuilding.SetMat();
        currentBuilding.isPlaced = true;

        if (currentInfo.isSurface)
        {
            currentBuilding.gameObject.layer = 0;
        }

        PlayerManager.Instance.UseResource(currentInfo.cost);
        currentBuilding = null;
    }
}
