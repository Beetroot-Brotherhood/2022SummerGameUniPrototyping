using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrainController : MonoBehaviour
{
    private PlayerInputs _input;
    public Cinemachine.CinemachineDollyCart dollyCartControls;
    public CharacterController _controller;
    public float _speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<PlayerInputs>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        
        this.transform.rotation = dollyCartControls.transform.rotation; //this is the line that makes the train rotate with the track
        dollyCartControls.m_Speed = _input.move.y * _speed;

    }

}
