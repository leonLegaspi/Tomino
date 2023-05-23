using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementFisrtPerson : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool _grounded;

    public  Transform orientation;
    private float     _horizontalInput;
    private float     _verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public Transform grounCheck;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        //Check suelo
        _grounded = Physics.Raycast(grounCheck.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();

        //Drag
        if (_grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput   = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        //Calculo la direccion de movimiento 
        moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 5f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        //Limito la velocidad
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
