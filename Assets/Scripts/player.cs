using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class player : MonoBehaviour
{

    private Master _Master;
    private Vector3 _Movement;
    private Rigidbody _Rb;

    [SerializeField] private float Speed;

    // Start is called before the first frame update
    private void Awake()
    {
        _Master = new Master();
        _Master.Player.Movement.performed += ctx => Move(ctx);
        

        _Rb = GetComponent<Rigidbody>();
        
    }


    private void Move(InputAction.CallbackContext ctx)
    {
        Vector2 Vec = ctx.ReadValue<Vector2>();
        _Movement.x = Vec.x;
        _Movement.z = Vec.y;
        
    }

    private void FixedUpdate()
    {
        _Rb.position += _Movement * Speed * Time.fixedDeltaTime;
        
    }

    private void Update()
    {
        Vector2 _mousePos = Mouse.current.position.ReadValue();


        Vector2 move = Camera.main.ScreenToWorldPoint(_mousePos);

        transform.Rotate(move, Space.World);

    }

    private void OnEnable()
    {
        _Master.Enable();
    }
    private void OnDisable()
    {
        _Master.Disable();
    }
}
