using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Script responsável pelo movimento do player e também pelo check do chão
/// Esse script usa o novo event system do novo player input
/// </summary>
public class PlayerMove : MonoBehaviour
{

    
    private Vector2 _playerMoveInput;
    private Vector3 _velocity;
    private bool _isGrounded;
    
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        Vector3 move = transform.right * _playerMoveInput.x + transform.forward * _playerMoveInput.y;
        _velocity.y += gravity * Time.deltaTime;
        controller.Move(Time.deltaTime * speed * move + _velocity * Time.deltaTime);
    }

    public void HandlePlayerMovementMove(InputAction.CallbackContext callback)
    {
        _playerMoveInput = callback.ReadValue<Vector2>();
    }
}
