using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObject : MonoBehaviour
{
    public GameObject followGameObject;

    public GameObject cameraBodyRoot;

    public float speed;

    public float thresholdDistance;

    public Vector3 rotationOffset;

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

        //this.transform.rotation = Quaternion.LookRotation(-Camera.main.transform.right, Vector3.up);
        this.transform.eulerAngles = new Vector3(-cameraBodyRoot.transform.eulerAngles.z + rotationOffset.x, cameraBodyRoot.transform.eulerAngles.y + rotationOffset.y, -cameraBodyRoot.transform.eulerAngles.x + rotationOffset.z);
    }
}
