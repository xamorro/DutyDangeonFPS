using UnityEngine;

public class PlayerInputHandlerCC : MonoBehaviour
{
    private PlayerMovement movementController;
    //private LookController lookController;
    private ShootController shootController;
    private Pausa pause;


    private void Start()
    {
        movementController = GetComponent<PlayerMovement>();
        //lookController = GetComponentInChildren<LookController>();
        shootController = GetComponentInChildren<ShootController>();
        pause = GameObject.Find("CanvasJOC").GetComponent<Pausa>();
    }


    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            movementController?.Jump();
        }

        if (Input.GetButton("Fire1"))
        {
            shootController?.Shoot();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            shootController?.Apuntar();
        }

        if (Input.GetButtonUp("Fire2"))
        {
            shootController?.Apuntar();
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            shootController?.HandleReload();    
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            pause?.MenuPausa();
        }

        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");
        movementController?.SetMoveDirection(new Vector3(xMovement, 0, zMovement));
    }
}