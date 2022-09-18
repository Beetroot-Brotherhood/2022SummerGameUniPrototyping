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

    public GameObject weaponHitBox;
    public GameObject parryBox;

    public float throwForce;
    public float throwUpwardForce;
    public Transform cameraPoint;
    public Transform throwablePoint;
    public GameObject suctionThrowable;

    public float holdThreshold;
    public float parryCooldown;
    public float throwableCooldown;

    private PlayerParryBox playerParryBox;

    private float currentHoldTime;

    private bool isAttacking;
    private bool isParrying;

    private bool canAttack;
    private bool canParry;
    private bool canThrow;

    private IDictionary<AttackDirections, IDictionary<string, bool>> attackDirectionsAnimationStates = new Dictionary<AttackDirections, IDictionary<string, bool>>() {
        { AttackDirections.Forward, new Dictionary<string, bool>() { { "ForwardAttack", true}, {"LeftAttack", false}, {"BackAttack", false}, {"RightAttack", false} }},
        { AttackDirections.Left, new Dictionary<string, bool>() { { "ForwardAttack", false}, {"LeftAttack", true},  {"BackAttack", false}, {"RightAttack", false} }},
        { AttackDirections.Back, new Dictionary<string, bool>() { { "ForwardAttack", false}, {"LeftAttack", false},  {"BackAttack", true}, {"RightAttack", false} }},
        { AttackDirections.Right, new Dictionary<string, bool>() { { "ForwardAttack", false}, {"LeftAttack", false},  {"BackAttack", false}, {"RightAttack", true} }},
    };
 
    // Start is called before the first frame update
    void Start()
    {
        playerParryBox = parryBox.GetComponent<PlayerParryBox>();
        currentParryCooldown = parryCooldown;
        currentThrowableCooldown = throwableCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        AttackAnimationState();

        DirectionalAttacks();

        ParryFunc();

        ThrowableFunc();

        KickFunc();
    }

    void AttackAnimationState() {
        weaponAnimator.SetFloat("AttackDirection", (int)OnSlicerInput.instance.currentAttackDirection);
    }

    void DirectionalAttacks() {
        Debug.Log(OnSlicerInput.instance.onSlice);
        if (OnSlicerInput.instance.onSlice) {
            currentHoldTime += Time.deltaTime;
            if (currentHoldTime >= holdThreshold) {
                foreach (KeyValuePair<string, bool> keyValuePair in attackDirectionsAnimationStates[OnSlicerInput.instance.currentAttackDirection]) {
                    weaponAnimator.SetBool(keyValuePair.Key, keyValuePair.Value);
                }
                weaponAnimator.SetBool("Charging", true);
            }
        }
        else {
            
            if (currentHoldTime >= holdThreshold) {
                ResetAttackAnimationStates();
            }
            else if (currentHoldTime > 0) {
                ResetAttackAnimationStates();
                RandomizeAttack();
            }
            currentHoldTime = 0;
            weaponAnimator.SetBool("Charging", false);
        }
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

    public void ParryFunc()
    {
        if (OnSlicerInput.instance.onParry && canParry){
            canAttack = false;
            canParry = false;
            currentParryCooldown = 0;
        
            weaponAnimator.SetTrigger("Parry");
            OnSlicerInput.instance.onParry = false;
        }
        ResetParry();
        ParryReact();
    }

    private float currentParryCooldown;

    private void ResetParry() {
        if (currentParryCooldown < parryCooldown) {
            currentParryCooldown += Time.deltaTime;
        }
        else{
            canAttack = true;
            canParry = true;
        }
    }

    public void ParryReact()
    {
        if(playerParryBox.parryReactBool == true) {
            playerParryBox.parryReactBool = false;

            weaponAnimator.SetTrigger("ParryReact");
        }
    }

    public void ThrowableFunc()
    {
        if (OnSlicerInput.instance.onThrowable && canThrow) {
            canThrow = false;
            currentThrowableCooldown = 0;

            GameObject projectile = Instantiate(suctionThrowable, throwablePoint.position, cameraPoint.rotation);

            Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();

            Vector3 forceToAdd = cameraPoint.transform.forward * throwForce + transform.up * throwUpwardForce;

            projectileRB.AddForce(forceToAdd, ForceMode.Impulse);

            projectile.GetComponent<GatherSlicedObjects>().owner = this.gameObject;

            OnSlicerInput.instance.onThrowable = false;
        }
        ResetThrowableCooldown();
    }

    private float currentThrowableCooldown;

    private void ResetThrowableCooldown() {
        if (currentThrowableCooldown < throwableCooldown) {
            currentThrowableCooldown += Time.deltaTime;
        }
        else {
            canThrow = true;
        }
    }

    public void KickFunc() {
        if(OnSlicerInput.instance.onKick && canAttack)
        {
            weaponAnimator.SetTrigger("Kick");


            OnSlicerInput.instance.onKick = false;
        }
    }

#region AnimationEvents

    public void ResetAttackAnimationStates() {
        weaponAnimator.SetBool("ForwardAttack", false);
        weaponAnimator.SetBool("LeftAttack", false);
        weaponAnimator.SetBool("BackAttack", false);
        weaponAnimator.SetBool("RightAttack", false);
    }

    public void AttackingBoolControllerTrue()
    {
        isAttacking = true;
        weaponHitBox.SetActive(true);

        weaponAnimator.SetBool("Attacking", isAttacking);
    }

    public void AttackingBoolControllerFalse()
    {
        isAttacking = false;
        weaponHitBox.SetActive(false);

        weaponAnimator.SetBool("Attacking", isAttacking);
    }    

    public void ParryingBoolControllerTrue()
    {
        isParrying = true;
        parryBox.SetActive(true);

        weaponAnimator.SetBool("Parrying", isParrying);
    }

    public void ParryingBoolControllerFalse()
    {
        isParrying = false;
        parryBox.SetActive(false);

        weaponAnimator.SetBool("Parrying", isParrying);
    }

#endregion
}

public enum AttackDirections {
    Forward,
    Left,
    Back,
    Right
}
