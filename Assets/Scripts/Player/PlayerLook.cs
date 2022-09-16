using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
/// <summary>
/// Script responsável pela movimentação da camera do player
/// Esse script usa o novo event system do novo player input
/// </summary>
public class PlayerLook : MonoBehaviour
{
    private Vector2 _playerLookInput;

    public Transform playerTransform;

    private float xRotation = 0f; 
    public float horizontalSensitivity = 100f;
    public float verticalSensitivity = 100;
    
    // Update is called once per frame
    void Update()
    {
        float mouseX = _playerLookInput.x * horizontalSensitivity * Time.deltaTime;
        float mouseY = _playerLookInput.y * verticalSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -75f, 75f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerTransform.Rotate(Vector3.up * mouseX);
    }
    
    public void HandlePlayerMovementLook(InputAction.CallbackContext context)
    {
        _playerLookInput = context.ReadValue<Vector2>();
    }
    
}
