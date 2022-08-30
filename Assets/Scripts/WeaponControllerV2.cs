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
  

    private void Start()
    {
        anim = GetComponent<Animator>();
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





    }



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




}
