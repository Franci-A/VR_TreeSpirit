using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingInfo", menuName = "BuildingData")]
public class BuildingData : ScriptableObject
{
    [System.Serializable]
    public class BuildingInfo
    {
        public string name;
        public BuildingType type;
        public int cost;
        public BuildingSurface buildingSurface;
        public bool isSurface;
        public BuildingSurface selfSurface;
        public GameObject prefab;
    }

    [SerializeField] private List<BuildingInfo> allBuildingData;

    public List<BuildingInfo> GetAll() => allBuildingData;

    public BuildingInfo GetByName(string searchName)
    {
        foreach (var info in allBuildingData)
        {
            if (info.name == searchName)
                return info;
        }
        return null;
    }
}

public enum BuildingType
{
    MotherTree,
    Platform,
    House,
    Lumberjack,
    Pub
}

public enum BuildingSurface
{
    Tree,
    Platform
}