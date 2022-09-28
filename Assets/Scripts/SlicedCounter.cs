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
    private FMOD.Studio.EventInstance collisionSounds;

    public void IncrementCounter(int previousCounter) {
        counter = previousCounter + 1;
    }

    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        collisionSounds = FMODUnity.RuntimeManager.CreateInstance("event:/--- Code Slicer ---/Environment/BoxPiecesCollisionPlaceholder");
        collisionSounds.setParameterByName("SlicesPitch", counter);
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

    public float RequiredVelocity = 4;
    //Creates inspector window slot in which the GameObject that contains the desired to be played RandomAudioPlayer Sctipt must be placed (In this case it should be tbe object that this script is also placed on)

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > RequiredVelocity)
        {
            PlayCollision();
        }
    }

    public void PlayCollision()
    {
        collisionSounds.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        collisionSounds.start();
        collisionSounds.release();
    }
}
