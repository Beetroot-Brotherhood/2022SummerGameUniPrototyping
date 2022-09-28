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

    public GameObject targetedUI;

    public GameObject chestPiece;

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

        Krezme.QualityOfLife.LookAtPlayer(targetedUI.transform, Camera.main.transform);
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
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        anim.SetBool("Attacking", false);
    }


    //attacking functions for animation trigger
    public void YbotAttackingTrue()
    {

        
        anim.SetBool("Attacking", true);
    }

    public void YbotAttackingFalse()
    {
        
        anim.SetBool("Attacking", false);
    }
    //


    //stagger functions
    public void YbotStagger()
    {
        staggered = true;
        canAttack = false;
        weaponHitbox.GetComponent<ybotSwordCollisionDetection>().staggerCollision = false;

        anim.SetBool("Staggering", true);
        anim.SetTrigger("YbotStagger");
        

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
