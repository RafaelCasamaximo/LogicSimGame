using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CircuitSimulatorFreeCamera : MonoBehaviour
{
    private Vector2 _playerMoveInput;
    public float horizontalSensitivity = 20f;
    public float verticalSensitivity = 20f;
    public float zoomSensitivity = 10f;
    private int backgroundWidth;
    private int backgroundHeight;
    private float horizontalMin;
    private float horizontalMax;
    private float verticalMin;
    private float verticalMax;
    private Vector2 zoomValue;
    private Camera _camera;

    private void Awake()
    {
        
    }

    private void Start()
    {
        _camera = Camera.main;
        backgroundWidth = CircuitSimulatorManager.Instance.backgroundWidth;
        backgroundHeight = CircuitSimulatorManager.Instance.backgroundHeight;
        
        float halfHeight = _camera.orthographicSize;
        float halfWidth = _camera.aspect * halfHeight;

        horizontalMin = halfWidth;
        horizontalMax = - halfWidth + backgroundWidth;
        verticalMin = halfHeight;
        verticalMax = - halfHeight + backgroundHeight;

        transform.position = new Vector3((horizontalMin + horizontalMax) / 2, (verticalMin + verticalMax) / 2, -10f);
    }

    private void Update()
    {
        if (_playerMoveInput != Vector2.zero)
        {
            transform.Translate(_playerMoveInput * Time.deltaTime * new Vector2(horizontalSensitivity, verticalSensitivity));
            Vector3 pos = transform.position;
            pos.x =  Mathf.Clamp(pos.x, horizontalMin, horizontalMax);
            pos.y =  Mathf.Clamp(pos.y, verticalMin, verticalMax);
            transform.position = pos;
        }
        
        if (zoomValue != Vector2.zero)
        {
            float halfHeight = _camera.orthographicSize;
            float halfWidth = _camera.aspect * halfHeight;
            float z = transform.position.z;

            horizontalMin = halfWidth;
            horizontalMax = - halfWidth + backgroundWidth;
            verticalMin = halfHeight;
            verticalMax = - halfHeight + backgroundHeight;

            transform.Translate(new Vector3(0f, 0f, zoomValue.y) * (Time.deltaTime * zoomSensitivity));
            Vector3 pos = transform.position;
            pos.z =  Mathf.Clamp(pos.z, -20f, -7f);
            transform.position = pos;
        }


    }

    /*
     * Handlers do FreeCamera InputMap
     * Convenção de nome: Handle<Nome do Input Action><Nome do Mapping><Nome da Action>
     */

    public void HandleCircuitSimulatorFreeCameraMove(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            _playerMoveInput = Vector2.zero;
            return;
        }
        if (!context.performed) return;
        _playerMoveInput = context.ReadValue<Vector2>();
    }
    
    public void HandleCircuitSimulatorFreeCameraZoom(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            zoomValue = Vector2.zero;
            return;
        }

        if (!context.performed) return;
        zoomValue = context.ReadValue<Vector2>();
    }

}
