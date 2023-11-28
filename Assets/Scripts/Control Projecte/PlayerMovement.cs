using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;

    [SerializeField] private float speed = 15f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float MaxJumps = 2f;

    //[SerializeField] private Transform groundCheck;
    //[SerializeField] private float groundDistance = 0.4f;

    //per si volguesim fer lo des groundcheck funciones en un determinat lloc. Emplean Layer
        //[SerializeField] private LayerMask groundMask;

    private float remainingJumps = 2f;
    private Vector3 moveDirection;
    Vector3 jumpVelocity;


    //bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        remainingJumps = 2f;
    }

    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    public void Jump()
    {
        if (controller.isGrounded || (!controller.isGrounded && remainingJumps > 0))
        {
            //Si pijam sa tecla de botar i grounded es true aplica una operacio matematica a velocitat Y que mos deixará botar fins a una determinada altura
            jumpVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            remainingJumps--;
        }
    }

    // Update is called once per frame
    void Update()
    {        
        Move();

        
        ApplyGravity();


        //Si grounded es true i sa velocitat és < 0, posa sa posició Y a -2. Ja que si no o feim, sa gravetat sempre va augmentant es valor cap abaix.
        if (controller.isGrounded && jumpVelocity.y < 0)
        {
            jumpVelocity.y = -2f;
        }

        //Cream una esfera amb un radius determinat que si es tocat donará TRUE. Si posam mascara(layer), només donará true quan toqui es layer asignat.
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);



    }

    void Move()
    {
        //Movement será es forward * sa direccio Z q rebem + es right  * sa direccio X q rebem de l'script PlayerInputs
        Vector3 movement = transform.forward * moveDirection.z + transform.right * moveDirection.x;

        //clamp the magnitude of this movement vector fixes de probLem with diagonal velocity and avoids 
        // the strange behavior that apears mixing GetAxis and Normalizing vectors.
        movement = Vector3.ClampMagnitude(movement, 1f);

        //Movem es controller amb sa posicio movement * velocitat * deltatime
        controller.Move(movement * speed * Time.deltaTime);

        if (isMoving())
        {
            AudioManager.I.PlaySound(SoundName.Pasos);
        }

    }

    private bool isMoving()
    {
        Vector3 horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);

        return horizontalVelocity.magnitude > 0.1f;
    }



    void ApplyGravity()
    {
        controller.Move(jumpVelocity * Time.deltaTime);

        if (controller.isGrounded && jumpVelocity.y < 0f)
        {
            remainingJumps = MaxJumps;
            jumpVelocity.y = gravity;
        }
        else
        {
            jumpVelocity.y += gravity * Time.deltaTime;
        }

    }
}
