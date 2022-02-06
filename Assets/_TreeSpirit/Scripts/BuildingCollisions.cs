using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCollisions : MonoBehaviour
{
    public bool CanBePlaced { get; set; } = true;
    [SerializeField] Material redMat;
    [SerializeField] Material blueMat;
    [SerializeField] Material normalMat;
    MeshRenderer mr;
    public bool isPlaced = false;

    public string buildingName;
    [SerializeField] BuildingData buildingData;
    public BuildingData.BuildingInfo info;

    int buildsurface = 0;
    int otherObjects = 0;

    private void Awake()
    {
        mr = GetComponentInChildren<MeshRenderer>();
        info = buildingData.GetByName(buildingName);
    }

    public void Init()
    {
        SetMat(blueMat);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPlaced && (other.CompareTag("House") ||
                          (other.CompareTag("BuildArea") && other.GetComponent<BuildingCollisions>().info.selfSurface != info.buildingSurface)))
        {
            otherObjects++;
            if (otherObjects > 0)
            {
                CanBePlaced = false;
                SetMat(redMat);
            }
        }else if (!isPlaced && other.CompareTag("BuildArea"))
        {
            buildsurface++;
            if(buildsurface > 1)
            {
                CanBePlaced = false;
                SetMat(redMat);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (!isPlaced && (other.CompareTag("House") ||
                          (other.CompareTag("BuildArea") && other.GetComponent<BuildingCollisions>().info.selfSurface != info.buildingSurface)))
        {
            otherObjects--;
        }
        else if (!isPlaced && other.CompareTag("BuildArea"))
        {
            buildsurface--;
        }

        if (otherObjects == 0 && buildsurface == 1)
        {
            CanBePlaced = true;
            SetMat(blueMat);
        }

        /*if (!isPlaced &&
            (other.CompareTag("House") || (other.CompareTag("BuildArea") && info.buildingSurface != other.GetComponent<BuildingCollisions>().info.selfSurface)))
        {
            CanBePlaced = true;
            SetMat(blueMat);
        }*/
    }

/*    private void OnTriggerStay(Collider other)
    {
        if (!isPlaced &&
            (other.CompareTag("House") || (other.CompareTag("BuildArea") && info.buildingSurface != other.GetComponent<BuildingCollisions>().info.selfSurface)))
        {
            CanBePlaced = false;
            SetMat(redMat);
        }
    }
*/
    public void SetMat(Material mat = null)
    {
        if(mat == null)
        {
            mat = normalMat;
        }
        mr.material = mat;
    }
}
