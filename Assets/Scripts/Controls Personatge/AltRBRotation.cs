using UnityEngine;

/// <summary>
/// Versió del controlador per Rigidbody sense rotació manual però
/// amb orientació automàtica del transform cap a la direcció on s'avança
/// 
/// Aqui també utilitzem un sol stick del comandament per moure el Rigidbody 
/// en l'eix horitzontal però aquest rotarà sempre cap a la direcció del moviment
/// Es girarà cap on avança
/// 
/// Per la resta té el mateix funcionament que RigidbodyController.
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

        // Simplement afegim una força adicional en la direcció del sistema de físiques
        // de Unity multiplicat per cert nombre. Així ajustem la gravetat del nostre personatge
        rb.AddForce(Physics.gravity * gravityAddition, ForceMode.Acceleration);
    }

    private void Move()
    {


        Vector3 horizontalVelocity = moveDirection * moveSpeed;
        rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);
    }

    /// <summary>
    /// Aquest mètode fa la seva màgia en dues línies per rotar el transform suaument i a certa
    /// velocitat cap a la direcció del moviment
    /// </summary>
    private void Rotate()
    {
        // Obtenim la rotació referent a una direcció de moviment. El moviment en que es mourà el personatge.
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

        // Interpolem la rotació des de la rotació actual del transform cap a la rotació objectiu
        // uns quants graus cada frame. La velocitat de rotació dependrà de 'rotationSpeed'
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
