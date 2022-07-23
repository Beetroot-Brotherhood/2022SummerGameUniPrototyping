using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text latchText, interactText;

    public Latch latchScript;
    public HumanController humanController;

    // Start is called before the first frame update
    void Start()
    {
        latchText.enabled = false;
        interactText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool canLatch = latchScript.canLatch;
        bool canInteract = humanController.canInteract;

        if (canLatch == true)
        {
            latchText.enabled = true;
        }
        else
        {
            latchText.enabled = false;
        }

        if (canInteract)
        {
            interactText.enabled = true;
        }
        else
        {

            interactText.enabled = false;
        }
    }
}
