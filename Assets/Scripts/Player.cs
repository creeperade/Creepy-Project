using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using static UnityEngine.Camera;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private Rigidbody _rb;
    private Master _master;
    private Vector3 _movement;
    private Vector2 _move;
    private Transform _camTransform;
    private float _speed;
    private bool _isGrounded;
    private int _currentJumps;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private int jumps;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    [FormerlySerializedAs("radius")] [SerializeField] private float raySize;
    [SerializeField] private LayerMask groundLayer;


    private void Awake()
    {
        _speed = moveSpeed;
        _currentJumps = jumps;
        _rb = GetComponent<Rigidbody>();
        _master = new Master();
        _master.Player.Movement.performed += Move;
        _master.Player.Jump.performed += Jump;
        _master.Player.Run.performed += ctx => _speed *= sprintMultiplier;
        _master.Player.Run.canceled += ctx => _speed /= sprintMultiplier;
        _camTransform = main!.transform;

    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (_currentJumps > 0)
        {
        
            _rb.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);
            _currentJumps--;
        }
    }


    private void Move(InputAction.CallbackContext ctx)
    {
        
        _move = ctx.ReadValue<Vector2>();
        

    }


    private void Update()
    {
        
        _isGrounded = Physics.Raycast(groundCheck.position, groundCheck.TransformDirection(-groundCheck.transform.up), raySize);

        if (_isGrounded)
        {
            _currentJumps = jumps;
        }
            
        _movement = (_camTransform.right * _move.x + _camTransform.forward * _move.y) * _speed;


    }

    private void FixedUpdate()
    {
        if (_isGrounded)
        {
            _rb.
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(groundCheck.position, groundCheck.TransformDirection(-groundCheck.transform.up) * raySize);
    }

    #region Input enable and disable
    private void OnEnable()
    {
        _master.Enable();
    }
    private void OnDisable()
    {
        _master.Disable();
    }
    #endregion
}


