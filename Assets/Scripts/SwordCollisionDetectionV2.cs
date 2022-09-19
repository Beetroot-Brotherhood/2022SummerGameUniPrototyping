using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using Krezme;

public class SwordCollisionDetectionV2 : MonoBehaviour
{
    public CombatManager combatManager;

    public GameObject cutPlane;

    public Vector3 cutPlaneSize;

    public LayerMask layerMask;

    public Collider[] hitGameobjects;

    public Material slicedMaterial;

    [HideInInspector]
    public AttackState currentAttackState;

    [SerializeField] private FMODUnity.EventReference _boxBreakingSound;
    [SerializeField] private FMODUnity.EventReference _swingHit;
    
    private FMOD.Studio.EventInstance boxBreakingSound;
    private FMOD.Studio.EventInstance swingHit;

    private Vector3 boxLocation;

    //[SerializeField] private PlayerSounds playerSounds;

    void Start() {
        if (!_boxBreakingSound.IsNull) {
            boxBreakingSound = FMODUnity.RuntimeManager.CreateInstance(_boxBreakingSound);
        }

        if (!_swingHit.IsNull) {
            swingHit = FMODUnity.RuntimeManager.CreateInstance(_swingHit);
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(cutPlane.transform.position, cutPlaneSize);
    }

    YbotTestController2 ybotTestController2;
    DollLimbController dollLimbController;

    public void OnTriggerEnter(Collider other) {
        if (1<<other.gameObject.layer == layerMask) {
            hitGameobjects = Physics.OverlapBox(cutPlane.transform.position, cutPlaneSize / 2, cutPlane.transform.rotation, layerMask);
            //!boxLocation = other.gameObject.transform.position; //Rhys - Just trying to get the location of the box being sliced so I can attach a sound instance to it
            boxLocation = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
            // This needs a bool to indicate when an enemy is staggered so they can be sliced
            for (int i = 0; i < hitGameobjects.Length; i++)
            {
                if (other.tag == "Enemy") {
                    if (currentAttackState == AttackState.LightAttack) {
                        if (IsEnemyStaggered(i) == Bool3D.False) {
                            DealStaggerDamage(combatManager.currentStatistics.lightAttackDamage);
                        }
                        else if (IsEnemyStaggered(i) == Bool3D.Null){
                            SlicingEnemy(other, i);
                        }
                        else {
                            Debug.Log("C'mon dude");
                        }
                    }
                    else if (currentAttackState == AttackState.HeavyAttack) {
                        if (IsEnemyStaggered(i) == Bool3D.False) {
                            DealStaggerDamage(combatManager.currentStatistics.heavyAttackDamage);
                        }
                        else{
                            SlicingEnemy(other, i);
                        }
                    }
                }
                else if (other.tag == "Terrain") {
                    SlicingTerrain(other, i);
                }
            }
            cutPlane.transform.Rotate(Vector3.forward * 180, Space.Self);
            this.gameObject.SetActive(false);
        }
    }

#region DealingStaggerDamage
    private bool hasHitTheEnemy;
    void DealStaggerDamage(float damage) {
        EnemyStatisticsManager tempEnemyStatisticsManager = ybotTestController2.GetComponent<EnemyStatisticsManager>();
        for (int i = 0; i < combatManager.hitEnemies.Count; i++) {
            if (combatManager.hitEnemies[i] == tempEnemyStatisticsManager) {
                hasHitTheEnemy = true;
            }
        }
        if (!hasHitTheEnemy) {
            tempEnemyStatisticsManager.IncreaseStagger(damage);
            combatManager.hitEnemies.Add(tempEnemyStatisticsManager);
        }else {
            hasHitTheEnemy = false;
        }
    }
#endregion

//? The Slicing Functionality is very repeated and separated for easier prototyping
#region Slicing Functionality
    Bool3D IsEnemyStaggered(int i) {
        if (hitGameobjects[i].TryGetComponent<DollLimbController>(out dollLimbController)) {
            if (hitGameobjects[i].transform.root.TryGetComponent<YbotTestController2>(out ybotTestController2)){
                if (ybotTestController2.staggered) {
                    return Bool3D.True;
                }else {
                    return Bool3D.False;
                }
            }   
        }
        return Bool3D.Null;
    }

    /// <summary>
    /// Totally separated enemy slicing functionality
    /// </summary>
    /// <param name="other"></param>
    /// <param name="i"></param>
    void SlicingEnemy(Collider other, int i) {
        if (IsEnemyStaggered(i) == Bool3D.False) {
            return;
        }else if (IsEnemyStaggered(i) == Bool3D.True) {
            dollLimbController.Sliced();
        }

        SlicedCounter slicedCounter;
        int thisObjectSlicedCounterInt = 0;
        hitGameobjects[i].gameObject.TryGetComponent<SlicedCounter>(out slicedCounter);

        if (slicedCounter == null || slicedCounter.counter < 4) {
            thisObjectSlicedCounterInt = slicedCounter ? slicedCounter.counter : 0;
            SlicedHull hull = hitGameobjects[i].gameObject.Slice(cutPlane.transform.position, cutPlane.transform.up, slicedMaterial);
            if (hull != null) {
                GameObject bottom = hull.CreateLowerHull(hitGameobjects[i].gameObject, slicedMaterial);
                MeshCollider tempMeshCol = bottom.AddComponent<MeshCollider>();
                tempMeshCol.convex = true;
                Rigidbody tempRB = bottom.AddComponent<Rigidbody>();
                SlicedCounter bottomSlicedCounter = bottom.AddComponent<SlicedCounter>();
                bottomSlicedCounter.IncrementCounter(thisObjectSlicedCounterInt);
                bottom.gameObject.layer = LayerMask.NameToLayer("Sliceable");
                bottom.gameObject.tag = "Enemy";
                tempRB.AddExplosionForce(200, cutPlane.transform.position, 15);

                #region Fmod
                boxBreakingSound.setParameterByName("BoxBreak", 2.0f);
                boxBreakingSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(boxLocation));
                boxBreakingSound.start();

                swingHit.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(boxLocation));
                swingHit.start();
                #endregion

                GameObject top = hull.CreateUpperHull(hitGameobjects[i].gameObject, slicedMaterial);
                MeshCollider tempMeshCol2 = top.AddComponent<MeshCollider>();
                tempMeshCol2.convex = true;
                Rigidbody tempRB2 = top.AddComponent<Rigidbody>();
                SlicedCounter topSlicedCounter = top.AddComponent<SlicedCounter>();
                topSlicedCounter.IncrementCounter(thisObjectSlicedCounterInt);
                top.gameObject.layer = LayerMask.NameToLayer("Sliceable");
                top.gameObject.tag = "Enemy";
                tempRB2.AddExplosionForce(200, cutPlane.transform.position, 15);
                Destroy(hitGameobjects[i].gameObject);
            }
        }
    }

    /// <summary>
    /// Totally separated terrain slicing functionality
    /// </summary>
    /// <param name="other"></param>
    /// <param name="i"></param>
    void SlicingTerrain(Collider other, int i) {
        SlicedCounter slicedCounter;
        int thisObjectSlicedCounterInt = 0;
        hitGameobjects[i].gameObject.TryGetComponent<SlicedCounter>(out slicedCounter);

        if (slicedCounter == null || slicedCounter.counter < 4) {
            thisObjectSlicedCounterInt = slicedCounter ? slicedCounter.counter : 0;
            SlicedHull hull = hitGameobjects[i].gameObject.Slice(cutPlane.transform.position, cutPlane.transform.up, slicedMaterial);
            if (hull != null) {
                GameObject bottom = hull.CreateLowerHull(hitGameobjects[i].gameObject, slicedMaterial);
                MeshCollider tempMeshCol = bottom.AddComponent<MeshCollider>();
                tempMeshCol.convex = true;
                Rigidbody tempRB = bottom.AddComponent<Rigidbody>();
                SlicedCounter bottomSlicedCounter = bottom.AddComponent<SlicedCounter>();
                bottomSlicedCounter.IncrementCounter(thisObjectSlicedCounterInt);
                bottom.gameObject.layer = LayerMask.NameToLayer("Sliceable");
                bottom.gameObject.tag = "Terrain";
                tempRB.AddExplosionForce(200, cutPlane.transform.position, 15);

                #region Fmod
                boxBreakingSound.setParameterByName("BoxBreak", 2.0f);
                boxBreakingSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(boxLocation));
                boxBreakingSound.start();

                swingHit.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(boxLocation));
                swingHit.start();
                #endregion

                GameObject top = hull.CreateUpperHull(hitGameobjects[i].gameObject, slicedMaterial);
                MeshCollider tempMeshCol2 = top.AddComponent<MeshCollider>();
                tempMeshCol2.convex = true;
                Rigidbody tempRB2 = top.AddComponent<Rigidbody>();
                SlicedCounter topSlicedCounter = top.AddComponent<SlicedCounter>();
                topSlicedCounter.IncrementCounter(thisObjectSlicedCounterInt);
                top.gameObject.layer = LayerMask.NameToLayer("Sliceable");
                top.gameObject.tag = "Terrain";
                tempRB2.AddExplosionForce(200, cutPlane.transform.position, 15);
                Destroy(hitGameobjects[i].gameObject);
            }
        }
    }

#endregion
}

public enum AttackState {
    None,
    LightAttack,
    HeavyAttack
}
