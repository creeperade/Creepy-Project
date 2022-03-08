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

    
    

    [SerializeField] private float speed;
    private Vector2 _move;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _master = new Master();
        _move = _master.Player.Movement.ReadValue<Vector2>();

    }
    

    private void FixedUpdate()
    {
        _rb.position += _movement * (speed * Time.fixedDeltaTime);
        
    }

    private void Update()
    {
        Transform camTransform = main.transform;
        _movement = camTransform.right * move.x + camTransform.forward * move.y;
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


