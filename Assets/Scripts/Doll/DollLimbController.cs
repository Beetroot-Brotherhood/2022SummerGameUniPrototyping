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

    public void Sliced() {
        for (int i = 0; i < children.Count; i++) {
            if (children != null) {
                children[i].Sliced();
            }
        }

        if (this.gameObject.TryGetComponent<Collider>(out Collider collider)) {
            collider.enabled = true;
        }
        if (this.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb)){
            rb.useGravity = true;
            rb.isKinematic = false;
        }
        UnParent();
    }

    public void UnParent(){
        this.transform.parent = null;
    }
}
