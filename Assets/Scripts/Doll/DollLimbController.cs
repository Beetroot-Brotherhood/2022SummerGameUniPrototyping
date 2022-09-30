using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DollLimbController : MonoBehaviour
{
    public bool rootPart = false;
    public DollLimbController parent;

    public List<DollLimbController> children;

    [HideInInspector]
    public YbotTestController2 ybotTestController2;

    public void Sliced () {
        //TODO Remove DollLimbController from this.gameObject and add the SlicedCounter script to this.gameObject
        for (int i = 0; i < children.Count; i++) {
            if (children[i] != null) {
                children[i].Sliced();
            }
        }
        if (this.gameObject.TryGetComponent<Collider>(out Collider collider)) {
            collider.enabled = true;
            collider.isTrigger = false;
        }
        if (this.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb)){
            rb.useGravity = true;
            rb.isKinematic = false;
        }
        UnParent();
        Destroy(this);
    }

    public void UnParent(){
        
        this.transform.parent = null;
    }
}
