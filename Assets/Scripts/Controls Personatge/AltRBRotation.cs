using UnityEngine;

/// <summary>
/// Versi� del controlador per Rigidbody sense rotaci� manual per�
/// amb orientaci� autom�tica del transform cap a la direcci� on s'avan�a
/// 
/// Aqui tamb� utilitzem un sol stick del comandament per moure el Rigidbody 
/// en l'eix horitzontal per� aquest rotar� sempre cap a la direcci� del moviment
/// Es girar� cap on avan�a
/// 
/// Per la resta t� el mateix funcionament que RigidbodyController.
/// 
/// Veure: RigidBodyController penjat al classroom.
/// </summary>
public class AltRBRotation : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float groundCheckerLength;

    [SerializeField] private float gravityAddition;

    private Vector3 moveDirection = Vector3.zero;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection.z = Input.GetAxis("Vertical");
        moveDirection.x = Input.GetAxis("Horizontal");

        moveDirection = Vector3.ClampMagnitude(moveDirection, 1);

        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        Rotate();
    }

    private void FixedUpdate()
    {
        Move();

        // Simplement afegim una for�a adicional en la direcci� del sistema de f�siques
        // de Unity multiplicat per cert nombre. Aix� ajustem la gravetat del nostre personatge
        rb.AddForce(Physics.gravity * gravityAddition, ForceMode.Acceleration);
    }

    private void Move()
    {


        Vector3 horizontalVelocity = moveDirection * moveSpeed;
        rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);
    }

    /// <summary>
    /// Aquest m�tode fa la seva m�gia en dues l�nies per rotar el transform suaument i a certa
    /// velocitat cap a la direcci� del moviment
    /// </summary>
    private void Rotate()
    {
        // Obtenim la rotaci� referent a una direcci� de moviment. El moviment en que es mour� el personatge.
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

        // Interpolem la rotaci� des de la rotaci� actual del transform cap a la rotaci� objectiu
        // uns quants graus cada frame. La velocitat de rotaci� dependr� de 'rotationSpeed'
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(groundChecker.position, -groundChecker.up, out RaycastHit hitInfo, groundCheckerLength);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(groundChecker.position, -groundChecker.up * groundCheckerLength);
    }
}
