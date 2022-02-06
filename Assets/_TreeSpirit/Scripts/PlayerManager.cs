using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get { return _instance; } }
    private static PlayerManager _instance;

    public int Wood { get; set; }

    private void Awake()
    {
        _instance = this;
        Wood += 100;
    }

    public void AddResource(int amount) => Wood += amount;
    public void UseResource(int amount) => Wood -= amount;

    public bool HasResource(int amount) => amount <= Wood;
}
