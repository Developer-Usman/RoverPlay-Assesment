using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _jumpSpeed = 0.5f;
    [SerializeField] private float _gravity = 2f;
    
    CharacterController _characterController;
    private Vector3 _moveDirection;

    void Awake() => _characterController = GetComponent<CharacterController>();
    void FixedUpdate()
    {
        

        float horizontal;
        float vertical;
        if(Application.isEditor)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        else
        {
            horizontal = joystick.Horizontal;
            vertical = joystick.Vertical;
        }

        Vector3 inputDirection = new Vector3(horizontal, 0, vertical);
        Vector3 transformDirection = transform.TransformDirection(inputDirection);
        
        Vector3 flatMovement = _moveSpeed * Time.deltaTime * transformDirection;

        _moveDirection = new Vector3(flatMovement.x, _moveDirection.y , flatMovement.z);

        Jump();

        _characterController.Move(_moveDirection);
    }
    public void Jump()
    {
        if (PlayerJumped)
            _moveDirection.y = _jumpSpeed;
        else if (_characterController.isGrounded)
            _moveDirection.y = 0f;
        else
            _moveDirection.y -= _gravity * Time.deltaTime;
    }

    private bool PlayerJumped => _characterController.isGrounded && Input.GetKey(KeyCode.Space);
    
}