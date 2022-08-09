using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YbotAttackingTest : MonoBehaviour
{
    
    public GameObject ybotWeaponHolder;
    public float ybotAttackCooldown = 1.5f;
    public float staggerTimer = 3.0f;

    public bool canAttack = true;
    public bool staggered = false;




    // Update is called once per frame
    void Update()
    {
        Animator anim = this.GetComponent<Animator>();

        if (canAttack)
        {

            ybotAttackFunc();

        }

        if (ybotWeaponHolder.GetComponent<ybotSwordCollisionDetection>().staggerCollision == true && anim.GetBool("Attacking") == true)
        {
            ybotStagger(); 
        }



    }


    //attack
    void ybotAttackFunc()
    {
        canAttack = false;

        Animator anim = this.GetComponent<Animator>();

        anim.SetBool("Attacking", true);
        anim.SetTrigger("YbotAttack");

        StartCoroutine(ResetYbotAttackCooldown());

    }


    IEnumerator ResetYbotAttackCooldown()
    {
        Animator anim = this.GetComponent<Animator>();
        yield return new WaitForSeconds(ybotAttackCooldown);
        canAttack = true;
        anim.SetBool("Attacking", false);

    }
    //attack end

    //stagger
    void ybotStagger()
    {
        canAttack = false;
        staggered = true;

        Animator anim = this.GetComponent<Animator>();

        anim.SetBool("Staggering", true);
        anim.SetTrigger("YbotStagger");

        StartCoroutine(YbotStaggerTimer());
    }

    IEnumerator YbotStaggerTimer()
    {
        Animator anim = this.GetComponent<Animator>();
        yield return new WaitForSeconds(staggerTimer);
        canAttack = true;
        ybotWeaponHolder.GetComponent<ybotSwordCollisionDetection>().staggerCollision = false;
        anim.SetBool("Staggering", false);
        staggered = false;
    }
    //stagger end




}
