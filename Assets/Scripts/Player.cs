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
    private float _currentSpeed;
    private bool _isGrounded;
    private int _currentJumps;
    private float _speedCap;
    
    
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
        _speedCap = moveSpeed;
        _currentJumps = jumps;
        _rb = GetComponent<Rigidbody>();
        _master = new Master();
        _master.Player.Movement.performed += Move;
        _master.Player.Jump.performed += Jump;
        _master.Player.Run.performed += ctx => _currentSpeed *= sprintMultiplier;
        _master.Player.Run.canceled += ctx => _currentSpeed /= sprintMultiplier;
        _camTransform = main!.transform;

    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (_currentJumps > 0)
        {
            _rb.AddForce(0f, jumpForce, 0f, ForceMode.VelocityChange);
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
            if (_currentSpeed < _speedCap)
            {
                _currentSpeed += dampSpeed * Time.deltaTime;
            }
            else
            {
                _speedCap += Mathf.Sqrt(_currentSpeed);
            }
            
        }
        else
        {
            if(_currentSpeed <= 0) return;
            _currentSpeed -= dampSpeed * Time.deltaTime;
             _speedCap = moveSpeed;
        }

        
            
            
        _movement = (_camTransform.right * _move.x + _camTransform.forward * _move.y) * _currentSpeed;


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


