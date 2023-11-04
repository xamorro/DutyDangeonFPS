using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;

    [SerializeField] private float speed = 15f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float remainingJumps = 2; // ME FALTA SEBRE COM FER ES DOBLE SALT BÉ.

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;

    //per si volguesim fer lo des groundcheck funciones en un determinat lloc. Emplean Layer
    [SerializeField] private LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Si grounded es true i sa velocitat és < 0, posa sa posició Y a -2. Ja que si no o feim, sa gravetat sempre va augmentant es valor cap abaix.
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Cream una esfera amb un radius determinat que si es tocat donará TRUE. Si posam mascara(layer), només donará true quan toqui es layer asignat.
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance);


        //INPUTS per captar posicions de sa x y z.
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Ficam és valors des inputs dins un vector3 anomenat move. Es transform x * s'imput X + es Transform z * s'input Z.
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        //Aplicam velocitat de gravetat per frame a sa Y
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        jump();
    }


        private void jump()
    {
        //Si pijam sa tecla de botar i grounded es true aplica una operacio matematica a velocitat y que mos deixará botar fins a una determinada altura
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            remainingJumps = 2f;
        }
        if (Input.GetButtonDown("Jump") && remainingJumps > 0)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            remainingJumps--;
        }
    }
}
