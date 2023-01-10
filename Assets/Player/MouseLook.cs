using UnityEngine;

public class MouseLook : MonoBehaviour
{
    #region Fields

    [SerializeField] private float _sensitivityX = 8f;
    [SerializeField] private float _sensitivityY = 0.5f;

    [SerializeField] private Transform _playerCamera;
    [SerializeField] private float _xClamp = 85f;
   
    private float xRotation = 0f;
    float mouseX, mouseY;

    #endregion

    #region Methods
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Rotate();

    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up, mouseX * Time.deltaTime);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -_xClamp, _xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        _playerCamera.eulerAngles = targetRotation;
    }

    public void ReceiveInput(Vector2 mouseInput)
    {
        mouseX = mouseInput.x * _sensitivityX;
        mouseY = mouseInput.y * _sensitivityY;
    }
    #endregion
}
