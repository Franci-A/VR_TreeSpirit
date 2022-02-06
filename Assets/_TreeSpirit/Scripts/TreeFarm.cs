using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFarm : MonoBehaviour
{
    [SerializeField] float respawnTimer = 30;
    float timer = 0;
    [SerializeField] GameObject treePrefab;
    GameObject tree;
    Vector3 treeOriginalScale;
    bool hasTree = false;

    private void Start()
    {
        hasTree = false;
        timer = 0;
        tree = Instantiate(treePrefab, transform);
        tree.GetComponent<Collider>().enabled = false;
        tree.GetComponent<PickableTree>().enabled = false;
        tree.GetComponent<Rigidbody>().isKinematic = true;
        treeOriginalScale = tree.transform.localScale;
        tree.transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if(timer >= respawnTimer && !hasTree)
        {
            hasTree = true;
            tree.GetComponent<PickableTree>().enabled = true;
            tree.GetComponent<Collider>().enabled = true;


        }
        else if( timer < respawnTimer)
        {
            timer += Time.deltaTime;
            tree.transform.localScale = Vector3.Lerp(Vector3.zero, treeOriginalScale, timer / respawnTimer);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == tree)
        {
            tree.GetComponent<Rigidbody>().isKinematic = false;
            hasTree = false;
            timer = 0;
            tree = Instantiate(treePrefab, transform);
            tree.GetComponent<PickableTree>().enabled = false;
            tree.GetComponent<Collider>().enabled = false;
            tree.GetComponent<Rigidbody>().isKinematic = true;
            treeOriginalScale = tree.transform.localScale;
            tree.transform.localScale = Vector3.zero;
        }
    }
}
