using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponControllerProto : MonoBehaviour
{
    public GameObject Sword;
    public bool canAttack = true;
    public float attackCooldown = 1.0f;

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



    }

    public void SwordAttack()
    {
        canAttack = false;

        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(swordAttackSound);


        StartCoroutine(ResetAttackCooldown());

    }


    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }




}
