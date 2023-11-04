using UnityEngine;

/*És útil quan volem que el personatge interactui correctament amb les físiques del joc i amb les colisions*/
public class RigidBodyControllerr : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private float jumpIntensity = 6f;
    [SerializeField] private bool useForce = false;
    [SerializeField] private float groundCheckerHeight = 0.4f;

    [SerializeField] private float gravityAddition;

    private Vector3 movementDir;
    private Rigidbody rb;
    private Collider col;
    private Camera cam;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = gameObject.GetComponent<Collider>();
        cam = Camera.main;
    }

    // Si volem moure a través del rigid body (físiques) és convenient capturar els inputs aquí
    // però utilitzar les físiques en el FixedDupdate
    void Update()
    {
        movementDir = Vector3.zero;

        movementDir.z = Input.GetAxis("Vertical");
        movementDir.x = Input.GetAxis("Horizontal");

        if (IsGrounded() && Input.GetButtonDown("Jump"))
            Jump();

    }

    private void FixedUpdate()
    {
        Move(movementDir);
    }

    private void Move(Vector3 movementDir)
    {
        //limitam la magnitud del vector de direcció a 1. No el podem normalitzar perque al venir el
        //valor d'un GetAxis, ens pot interessar que valgui menys que 1. Per això només el limitem per dalt
        movementDir = Vector3.ClampMagnitude(movementDir, 1f);

        //afegir la rotació de la càmera principal,
        //així sempre funcionarà bé sigui quina sigui la rotació de la càmera
        Vector3 movementDirAndCam = Quaternion.Euler(0, cam.gameObject.transform.eulerAngles.y, 0) * movementDir;
        if (useForce)
        {
            rb.AddForce(movementDirAndCam * moveSpeed, ForceMode.Acceleration);
        }
        else
        {
            //Opicó per si no volem moure el personatge per físiques. MovePosition es com si mogués el trasnsform
            //però no interactua bé amb colisions. Realment es tracta d'un mètode per moure objectes Kinemàtics.
            //rb.MovePosition(rb.position + movementDirAndCam * moveSpeed * Time.fixedDeltaTime);

            //Per això si volem detectar bé les colisions millor canviar directament la velocitat del RB
            Vector3 horizontalVelocity = movementDirAndCam * moveSpeed;
            rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);
        }

        // Simplement afegim una força adicional en la direcció del sistema de físiques
        // de Unity multiplicat per cert nombre. Així ajustem la gravetat del nostre personatge
        rb.AddForce(Physics.gravity * gravityAddition, ForceMode.Acceleration);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpIntensity, ForceMode.Impulse);
    }

    private bool IsGrounded()
    {

        // en aquest cas, el raycast el llancem directament des del centre del collider perquè només tenim uns sol gameObject
        // amb el pivot enmig del mesh. A partir del centre li passem un llongitud de projecció
        // Això també es pot fer amb un transform que actui d'origen pel raycast.
        bool isGrounded = Physics.Raycast(col.bounds.center, Vector3.down, out RaycastHit hit, col.bounds.extents.y + groundCheckerHeight);

        return isGrounded;
    }


    //mètode per dibuixar elements en la vista d'escena quan seleccionem cert gameObject
    private void OnDrawGizmosSelected()
    {
        Collider thisCollider = gameObject.GetComponent<Collider>();
        Gizmos.color = Color.red;
        Gizmos.DrawRay(thisCollider.bounds.center, Vector3.down * (thisCollider.bounds.extents.y + groundCheckerHeight));
    }
}