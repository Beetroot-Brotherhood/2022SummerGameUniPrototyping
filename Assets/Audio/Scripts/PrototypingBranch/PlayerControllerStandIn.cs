using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerStandIn : MonoBehaviour
{
    //! THIS GOES INTO THE PLAYER CONTROLLER
    
    private PlayerSounds playerSounds; //* This provides a reference point for the script which allows you to call the various sound functions which will be written into the 'PlayerSounds' script


    void PlayStep() //* This will trigger the PlayerSounds script to execute the code within the PlaySteo function 
    {
        playerSounds.PlayStep();
    }

}