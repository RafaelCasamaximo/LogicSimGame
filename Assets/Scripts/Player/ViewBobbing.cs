using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Esse script é responsável pelo viewBobbing do personagem
/// A frequencia altera a velocidade do passo e a amplitude a variação na altura
/// Esse script deve ser colocado em um gameObject com o character controller,
/// mantendo a estrutura
/// CharacterController <ViewBobbing>
/// |-CameraHolder
/// ||-Camera
/// </summary>
public class ViewBobbing : MonoBehaviour
{

    [SerializeField] private bool _enable = true;
    [SerializeField, Range(0, 0.1f)] private float _amplitude = 0.015f;
    [SerializeField, Range(0, 30f)] private float _frequency = 10.0f;

    [SerializeField] private Transform _camera = null;
    [SerializeField] private Transform _cameraHolder = null;

    private float _toggleSpeed = 3.0f;
    private Vector3 _startPos;
    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _startPos = _camera.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!_enable) return;
        
        ResetPosition();
        CheckMotion();
        _camera.LookAt(FocusTarget());
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * _frequency) * _amplitude;
        pos.x += Mathf.Cos(Time.time * _frequency / 2) * _amplitude * 2;
        return pos;
    }

    private void CheckMotion()
    {
        float speed = transform.InverseTransformDirection(_controller.velocity).magnitude;
        
        if (speed < _toggleSpeed) return;

        PlayMotion(FootStepMotion());
    }
    
    private void PlayMotion(Vector3 motion){
        _camera.localPosition += motion; 
    }

    private void ResetPosition()
    {
        if (_camera.localPosition == _startPos) return;

        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, 10f * Time.deltaTime);
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + _cameraHolder.localPosition.y, transform.position.z);
        pos += _cameraHolder.forward * 15.0f;
        return pos;
    }
}
