using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    public new Transform camera;
    private Animator animator;

    [SerializeField] private float velocidadDeMovimiento;
    [SerializeField] private float suavizadoMovimiento;
    [SerializeField] private float jumpForce;
    [SerializeField] private float radio;
    private bool isTouching;
    public bool activo;
    public bool movimiento = false;

    private float gravity = -11.8f;
  
    public Transform checkGround;

    private Vector3 playerVelocity;

    [SerializeField] private LayerMask Ground;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.Log(movimiento);
        if(movimiento)
        Movimiento();

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(3);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void ContactCount(int contador)
    {
        if(contador == 1)
        {
            isTouching = true;
            animator.SetBool("Saltar", !isTouching);
        }

        if (contador == 0)
        {
            isTouching = false;
            animator.SetBool("Saltar", !isTouching);
        }
    }

    private void Movimiento()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        Vector3 movement = Vector3.zero;
        float movementSpeed = 0f;

        RaycastHit hit;
        Debug.DrawRay(checkGround.position, Vector3.down, Color.red);

        if (Physics.Raycast(checkGround.position, Vector3.down, out hit, Ground.value))
        {
            if (hit.distance > 0.1f)
            {
                playerVelocity.y += gravity * Time.deltaTime;
            }
        }

        if (hor != 0 || ver != 0)
        {
            Vector3 forward = camera.forward;
            forward.y = 0;
            forward.Normalize();

            Vector3 right = camera.right;
            right.y = 0;
            forward.Normalize();

            Vector3 direction = forward * ver + right * hor;
            movementSpeed = Mathf.Clamp01(direction.magnitude);
            direction.Normalize();

            movement = direction * velocidadDeMovimiento * movementSpeed * Time.deltaTime;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), suavizadoMovimiento);
        }

        if (Input.GetButtonDown("Jump") && isTouching)
        {
            animator.SetBool("Saltar", isTouching);
            playerVelocity.y = Mathf.Sqrt(jumpForce * -3f * gravity);
        }

        characterController.Move(playerVelocity * Time.deltaTime);
        characterController.Move(movement);

        animator.SetFloat("Speed", movementSpeed);
    }
}
