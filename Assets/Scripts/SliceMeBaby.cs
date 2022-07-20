using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SliceMeBaby : MonoBehaviour
{
    public GameObject cutPlane;

    public Vector3 cutPlaneSize;

    public LayerMask layerMask;

    public Collider[] hitGameobjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OnSlicerInput.instance.onSlice) {
            hitGameobjects = Physics.OverlapBox(cutPlane.transform.position, cutPlaneSize/2, cutPlane.transform.rotation, layerMask);

            for (int i = 0; i < hitGameobjects.Length; i++) {
                SlicedHull hull = hitGameobjects[i].gameObject.Slice(cutPlane.transform.position, cutPlane.transform.up, null);
                if (hull != null) {

                    GameObject bottom = hull.CreateLowerHull(hitGameobjects[i].gameObject, null);
                    MeshCollider tempMeshCol = bottom.AddComponent<MeshCollider>();
                    tempMeshCol.convex = true;
                    Rigidbody tempRB = bottom.AddComponent<Rigidbody>();
                    tempRB.AddExplosionForce(20, cutPlane.transform.position, 15);

                    GameObject top = hull.CreateUpperHull(hitGameobjects[i].gameObject, null);
                    
                    MeshCollider tempMeshCol2 = top.AddComponent<MeshCollider>();
                    tempMeshCol2.convex = true;
                    Rigidbody tempRB2 = top.AddComponent<Rigidbody>();
                    tempRB2.AddExplosionForce(200, cutPlane.transform.position, 15);
                    Destroy(hitGameobjects[i].gameObject);
                }
            }
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(cutPlane.transform.position, cutPlaneSize);
    }
}
