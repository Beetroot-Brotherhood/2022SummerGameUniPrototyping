using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombatManager : MonoBehaviour
{

    public static CombatManager instance;

    void Awake() {
        if (instance != null){
            Debug.LogError("More than one instance of CombatManager exists.");
        }
        else {
            instance = this;
        }
    }

    public Animator weaponAnimator;

    public float holdThreshold;
    private float currentHoldTime;
    private IDictionary<AttackDirections, Action> animatorDirectionalAttacksController = new Dictionary<AttackDirections, Action>();
 
    // Start is called before the first frame update
    void Start()
    {
        animatorDirectionalAttacksController.Add(AttackDirections.Forward, CombatManager.instance.ForwardAttack);
        animatorDirectionalAttacksController.Add(AttackDirections.Left, CombatManager.instance.LeftAttack);
        animatorDirectionalAttacksController.Add(AttackDirections.Back, CombatManager.instance.BackAttack);
        animatorDirectionalAttacksController.Add(AttackDirections.Right, CombatManager.instance.RightAttack);
    }

    // Update is called once per frame
    void Update()
    {
        AttackAnimationState();

        DirectionalAttacks();
    }

    void DirectionalAttacks() {
        if (OnSlicerInput.instance.onSlice) {
            currentHoldTime += Time.deltaTime;
            weaponAnimator.SetBool("Charging", true);
        }
        else {
            if (currentHoldTime >= holdThreshold) {
                animatorDirectionalAttacksController[OnSlicerInput.instance.currentAttackDirection]();
            }
            currentHoldTime = 0;
            weaponAnimator.SetBool("Charging", false);
        }
    }

    public void ForwardAttack() {
        weaponAnimator.SetBool("ForwardAttack", true);
        weaponAnimator.SetBool("LeftAttack", false);
        weaponAnimator.SetBool("BackAttack", false);
        weaponAnimator.SetBool("RightAttack", false);
    }
    
    public void LeftAttack() {
        weaponAnimator.SetBool("ForwardAttack", false);
        weaponAnimator.SetBool("LeftAttack", true);
        weaponAnimator.SetBool("BackAttack", false);
        weaponAnimator.SetBool("RightAttack", false);
    }
    
    public void BackAttack() {
        weaponAnimator.SetBool("ForwardAttack", false);
        weaponAnimator.SetBool("LeftAttack", false);
        weaponAnimator.SetBool("BackAttack", true);
        weaponAnimator.SetBool("RightAttack", false);
    }
    
    public void RightAttack() {
        weaponAnimator.SetBool("ForwardAttack", false);
        weaponAnimator.SetBool("LeftAttack", false);
        weaponAnimator.SetBool("BackAttack", false);
        weaponAnimator.SetBool("RightAttack", true);
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
}

public enum AttackDirections {
    Forward,
    Left,
    Back,
    Right
}
