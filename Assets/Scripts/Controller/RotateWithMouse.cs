using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class RotateWithMouse : MonoBehaviour
{
    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] private float _turnSpeed = 3f;

    private void Update()
    {
        float horizontal;
        if(Application.isEditor)
        {
            horizontal = Input.GetAxis("Mouse X");
        }
        else
        {
            horizontal = joystick.Horizontal;
        }
        

        transform.Rotate(horizontal * _turnSpeed * Vector3.up, Space.World);

    }

}