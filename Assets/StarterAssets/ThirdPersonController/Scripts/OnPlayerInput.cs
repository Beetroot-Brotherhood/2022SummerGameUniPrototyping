using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnPlayerInput : MonoBehaviour
{

    public static OnPlayerInput instance;

    void Awake() {
        if (instance != null) {
            Debug.LogError("More than one instance of OnPlayerInput exists!");
        }
        else {
            instance = this;
        }
    }

    [HideInInspector]
    public bool onInteract;
    [HideInInspector]
    public bool onAttack;

    public void OnInteract(InputValue value) {
        InteractInput(value.isPressed);
    }

    public void OnAttack(InputValue value) {
        AttackInput(value.isPressed);
    }

    public void InteractInput(bool state) {
        onInteract = state;
    }

    public void AttackInput(bool state) {
        onAttack = state;
        Debug.Log("onAttack: " + onAttack);
    }
}
