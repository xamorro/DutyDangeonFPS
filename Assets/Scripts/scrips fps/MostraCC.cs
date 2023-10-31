using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostraCC : MonoBehaviour
{
    [SerializeField] private float speed;

    private CharacterController cc;
    private Vector3 movementVector = Vector3.zero;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.z = Input.GetAxis("Vertical");

        movementVector = Vector3.ClampMagnitude(movementVector, 1);

        cc.Move(movementVector * speed * Time.deltaTime);
    }

}
