using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SwordCollisionDetectionV2 : MonoBehaviour
{
    public WeaponControllerV2 wp;


    public GameObject cutPlane;

    public Vector3 cutPlaneSize;

    public LayerMask layerMask;

    public Collider[] hitGameobjects;

    public Material slicedMaterial;


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(cutPlane.transform.position, cutPlaneSize);
    }



    public void OnTriggerEnter(Collider other)
    {
        

        if (other.tag == "Enemy")
        {
            hitGameobjects = Physics.OverlapBox(cutPlane.transform.position, cutPlaneSize / 2, cutPlane.transform.rotation, layerMask);

            

            Debug.Log("Enemy Hit");

            for (int i = 0; i < hitGameobjects.Length; i++)
            {
                SlicedCounter slicedCounter;
                int thisObjectSlicedCounterInt = 0;
                hitGameobjects[i].gameObject.TryGetComponent<SlicedCounter>(out slicedCounter);
                if (slicedCounter == null || slicedCounter.counter < 2)
                {
                    thisObjectSlicedCounterInt = slicedCounter ? slicedCounter.counter : 0;
                    SlicedHull hull = hitGameobjects[i].gameObject.Slice(cutPlane.transform.position, cutPlane.transform.up, slicedMaterial);
                    if (hull != null)
                    {
                        GameObject bottom = hull.CreateLowerHull(hitGameobjects[i].gameObject, slicedMaterial);
                        MeshCollider tempMeshCol = bottom.AddComponent<MeshCollider>();
                        tempMeshCol.convex = true;
                        Rigidbody tempRB = bottom.AddComponent<Rigidbody>();
                        SlicedCounter bottomSlicedCounter = bottom.AddComponent<SlicedCounter>();
                        bottomSlicedCounter.IncrementCounter(thisObjectSlicedCounterInt);
                        bottom.gameObject.layer = LayerMask.NameToLayer("awawa");
                        bottom.gameObject.tag = "Enemy";
                        tempRB.AddExplosionForce(20, cutPlane.transform.position, 15);
                        

                        GameObject top = hull.CreateUpperHull(hitGameobjects[i].gameObject, slicedMaterial);

                        MeshCollider tempMeshCol2 = top.AddComponent<MeshCollider>();
                        tempMeshCol2.convex = true;
                        Rigidbody tempRB2 = top.AddComponent<Rigidbody>();
                        SlicedCounter topSlicedCounter = top.AddComponent<SlicedCounter>();
                        topSlicedCounter.IncrementCounter(thisObjectSlicedCounterInt);
                        top.gameObject.layer = LayerMask.NameToLayer("awawa");
                        top.gameObject.tag = "Enemy";
                        tempRB2.AddExplosionForce(200, cutPlane.transform.position, 15);
                        
                        Destroy(hitGameobjects[i].gameObject);

                    }
                }

                
            }
            cutPlane.transform.Rotate(Vector3.forward * 180, Space.Self);

        }
    }
    




}
