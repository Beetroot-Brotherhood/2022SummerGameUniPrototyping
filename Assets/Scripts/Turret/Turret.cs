using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Krezme;

public class Turret : MonoBehaviour
{
    public Transform player;
    public Transform turretHead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        QualityOfLife.LookAtPlayer(turretHead, player);
    }
}
