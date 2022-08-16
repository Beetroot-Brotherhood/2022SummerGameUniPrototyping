using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrainController : MonoBehaviour
{
    private StarterAssets.StarterAssetsInputs _input;
    public Cinemachine.CinemachineDollyCart dollyCartControls;
    public CharacterController _controller;
    public float _speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<StarterAssets.StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
       
        Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;
        inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y;

        inputDirection.z = dollyCartControls.m_Position;

        _controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) );
    }

    private void OnMove()
    {
        
    }

}
