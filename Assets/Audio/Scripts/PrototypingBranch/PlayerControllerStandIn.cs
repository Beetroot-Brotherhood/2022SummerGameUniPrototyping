using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerStandIn : MonoBehaviour
{
    //! THIS GOES INTO THE PLAYER CONTROLLER
    
    [SerializeField] private PlayerSounds playerSounds; //* This provides a reference point for the script which allows you to call the various sound functions which will be written into the 'PlayerSounds' script


    void PlayStepLeft() //* This will trigger the PlayerSounds script to execute the code within the PlayStep function 
    {
        playerSounds.PlayStepLeft();
    }

    void PlayStepRight() //* This will trigger the PlayerSounds script to execute the code within the PlaySteo function 
    {
        playerSounds.PlayStepRight();
    }

}