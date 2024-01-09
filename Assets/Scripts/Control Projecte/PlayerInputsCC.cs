using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandlerCC : MonoBehaviour
{
    private PlayerMovement movementController;
    private MouseLook mouseLook;
    private ShootController shootController;
    private Pausa pause;

    private PlayerController playerControls;

    private void Awake()
    {
        mouseLook = GetComponentInChildren<MouseLook>();
    }

    private void OnEnable()
    {
        playerControls = new PlayerController();
        playerControls.HumanControls.Enable();

        playerControls.HumanControls.Camera.performed += CameraMove;
        playerControls.HumanControls.Camera.canceled += CameraMove;

    }
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

    private void CameraMove(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();

        if (context.control.device is Gamepad)
        {
            inputVector *= Time.deltaTime;
        }

        mouseLook.SetCameraMoveDirection(new Vector2(inputVector.x, inputVector.y));




        //throw new NotImplementedException();
    }

    private void OnDisable()
    {
        playerControls.HumanControls.Camera.performed -= CameraMove;
        playerControls.HumanControls.Camera.canceled -= CameraMove;
        playerControls.HumanControls.Disable();
    }
}