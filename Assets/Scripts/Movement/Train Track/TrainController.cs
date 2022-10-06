using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrainController : MonoBehaviour
{
    private PlayerInputs _input;
    private PlayerInput _playerInput;
    public Cinemachine.CinemachineDollyCart dollyCartControls;
    public Transform newdollyCart;
    public Spline spline;
    public CharacterController _controller;
    public float _speed = 5.0f;
    public float LookSpeed = 3.0f;

    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;
    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 90.0f;
    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -90.0f;
    [Tooltip("How far in degrees can you move the camera right")]
    public float RightClamp = 90.0f;
    [Tooltip("How far in degrees can you move the camera left")]
    public float LeftClamp = -90.0f;

    // cinemachine
    private float _cinemachineTargetPitchY;
    private float _cinemachineTargetPitchX;

     private const float _threshold = 0.01f;
    private bool IsCurrentDeviceMouse
		{
			get
			{
				return _playerInput.currentControlScheme == "KeyboardMouse";
			}
		}

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<PlayerInputs>();
        _playerInput = GetComponent<PlayerInput>();

    }

    // Update is called once per frame
    void Update()
    {
        //Move();
    }
    private void LateUpdate()
    {
        CameraRotation();
    }

    void OnEnable()
    {
        //_playerInput.SwitchCurrentActionMap("Train");
    }

    void OnDisable()
    {
        dollyCartControls.m_Speed = 0;
        this.transform.rotation = Quaternion.identity;
    }

    private void Move()
    {
        
        //this.transform.rotation = dollyCartControls.transform.rotation; //this is the line that makes the train rotate with the track
        //dollyCartControls.m_Speed = _input.move.y * _speed;


        //newdollyCart.forward = newdollyCart.position * _input.move.y * _speed;
        Vector3 move = newdollyCart.forward * _input.move.y * _speed;
        newdollyCart.position = spline.WhereOnSpline(move);
        
        this.transform.rotation = newdollyCart.rotation; //this is the line that makes the train rotate with the trac

        if (_input == null)
        {
            //dollyCartControls.m_Speed = 0;
            newdollyCart.position = Vector3.zero;
        }

    }

    private void CameraRotation()
    {
        // if there is an input
        if (_input.look.sqrMagnitude >= _threshold)
        {
            //Don't multiply mouse input by Time.deltaTime
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
            
            _cinemachineTargetPitchY += _input.look.y * LookSpeed * deltaTimeMultiplier;
            _cinemachineTargetPitchX += _input.look.x * LookSpeed * deltaTimeMultiplier;
            

            // clamp our pitch rotation
            _cinemachineTargetPitchY = ClampAngle(_cinemachineTargetPitchY, BottomClamp, TopClamp);
            // clamp our pitch rotation
            _cinemachineTargetPitchX = ClampAngle(_cinemachineTargetPitchX, LeftClamp, RightClamp);

            // Update Cinemachine camera target pitch
            CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitchY, _cinemachineTargetPitchX, 0.0f);

            
        }
    }
     private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

}
