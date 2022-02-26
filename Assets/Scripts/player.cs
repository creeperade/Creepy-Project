using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class player : MonoBehaviour
{

    private Master _Master;
    private Vector3 _Movement = new Vector3();
    private Rigidbody _Rb;
    private float YRotation = 0f;

    [SerializeField] private float speed;
    [SerializeField] private float mouseSensitivity;

    // Start is called before the first frame update
    private void Awake()
    {
        _Master = new Master();
        _Master.Player.Movement.performed += ctx => Move(ctx);
        

        _Rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }


    private void Move(InputAction.CallbackContext ctx)
    {
        Vector2 Vec = ctx.ReadValue<Vector2>().normalized;
        _Movement.x = Vec.x;
        _Movement.z = Vec.y;
        
    }

    private void FixedUpdate()
    {
        
        _Rb.position += _Movement * speed * Time.fixedDeltaTime;
        
    }

    private void Update()
    {
        Vector2 _mousePos = _Master.Player.Look.ReadValue<Vector2>() * mouseSensitivity;

        transform.rotation = Quaternion.Euler(0, _mousePos.x, 0);

        Camera.main.transform.Rotate(_mousePos.y, 0,0);
        Debug.Log(_mousePos);
      

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
