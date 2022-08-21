using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordReference : MonoBehaviour
{
    public Collider swordCollider;

    public void BeginAttack() {
        swordCollider.enabled = true;
    }

    public void EndAttack() {
        swordCollider.enabled = false;
    }
}
