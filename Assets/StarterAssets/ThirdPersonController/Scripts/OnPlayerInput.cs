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

    public void OnInteract(InputValue value) {
        InteractInput(value.isPressed);
    }

    public void InteractInput(bool state) {
        onInteract = state;
    }
}
