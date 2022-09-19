using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedCounter : MonoBehaviour
{
    public int counter = 0;
    bool moveTowardsPlayer;
    Rigidbody rigidbody;
    Collider collider;
    GameObject player;
    float timePassed;

    public void IncrementCounter(int previousCounter) {
        counter = previousCounter + 1;
    }

    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    void Update() {
        if (moveTowardsPlayer) {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer () {
        timePassed += Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, timePassed);
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 0.4) {
            //GatherSlicedObjects.instance.score += 10;
            Destroy(this.gameObject);
        }
    }

    public void StartMovingTowardsPlayer (GameObject newPlayer) {
        player = newPlayer;
        rigidbody.useGravity = false;
        collider.isTrigger = true;
        moveTowardsPlayer = true;
        timePassed = 0;
    }

    public void StopMovingTowardsPlayer () {
        player = null;
        rigidbody.useGravity = true;
        collider.isTrigger = false;
        moveTowardsPlayer = false;
        timePassed = 0;
    }
}
