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


    [SerializeField] private FMODUnity.EventReference _boxBreakingSound;
    [SerializeField] private FMODUnity.EventReference _swingHit;




    private FMOD.Studio.EventInstance boxBreakingSound;
    private FMOD.Studio.EventInstance swingHit;

    private Vector3 boxLocation;

    //[SerializeField] private PlayerSounds playerSounds;



    void Start()
    {
        if (!_boxBreakingSound.IsNull)
        {
            boxBreakingSound = FMODUnity.RuntimeManager.CreateInstance(_boxBreakingSound);
        }

        if (!_swingHit.IsNull)
        {
            swingHit = FMODUnity.RuntimeManager.CreateInstance(_swingHit);
        }
    }





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

            //!boxLocation = other.gameObject.transform.position; //Rhys - Just trying to get the location of the box being sliced so I can attach a sound instance to it

            boxLocation = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
            


            for (int i = 0; i < hitGameobjects.Length; i++)
            {
                if (hitGameobjects[i].TryGetComponent<DollLimbController>(out DollLimbController dollLimbController)) {
                    if (hitGameobjects[i].transform.root.TryGetComponent<YbotTestController2>(out YbotTestController2 ybotTestController2)){
                        if (ybotTestController2.staggered) {
                            dollLimbController.Sliced();
                        }else {
                            return;
                        }
                    }
                }   
                SlicedCounter slicedCounter;
                int thisObjectSlicedCounterInt = 0;
                hitGameobjects[i].gameObject.TryGetComponent<SlicedCounter>(out slicedCounter);

                if (slicedCounter == null || slicedCounter.counter < 4)
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



                        boxBreakingSound.setParameterByName("BoxBreak", 2.0f);
                        boxBreakingSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(boxLocation));
                        boxBreakingSound.start();

                        swingHit.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(boxLocation));
                        swingHit.start();

                        //playerSounds.PlaySwingHit();
                        
                        
                        

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
            this.gameObject.SetActive(false);
        }
    }
    




}
