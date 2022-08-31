using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControllerV2 : MonoBehaviour
{
    public float attackCooldown = 1.0f;
    public float parryCooldown = 1.0f;

    public GameObject hitBox;
    public GameObject parryBox;

    public Animator anim;

    public bool canAttack = true;
    public bool canParry = true;

    public bool isAttacking = false;
    public bool isParrying = false;

    public LayerMask enemyLayer;
    public float finisherRayCastRange;

    [Header("Suction Throwable Variables")]

    public Transform throwablePoint;
    public GameObject suctionThrowable;
    public float throwableCooldown;
    public bool canThrow = true;
    public float throwForce;
    public float throwUpwardForce;
    public Transform cameraPoint;
  

    private void Start()
    {
        anim = GetComponent<Animator>();
        canThrow = true;
    }

    private void Update()
    {
        if (OnSlicerInput.instance.onSlice && canAttack)
        {
            AttackFunc();


            OnSlicerInput.instance.onSlice = false;
        }

        if (OnSlicerInput.instance.onParry && canParry)
        {
            ParryFunc();


            OnSlicerInput.instance.onParry = false;
        }

        if(parryBox.GetComponent<PlayerParryBox>().parryReactBool == true)
        {

            ParryReact();



        }

        if (OnSlicerInput.instance.onThrowable && canThrow)
        {

            ThrowableFunc();


            OnSlicerInput.instance.onThrowable = false;
        }

        if(OnSlicerInput.instance.onKick && canAttack)
        {
            anim.SetTrigger("Kick");


            OnSlicerInput.instance.onKick = false;
        }

    }


    // start of attack code
    public void AttackFunc()
    {
        canAttack = false;
        canParry = false;



        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out hit, finisherRayCastRange, enemyLayer, QueryTriggerInteraction.UseGlobal))
        {
            if(hit.collider.gameObject.TryGetComponent<YbotTestController2>(out YbotTestController2 ybotTestController2))
            {
                if (ybotTestController2.staggered)
                {
                    anim.SetTrigger("Finisher");
                }
                else
                {
                    RandomizeAttack();
                }
            }
            else
            {
                RandomizeAttack();
            }

        }
        else
        {
            RandomizeAttack();
        }




        StartCoroutine(ResetAttackCooldown());

    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
        canParry = true;

        

    }

    private void RandomizeAttack()
    {
        int rnd = Random.Range(0, 2);

        if (rnd == 0)
        {
            anim.SetTrigger("Attack1");
        }
        else
        {
            anim.SetTrigger("Attack2");
        }
    }


    public void AttackingBoolControllerTrue()
    {
        isAttacking = true;
        hitBox.SetActive(true);


        anim.SetBool("Attacking", isAttacking);


    }

    public void AttackingBoolControllerFalse()
    {
        isAttacking = false;
        hitBox.SetActive(false);


        anim.SetBool("Attacking", isAttacking);


    }
    //end of attacking code




    // start of parry code
    public void ParryFunc()
    {
        canAttack = false;
        canParry = false;

        anim.SetTrigger("Parry");

        StartCoroutine(ResetParryCooldown());
    }

    IEnumerator ResetParryCooldown()
    {
        yield return new WaitForSeconds(parryCooldown);

        canAttack = true;
        canParry = true;
    }

    public void ParryingBoolControllerTrue()
    {
        isParrying = true;
        parryBox.SetActive(true);

        anim.SetBool("Parrying", isParrying);
    }

    public void ParryingBoolControllerFalse()
    {
        isParrying = false;
        parryBox.SetActive(false);

        anim.SetBool("Parrying", isParrying);
    }

    public void ParryReact()
    {
        parryBox.GetComponent<PlayerParryBox>().parryReactBool = false;

        anim.SetTrigger("ParryReact");




    }
    //end of parry code

    //start of throwable code

    public void ThrowableFunc()
    {
        Debug.Log("Throwable!");

        canThrow = false;

        GameObject projectile = Instantiate(suctionThrowable, throwablePoint.position, cameraPoint.rotation);

        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();

        Vector3 forceToAdd = cameraPoint.transform.forward * throwForce + transform.up * throwUpwardForce;

        projectileRB.AddForce(forceToAdd, ForceMode.Impulse);

        projectile.GetComponent<GatherSlicedObjects>().owner = this.gameObject;

        StartCoroutine(ResetThrowableCooldown());
    }

    IEnumerator ResetThrowableCooldown()
    {
        yield return new WaitForSeconds(throwableCooldown);

        canThrow = true;
    }
}
