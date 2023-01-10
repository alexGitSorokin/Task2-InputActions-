using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Fields
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float gravity = -30f; 

    [SerializeField] private float _jumpHeight = 3;
    private bool _jump;

   private Vector2 _horizontalInput;
    Vector3 verticalVelocity = Vector3.zero;

    [SerializeField] private LayerMask _groundMask;
    private bool _isGrounded;
    #endregion

    #region Properties
    private bool IsGrounded { get => _isGrounded = Physics.CheckSphere(transform.position, 0.6f, _groundMask); }
    #endregion

    public void ReceiveInput(Vector2 horizontalInput) => _horizontalInput = horizontalInput;

    private void Update()
    {
        CheckVerticalVelocity();
        CheckHorizontalVelocity();
    }

    private void CheckVerticalVelocity()
    {
        if (IsGrounded)
            verticalVelocity.y = 0;

        if (_jump)
        {
            if (_isGrounded)
                verticalVelocity.y = Mathf.Sqrt(-2f * _jumpHeight * gravity);
            else
                _jump = false;
        }

        verticalVelocity.y += gravity * Time.deltaTime;
        _controller.Move(verticalVelocity * Time.deltaTime);
    }

    private void CheckHorizontalVelocity()
    {
        Vector3 horizontalVelocity = (transform.right * _horizontalInput.x + transform.forward * _horizontalInput.y)
            * _speed;
        _controller.Move(horizontalVelocity * Time.deltaTime);
    }

    public void OnJumpPressed() => _jump = true;
}
