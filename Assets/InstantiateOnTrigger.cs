using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnTrigger : MonoBehaviour
{

    public GameObject instantiateGO;
    
    public Vector3 offset;

    public string triggerTag;

    public void OnTriggerEnter(Collider other) {
        if (triggerTag == other.tag) {

            Vector3 newOffset = new Vector3(Random.Range(-offset.x, offset.x), Random.Range(-offset.y, offset.y), Random.Range(-offset.z, offset.z));
            Instantiate(instantiateGO, (transform.position + Vector3.up) + newOffset, Quaternion.identity);
        }
    }
}
