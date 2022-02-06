using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMill : MonoBehaviour
{
    [SerializeField] float chopTimer = 10f;
    [SerializeField] int woodPertree = 10;
    float timer = 0;
    bool isChopping = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isChopping && other.CompareTag("Tree"))
        {
            isChopping = true;
            timer = 0;
            Destroy(other.gameObject);
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (!isChopping && other.CompareTag("Tree"))
        {
            isChopping = true;
            timer = 0;
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if (isChopping)
        {
            timer += Time.deltaTime;
            if(timer >= chopTimer)
            {
                PlayerManager.Instance.Wood += woodPertree;
                isChopping = false;
            }
        }
    }
}
