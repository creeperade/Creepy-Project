using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private Rigidbody _rb;
    private Master _master;
    private Vector3 _movement = Vector3.forward;

    [SerializeField] private float speed;
    
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _master = new Master();
        _master.Player.Movement.performed += ctx => Move(ctx);

    }

    private void Move(InputAction.CallbackContext ctx)
    {
        Vector2 move = ctx.ReadValue<Vector2>();
        _movement.x = move.x;
        _movement.z = move.y;
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movement * speed * Time.fixedDeltaTime;
    }
}


