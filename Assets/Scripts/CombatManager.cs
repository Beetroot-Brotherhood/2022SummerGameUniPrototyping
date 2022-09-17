using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombatManager : MonoBehaviour
{

#region Singleton

    public static CombatManager instance;

    void Awake() {
        if (instance != null){
            Debug.LogError("More than one instance of CombatManager exists.");
        }
        else {
            instance = this;
        }
    }

#endregion

    public Animator weaponAnimator;

    public float holdThreshold;
    [SerializeField]
    private float currentHoldTime;
    private IDictionary<AttackDirections, IDictionary<string, bool>> attackDirectionsAnimationStates = new Dictionary<AttackDirections, IDictionary<string, bool>>() {
        { AttackDirections.Forward, new Dictionary<string, bool>() { { "ForwardAttack", true}, {"LeftAttack", false}, {"BackAttack", false}, {"RightAttack", false} }},
        { AttackDirections.Left, new Dictionary<string, bool>() { { "ForwardAttack", false}, {"LeftAttack", true},  {"BackAttack", false}, {"RightAttack", false} }},
        { AttackDirections.Back, new Dictionary<string, bool>() { { "ForwardAttack", false}, {"LeftAttack", false},  {"BackAttack", true}, {"RightAttack", false} }},
        { AttackDirections.Right, new Dictionary<string, bool>() { { "ForwardAttack", false}, {"LeftAttack", false},  {"BackAttack", false}, {"RightAttack", true} }},
    };
 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AttackAnimationState();

        DirectionalAttacks();
    }

    void DirectionalAttacks() {
        Debug.Log(OnSlicerInput.instance.onSlice);
        if (OnSlicerInput.instance.onSlice) {
            currentHoldTime += Time.deltaTime;
            if (currentHoldTime >= holdThreshold) {
                weaponAnimator.SetBool("Charging", true);
            }
        }
        else {
            
            if (currentHoldTime >= holdThreshold) {
                foreach (KeyValuePair<string, bool> keyValuePair in attackDirectionsAnimationStates[OnSlicerInput.instance.currentAttackDirection]) {
                    weaponAnimator.SetBool(keyValuePair.Key, keyValuePair.Value);
                }
            }
            else if (currentHoldTime > 0) {
                ResetAttackAnimationStates();
                RandomizeAttack();
            }
            currentHoldTime = 0;
            weaponAnimator.SetBool("Charging", false);
        }
    }

    public void ResetAttackAnimationStates() {
        weaponAnimator.SetBool("ForwardAttack", false);
        weaponAnimator.SetBool("LeftAttack", false);
        weaponAnimator.SetBool("BackAttack", false);
        weaponAnimator.SetBool("RightAttack", false);
    }

    void AttackAnimationState() {
        weaponAnimator.SetFloat("AttackDirection", (int)OnSlicerInput.instance.currentAttackDirection);
    }

    private void RandomizeAttack()
    {
        int rnd = UnityEngine.Random.Range(0, 2);

        if (rnd == 0)
        {
            weaponAnimator.SetTrigger("Attack1");
        }
        else
        {
            weaponAnimator.SetTrigger("Attack2");
        }
    }
}

public enum AttackDirections {
    Forward,
    Left,
    Back,
    Right
}
