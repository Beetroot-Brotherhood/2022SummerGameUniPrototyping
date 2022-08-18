using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObject : MonoBehaviour
{
    public GameObject followGameObject;

    public float speed;

    public float thresholdDistance;

    private float passedTimeToCompleteSwing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, followGameObject.transform.position) > thresholdDistance) {
            passedTimeToCompleteSwing += Time.deltaTime;
        }
        else {
            passedTimeToCompleteSwing = Time.deltaTime;
        }
        this.transform.position = Vector3.Lerp(this.transform.position, followGameObject.transform.position, speed * passedTimeToCompleteSwing);
    }
}
