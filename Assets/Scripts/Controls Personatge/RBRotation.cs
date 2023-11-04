using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

/// <summary>
/// Versi� del controlador per Rigidbody amb separaci� d'eixos Axis
/// El vertical per avan�ar i l'horitzontal per rotar.
/// 
/// Aquesta versi� tamb� incorpora control de salts.
/// 
/// Veure: RigidBodyController penjat al classroom.
/// </summary>
public class RBRotation : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityAddition;

    [SerializeField] private Transform groundChecker;

    [SerializeField] private int maxJumps = 2;

    [SerializeField] private TextMeshProUGUI velocityText;


    private int remainJumps = 0;

    private float movement;
    private float rotation;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        remainJumps = maxJumps;
    }

    // Update is called once per frame
    void Update()
    {
        // Aqu� separem els floats per les Axis vertical i hortizontal.
        movement = Input.GetAxis("Vertical");
        rotation = Input.GetAxis("Horizontal");

        // si intentem botar amb l'acci� de Jump es comprova que si estem a terra
        // hem de reiniciar els bots disponibles. Despr�s si encara ens queden bots
        // disponibles podrem botar.
        if (Input.GetButtonDown("Jump"))
        {
            if (GroundCheck())
                remainJumps = maxJumps;

            if (remainJumps > 0)
                Jump();

        }

        // el valor obtingut amb les Axis Horizontal l'utilitzem per rotar el transform
        transform.Rotate(Vector3.up * rotation * rotationSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Move();

        velocityText.text = rb.velocity.magnitude.ToString("f2");
    }

    /// <summary>
    /// Movem modificant la velocitat de Rigidbody per� utilitzat el float obtingut
    /// amb les Axis Vertical 'movement' i aplique el moviment en relaci� al forward del transform. Aix� 
    /// sempre s'avan�a cap 'endavant' si entenem per 'endavant' l'eix blau del transform.
    /// </summary>
    private void Move()
    {
        Vector3 horizontalVelocity = transform.forward * movement * movementSpeed;

        rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);

        //multiplicador de gravetat
        rb.AddForce(Physics.gravity * gravityAddition, ForceMode.Acceleration);
    }

    /// <summary>
    /// Aquest m�tode llan�a un Ray cap a baix per saber si el nostre personatge
    /// 'toca' a terra. El m�tode Raycast retorna un boole� vertader si el Ray impacta.
    /// </summary>
    /// <returns></returns>
    private bool GroundCheck()
    {
        return Physics.Raycast(groundChecker.position, -groundChecker.up, 0.1f);
    }

    /// <summary>
    /// Botem amb Impulse o VelocityChange i restem un bot als bots restants.
    /// </summary>
    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
        remainJumps--;
    }

    //m�tode per dibuixar elements en la vista d'escena quan seleccionem cert gameObject
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(groundChecker.position, -groundChecker.up * 0.1f);
    }
}