using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public OnPlayerInput _input;
    public Canvas controlsUI;

    // Update is called once per frame
    void Update()
    {
        if(_input.onHideUI)
        {
            controlsUI.enabled = false;
        }
        else
        {
            controlsUI.enabled = true;
        }
    }
}
