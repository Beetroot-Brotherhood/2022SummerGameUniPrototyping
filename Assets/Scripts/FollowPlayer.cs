using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class FollowPlayer : MonoBehaviour
{
     public PathCreator pathCreator;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = pathCreator.path.GetClosestPointOnPath(player.transform.position);
    }
}
