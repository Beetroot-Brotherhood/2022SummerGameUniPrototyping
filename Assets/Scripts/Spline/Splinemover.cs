using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splinemover : MonoBehaviour
{
    
    public Spline spline;
    public Transform followObj;
    public Transform followObj2;
    private Transform thisTransform;
    


    private void Start()
    {
        thisTransform = transform;
    }

    private void Update()
    {
        if(!StickingToTrack.instance.onTrack)
        {
            Debug.Log("Not on track");
            thisTransform.position = spline.WhereOnSpline(followObj.position);
        }
        else
        {
            Debug.Log("track");
            thisTransform.position = spline.WhereOnSpline(followObj2.position);
        }
        
    }
}