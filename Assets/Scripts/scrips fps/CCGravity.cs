using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCGravity : MonoBehaviour
{
    [SerializeField] private CharacterController cc;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpHeight = 4f;
    [SerializeField] private float gravityMultiplier = 2f;

    private Vector3 moveDirection;
    private Vector3 jumpVelocity;
    private float gravityApplied;

    private void Start()
    {
        moveDirection = Vector3.zero;
        gravityApplied = Physics.gravity.y * gravityMultiplier;
    }

    public void Jump()
    {
        if (cc.isGrounded || !cc.isGrounded)
        {
            jumpVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityApplied);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ApplyGravity();
    }

    void Move()
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.z = Input.GetAxis("Vertical");

        Vector3 movement = transform.forward * moveDirection.z + transform.right * moveDirection.x;

        //clamp the magnitude of this movement vector fixes de probLem with diagonal velocity and avoids 
        // the strange behavior that apears mixing GetAxis and Normalizing vectors.
        movement = Vector3.ClampMagnitude(movement, 1f);

        cc.Move(movement * speed * Time.deltaTime);
    }

    void ApplyGravity()
    {
        cc.Move(jumpVelocity * Time.deltaTime);

        if (cc.isGrounded && jumpVelocity.y < 0f)
        {
            jumpVelocity.y = 0f;
        }
        else
        {
            jumpVelocity.y += gravityApplied * Time.deltaTime;
        }

    }
}
