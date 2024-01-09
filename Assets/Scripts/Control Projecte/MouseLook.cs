using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float mouseSensibility = 100f;
    public Transform playerBody;
    private Vector2 inputDirection;

    float xRotation = 0f;


        // Start is called before the first frame update
        void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    //// Update is called once per frame
    //void Update()
    //{

    //    float mouseX = Input.GetAxis("Mouse X") * mouseSensibility;
    //    float mouseY = Input.GetAxis("Mouse Y") * mouseSensibility;

    //    xRotation -= mouseY;
    //    xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    //    transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    //    playerBody.Rotate(Vector3.up * mouseX);
    //}

    private void Update()
    {
        CameraMovement();
    }

    public void SetCameraMoveDirection(Vector2 direction)
    {
        inputDirection = direction;
    }
    void CameraMovement()
    {

        xRotation -= inputDirection.y;
         xRotation = Mathf.Clamp(xRotation, -90f, 90f);

         transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
         playerBody.Rotate(Vector3.up * inputDirection.x);
    }


}
