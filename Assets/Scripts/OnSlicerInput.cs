using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnSlicerInput : MonoBehaviour
{
    public static OnSlicerInput instance;

    void Awake () {
        if (instance != null) {
            Debug.LogError("Two instances of OnSlicerInput!!!!");
        }else {
            instance = this;
        }
    }

    [HideInInspector] public bool onRotateAntiClock;
    [HideInInspector] public bool onRotateClock;
    [HideInInspector] public bool onSlice;
    
    public void OnRotateAntiClock (InputValue value) {
        onRotateAntiClock = value.isPressed;
    }

    public void OnRotateClock (InputValue value) {
        onRotateClock = value.isPressed;
    }

    public void OnSlice (InputValue value) {
        onSlice = value.isPressed;
    }

    public void Update () {
        if (onRotateAntiClock) {
            gameObject.transform.Rotate(Vector3.forward*0.8f, Space.Self);
            //onRotateAntiClock = false;
        }else if (onRotateClock) {
            gameObject.transform.Rotate(Vector3.back*0.8f, Space.Self);
            //onRotateClock = false;
        }
    }
}
