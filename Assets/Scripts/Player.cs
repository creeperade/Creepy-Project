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
    private bool _isGrounded;
    private int _currentJumps;


    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private int jumps;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    [FormerlySerializedAs("radius")] [SerializeField] private float raySize;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float dampSpeed;
    
    
    

    
    private void Awake()
    {
        _currentJumps = jumps;
        _rb = GetComponent<Rigidbody>();
        _master = new Master();
        _master.Player.Movement.performed += Move;
        _master.Player.Jump.performed += Jump;
        _master.Player.Run.performed += ctx => { };
        _master.Player.Run.canceled += ctx => { };
        _camTransform = main != null ? main.transform : null;

    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (_currentJumps > 0)
        {
            _rb.position += Vector3.up*jumpForce;
            _currentJumps--;
        }
    }


    private void Move(InputAction.CallbackContext ctx)
    {

        _move = ctx.ReadValue<Vector2>();
    }


    private void Update()
    {
        
        _isGrounded = Physics.Raycast(groundCheck.position, groundCheck.TransformDirection(-groundCheck.transform.up), raySize, groundLayer);

        if (_isGrounded)
        {
            _currentJumps = jumps;
        }

        if (_move != Vector2.zero)
        {
        }
        

        
            
            
        _movement = (_camTransform.right * _move.x + _camTransform.forward * _move.y) * moveSpeed;


    }

    private void FixedUpdate()
    {
        if (_isGrounded)
        {
            _rb.velocity = _movement * Time.fixedDeltaTime;
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


