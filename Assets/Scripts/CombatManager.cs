using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Krezme;
using UnityEngine.UI;

[System.Serializable]
public class Statistics{
    public float lightAttackDamage;
    public float heavyAttackDamage;
    public float ultimateCharge;
    public float ultimateRechargeRPS; // Ultimate recharge rate per second
}

public class CombatManager : MonoBehaviour
{

#region Singleton

    public static CombatManager instance;

    [SerializeField] private PlayerSounds playerSounds; // Added as the throwable item doesn't use an animation which I could use to trigger an event

    void Awake() {
        if (instance != null){
            Debug.LogError("More than one instance of CombatManager exist.");
        }
        else {
            instance = this;
        }
    }

#endregion

    [Header("Statistics")]
    public Statistics defaultStatistics;
    public Statistics currentStatistics;
    public Statistics maxStatistics;

    [Header("General")]
    public Animator weaponAnimator;
    public SwordCollisionDetectionV2 swordCollisionDetectionV2;
    public GameObject weaponHitBox;
    public GameObject parryBox;

    public float throwForce;
    public float throwUpwardForce;
    public Transform cameraPoint;
    public Transform throwablePoint;
    public GameObject suctionThrowable;
    public EnemyLockOn enemyLockOn;

    public Image ultimateBar;

    public float holdThreshold;
    public float parryCooldown;
    public float throwableCooldown;

    public float timeBetweenUltimateCuts;

    [HideInInspector]
    public List<int> enemySliceOrder = new List<int>();

    private PlayerParryBox playerParryBox;

    [HideInInspector]
    public List<EnemyStatisticsManager> hitEnemies = new List<EnemyStatisticsManager>();

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
        ResetCurrentStatistics();
    }

    // Update is called once per frame
    void Update()
    {
        AttackAnimationState();

        DirectionalAttacks();

        ParryFunc();

        ThrowableFunc();

        KickFunc();

        Ultimate();

        UltimateRecharge();
    }

    void AttackAnimationState() {
        weaponAnimator.SetFloat("AttackDirection", (int)OnSlicerInput.instance.currentAttackDirection);
    }

    void DirectionalAttacks() {
        if (!OnSlicerInput.instance.onUltimate) {
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

            playerSounds.PlayBallShoot();
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

    public void IncreaseUltimateCharge(float amount) {
        currentStatistics.ultimateCharge += amount;
        if (currentStatistics.ultimateCharge > maxStatistics.ultimateCharge) {
            currentStatistics.ultimateCharge = maxStatistics.ultimateCharge;
        }
        
        if (currentStatistics.ultimateCharge == maxStatistics.ultimateCharge) {
            canUltimate = true;
        }

        ultimateBar.fillAmount = currentStatistics.ultimateCharge / maxStatistics.ultimateCharge;
    }

    public void KickFunc() {
        if(OnSlicerInput.instance.onKick && canAttack)
        {
            weaponAnimator.SetTrigger("Kick");
            FMODUnity.RuntimeManager.PlayOneShot("event:/--- Code Slicer ---/Player/Kick/KickAction");

            OnSlicerInput.instance.onKick = false;
        }
    }

    public void ResetCurrentStatistics() {
        currentStatistics = defaultStatistics.DeepClone();
    }

    private bool canUltimate = true;

    private float currentTimeBetweenUltimateCuts = 0;

    public void Ultimate() {
        
        if (canUltimate) {
            UltimatePreparation();
        }

        if (!canUltimate) {
            UltimateFunc();
        }
    }

    public float currentRecharge = 0;

    public void UltimateRecharge(){
        if (currentStatistics.ultimateCharge < maxStatistics.ultimateCharge) {
            currentRecharge += Time.deltaTime * currentStatistics.ultimateRechargeRPS;
            if (currentRecharge >= 1) {
                currentRecharge = -1;
                IncreaseUltimateCharge(1);
            }
        }
    }

    public void UltimatePreparation() {
        if (OnSlicerInput.instance.onUltimate && canUltimate) {
            weaponAnimator.SetBool("UltimateSheath", true);
            EnemyLockOn.instance.lockOn = true;
            if (OnSlicerInput.instance.onSlice) {
                if (EnemyLockOn.instance.visibleTargets.Count > 0) {
                    weaponAnimator.SetBool("UltimateAttack", true);
                    EnemyLockOn.instance.attackedTargets = EnemyLockOn.instance.visibleTargets; //! Check if this makes a clone or a reference
                    enemySliceOrder = QualityOfLife.RandomNumberListWithoutRepeating(0, EnemyLockOn.instance.attackedTargets.Count-1);
                    canUltimate = false;
                    currentStatistics.ultimateCharge = 0;
                    IncreaseUltimateCharge(0);
                }
                else{
                    //! FILL this in with the code for when there are no visible targets
                }
            }
        }
        else {
            ResetUltAnimParameters();
            EnemyLockOn.instance.lockOn = false;
        }
    }

    private int enemySliceOrderIndex;

    public void UltimateFunc() {
        if (enemySliceOrderIndex < enemySliceOrder.Count) {

            currentTimeBetweenUltimateCuts += Time.deltaTime;

            if (currentTimeBetweenUltimateCuts >= timeBetweenUltimateCuts) {
                currentTimeBetweenUltimateCuts = 0;

                YbotTestController2 tempYbotController2 = EnemyLockOn.instance.attackedTargets[enemySliceOrder[enemySliceOrderIndex]].GetComponent<YbotTestController2>();
                
                try {
                    Debug.Log(tempYbotController2.chestPiece.GetComponent<DollLimbController>());

                    SwordCollisionDetectionV2.instance.SlicingEnemy(
                        tempYbotController2.chestPiece.GetComponent<Collider>(), 
                        tempYbotController2.chestPiece.transform.position,
                        new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized,
                        SwordCollisionDetectionV2.instance.slicedMaterial,
                        tempYbotController2.chestPiece.transform.position,
                        tempYbotController2.chestPiece.GetComponent<DollLimbController>(),
                        enemySliceOrderIndex, 
                        bypass:true
                    );
                }
                catch (Exception) {}
                enemySliceOrderIndex++;

                //TODO Add additional sounds for the ultimate
            }
        }

        if (enemySliceOrderIndex >= enemySliceOrder.Count) {
            enemySliceOrderIndex = 0;
            EnemyLockOn.instance.attackedTargets.Clear();
            enemySliceOrder.Clear();
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

    public void SetAttackStateToNone() {
        swordCollisionDetectionV2.currentAttackState = AttackState.None;
        hitEnemies = new List<EnemyStatisticsManager>();
    }

    public void SetAttackStateToLightAttack(){
        swordCollisionDetectionV2.currentAttackState = AttackState.LightAttack;
    }

    public void SetAttackStateToHeavyAttack(){
        swordCollisionDetectionV2.currentAttackState = AttackState.HeavyAttack;
    }

    public void ResetUltAnimParameters(){
        weaponAnimator.SetBool("UltimateSheath", false);
        weaponAnimator.SetBool("UltimateAttack", false);
    }
#endregion
}

public enum AttackDirections {
    Forward,
    Left,
    Back,
    Right
}
