using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    private Master _master;
    private Vector3 _movement;
    private Rigidbody _rb;

    [SerializeField] private float speed;
    [SerializeField] private float mouseSensitivity;

    // Start is called before the first frame update
    private void Awake()
    {
        _master = new Master();
        _master.Player.Movement.performed += ctx => Move(ctx);
        

        _rb = GetComponent<Rigidbody>();
            
        Cursor.visible = false;

    }


    private void Move(InputAction.CallbackContext ctx)
    {
        Vector2 vec = ctx.ReadValue<Vector2>().normalized;
        _movement.x = vec.x;
        _movement.z = vec.y;
        
    }

    private void FixedUpdate()
    {
        
        _rb.position += _movement * speed * Time.fixedDeltaTime;
        
    }

    private void Update()
    {
        Vector2 mousePos = _master.Player.Look.ReadValue<Vector2>() * mouseSensitivity;

        transform.rotation = Quaternion.Euler(0, mousePos.x, 0);

        if (Camera.main != null) Camera.main.transform.Rotate(mousePos.y, 0, 0);
        Debug.Log(mousePos);
      

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
