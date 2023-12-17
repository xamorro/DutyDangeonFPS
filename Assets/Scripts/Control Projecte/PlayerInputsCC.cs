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
        //Botar
        if (Input.GetButtonDown("Jump"))
        {
            movementController?.Jump();
        }

        //Disparar
        if (Input.GetButton("Fire1"))
        {
            shootController?.Shoot();
        }

        //Apuntar
        if (Input.GetButton("Fire2"))
        {
            shootController.AimIn();
        }

        //Desapuntar
        if (Input.GetButtonUp("Fire2"))
        {
            shootController.SetAimOut();
        }

        //Recargar
        if (Input.GetKeyDown(KeyCode.R))
        {
            shootController?.HandleReload();    
        }

        //Menu Pausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause?.MenuPausa();
        }

        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");
        movementController?.SetMoveDirection(new Vector3(xMovement, 0, zMovement));
    }
}