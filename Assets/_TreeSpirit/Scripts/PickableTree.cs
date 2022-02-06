using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableTree : Holdable
{
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag("Saw"))
        {
            //chop up wood
            //destroy gameobject
            //add wood to inventory/ resources
        }
    }
}
