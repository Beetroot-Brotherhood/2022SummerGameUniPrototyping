using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnPlayerInput : MonoBehaviour
{
    [Header("Character Input Values")]
		public Vector2 move; // player movement
		public Vector2 look; // free camera look
		public bool jump;
		public bool sprint;
		public bool readyLazer;
		public bool readySight;
		public bool lazerFire;
		public bool missileFire;
		public bool onBoard;

		public bool onHideUI;

    [Header("Movement Settings")]
		public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}
   
    public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

	public void OnJump(InputValue value)
	{
		JumpInput(value.isPressed);
	}

	public void OnSprint(InputValue value)
	{
		SprintInput(value.isPressed);
	}

	public void OnReadySight(InputValue value)
	{
		ReadySightInput(value.isPressed);
	}

	public void OnReadyLazer(InputValue value)
	{
		ReadyLazerInput(value.isPressed);
	}

	public void OnLazerFire(InputValue value)
	{
		LazerFireInput(value.isPressed);
	}
	public void OnMissileFire(InputValue value)
	{
		MissileFireInput(value.isPressed);
	}
	public void OnBoard(InputValue value)
	{
		OnBoardInput(value.isPressed);
	}
	public void OnHideUI(InputValue value)
	{
		HideUIInput(value.isPressed);
	}



    public void MoveInput(Vector2 newMoveDirection)
	{
		move = newMoveDirection;
	} 
	public void LookInput(Vector2 newLookDirection)
	{
		look = newLookDirection;
	}
	public void JumpInput(bool newJumpState)
	{
		jump = newJumpState;
	}
	public void SprintInput(bool newSprintState)
	{
		sprint = newSprintState;
	}
	void ReadySightInput(bool newReadySightState)
	{
		if(newReadySightState)
		{
			newReadySightState = false;
			readySight = !readySight;
			FMODUnity.RuntimeManager.PlayOneShot("event:/Mech/Buttons/sight button");
		}

	}
	void ReadyLazerInput(bool newReadyLazerState)
	{
		if (newReadyLazerState)
		{
			newReadyLazerState = false;
			readyLazer = !readyLazer;
			FMODUnity.RuntimeManager.PlayOneShot("event:/Mech/Buttons/arm button");
		}

	}
	public void LazerFireInput(bool newLazerFireState)
	{
		lazerFire = newLazerFireState;
	}
	public void MissileFireInput(bool newMissileFireState)
	{
		missileFire = newMissileFireState;
	}
	public void OnBoardInput(bool newOnBoardState)
	{
		onBoard = newOnBoardState;
	}
	public void HideUIInput(bool newHideUIState)
	{
		if(newHideUIState)
		{
			newHideUIState = false;
			onHideUI = !onHideUI;
		}
	}

	private void OnApplicationFocus(bool hasFocus) // locks the cursor if the game is in focus
	{
		SetCursorState(cursorLocked);
	}

	private void SetCursorState(bool newState)
	{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
	}
    
}
