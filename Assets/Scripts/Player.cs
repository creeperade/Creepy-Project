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
    private float _speed;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintMultiplier;
        


    private void Awake()
    {
        _speed = moveSpeed;

        _rb = GetComponent<Rigidbody>();
        _master = new Master();
        _master.Player.Movement.performed += Move;
       
        _master.Player.Run.performed += (ctx => _speed *= sprintMultiplier);
        _master.Player.Run.canceled += (ctx => _speed /= sprintMultiplier);
        _camTransform = main!.transform;

    }

    
   

    private void Move(InputAction.CallbackContext ctx)
    {
        
        _move = ctx.ReadValue<Vector2>();
        

    }


    private void Update()
    {
        
        _movement = (_camTransform.right * _move.x + _camTransform.forward * _move.y) * _speed;


    }

    private void FixedUpdate()
    {
        _rb.position += _movement * Time.fixedDeltaTime;

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


