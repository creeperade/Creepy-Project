using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Camera;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private Rigidbody _rb;
    private Master _master;
    private Vector3 _movement;
    private Vector2 _move;
    private Transform _camTransform;


    [SerializeField] private float speed;


    private void Awake()
    {
        

        _rb = GetComponent<Rigidbody>();
        _master = new Master();
        _master.Player.Movement.performed += Move;
        _camTransform = main!.transform;

    }
    void Move(InputAction.CallbackContext ctx) => _move = ctx.ReadValue<Vector2>();


    private void Update()
    {

        _movement = _camTransform.right * _move.x + _camTransform.forward * _move.y;
    }

    private void FixedUpdate()
    {
        _rb.position += _movement * (speed * Time.fixedDeltaTime);
        
    }


    private void OnEnable()
    {
        _master.Enable();
    }

    private void OnDisable()
    {
        _master.Disable();
    }
}


