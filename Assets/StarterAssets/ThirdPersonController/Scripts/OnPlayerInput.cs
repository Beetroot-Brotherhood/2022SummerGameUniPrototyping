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
    public bool onEquipRight;
    public bool onEquipLeft;

    public void OnEquipRight(InputValue value) {
        EquipRightInput(value.isPressed);
    }

    public void OnEquipLeft(InputValue value) {
        EquipLeftInput(value.isPressed);
    }

    public void EquipRightInput(bool state) {
        onEquipRight = state;
    }

    public void EquipLeftInput(bool state) {
        onEquipLeft = state;
    }
}
