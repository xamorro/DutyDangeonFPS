using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

// SCRIPT VALIDATED. GOOD PRACTICES. APROVED.
public class FPMovementController : MonoBehaviour
{
    [SerializeField] private Transform groundChecker;
    [SerializeField] private int JumpforceAmount = 10;
    [SerializeField] private float groundCheckerLength;
    [SerializeField] private int MovementForce = 10;

    private Rigidbody rigidb;


    private Vector3 moveDirection;

    private void Awake()
    {
        rigidb = GetComponent<Rigidbody>();
    }

    private void Start()
    {

    }

    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = transform.forward * direction.z + transform.right * direction.x;

        moveDirection = Vector3.ClampMagnitude(moveDirection, 1);
    }


    // Update is called once per frame
    void Update()
    {
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        //      rigidb.AddForce(moveDirection * MovementForce, ForceMode.Force);
        Vector3 velocitathoritzontal = moveDirection* MovementForce;
        rigidb.velocity = new Vector3(velocitathoritzontal.x, rigidb.velocity.y, velocitathoritzontal.z);
    }
    private bool IsGrounded()
    {
        return Physics.Raycast(groundChecker.position, -groundChecker.up, out RaycastHit hitInfo, groundCheckerLength);
    }

    public void Jump()
     {
        rigidb.AddForce(transform.up * JumpforceAmount, ForceMode.Impulse);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(groundChecker.position, -groundChecker.up * groundCheckerLength);
    }
}
