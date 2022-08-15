using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YbotTestController2 : MonoBehaviour
{
    public Animator anim;

    public float parryStunTime = 4.0f;
    public float attackCooldown = 1.0f;

    public bool canAttack = true;
    public bool staggered = false;

    public bool isAttacking = false;

    public GameObject weaponHitbox;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        if(weaponHitbox.GetComponent<ybotSwordCollisionDetection>().staggerCollision == true)
        {
            
            YbotStagger();
        }


        if (canAttack == true)
        {
            YbotAttackFunc();
        }
    }

    //attacking functions for playing animations and cooldown
    public void YbotAttackFunc()
    {
        canAttack = false;

        

        
        anim.SetTrigger("YbotAttack");

        StartCoroutine(ResetYbotAttackCooldown());
    }

    IEnumerator ResetYbotAttackCooldown()
    {
        Animator anim = this.GetComponent<Animator>();
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        anim.SetBool("Attacking", false);

    }


    //attacking functions for animation trigger
    public void YbotAttackingTrue()
    {

        weaponHitbox.SetActive(true);
        anim.SetBool("Attacking", true);
    }

    public void YbotAttackingFalse()
    {
        weaponHitbox.SetActive(false);
        anim.SetBool("Attacking", false);
    }
    //


    //stagger functions
    void YbotStagger()
    {
        weaponHitbox.GetComponent<ybotSwordCollisionDetection>().staggerCollision = false;
        staggered = true;
        canAttack = false;


        anim.SetBool("Staggering", true);
        anim.SetTrigger("Stagger");
        

        StartCoroutine(YbotStaggerTimer());
    }


        IEnumerator YbotStaggerTimer()
    {
        
        yield return new WaitForSeconds(parryStunTime);


        anim.SetBool("Staggering", false);

        canAttack = true;
        staggered = false;
    }

}
