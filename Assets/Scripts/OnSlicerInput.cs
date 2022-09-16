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
    [HideInInspector] public bool onGatherSlicedParts;
    [HideInInspector] public bool onParry;
    [HideInInspector] public bool onThrowable;
    [HideInInspector] public bool onKick;
    [HideInInspector] public AttackDirections currentAttackDirection;

    //public GameObject slicer;
    
    public void OnRotateAntiClock (InputValue value) {
        onRotateAntiClock = value.isPressed;
    }

    public void OnRotateClock (InputValue value) {
        onRotateClock = value.isPressed;
    }

    public void OnSlice (InputValue value) {
        onSlice = value.isPressed;
    }

    public void OnGatherSlicedParts(InputValue value) {
        onGatherSlicedParts = value.isPressed;
    }

    public void FixedUpdate () {
        /* if (onRotateAntiClock) {
            slicer.transform.Rotate(Vector3.forward*5f, Space.Self);
            //onRotateAntiClock = false;
        }else if (onRotateClock) {
            slicer.transform.Rotate(Vector3.back*5f, Space.Self);
            //onRotateClock = false;
        } */

    }

    public void OnParry(InputValue value)
    {
        onParry = value.isPressed;
    }

    public void OnThrowable(InputValue value)
    {
        onThrowable = value.isPressed;
    }

    public void OnKick(InputValue value)
    {
        onKick = value.isPressed;
    }

    public void OnForwardAttack(InputValue value){
        if (value.isPressed) {
            currentAttackDirection = AttackDirections.Forward;
        }
    }

    public void OnLeftAttack(InputValue value){
        if (value.isPressed) {
            currentAttackDirection = AttackDirections.Left;
        }
    }

    public void OnBackAttack(InputValue value){
        if (value.isPressed) {
            currentAttackDirection = AttackDirections.Back;
        }
    }

    public void OnRightAttack(InputValue value){
        if (value.isPressed) {
            currentAttackDirection = AttackDirections.Right;
        }
    }
}