using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponControllerProto : MonoBehaviour
{
    public GameObject Sword;
    public bool canAttack = true;
    public bool canParry = true;
    public float attackCooldown = 1.0f;
    public float parryCooldown = 1.0f;

    public AudioClip swordAttackSound;

    void Update()
    {
        if(OnSlicerInput.instance.onSlice)
        {

            if (canAttack)
            {
                SwordAttack();
            }


            
            OnSlicerInput.instance.onSlice = false;
        }

        if (OnSlicerInput.instance.onParry)
        {
            if (canParry)
            {
                SwordParry();
            }


            OnSlicerInput.instance.onParry = false;
        }



    }

    //attacking
    public void SwordAttack()
    {
        canParry = false;
        canAttack = false;
        int rnd = Random.Range(0, 2);

        Animator anim = Sword.GetComponent<Animator>();

        if (rnd == 0)
        {
            anim.SetTrigger("Attack");
        }
        else
        {
            anim.SetTrigger("Attack2");
        }



        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(swordAttackSound);


        StartCoroutine(ResetAttackCooldown());

        canParry = true;
    }


    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    //attack ends

    //parrying
    public void SwordParry()
    {
        canParry = false;
        canAttack = false;
        int rnd = Random.Range(0, 2);

        Animator anim = Sword.GetComponent<Animator>();


        anim.SetTrigger("Parry");

        StartCoroutine(ResetParryCooldown());

        canAttack = true;
    }

    IEnumerator ResetParryCooldown()
    {
        yield return new WaitForSeconds(parryCooldown);
        canParry = true;
        
    }
    //parrying ends

}
