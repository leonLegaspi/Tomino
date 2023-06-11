using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovPlayer : MonoBehaviour
{
    private CharacterController characterController;
    public new Transform camera;

    [SerializeField] private float velocidadDeMovimiento;
    [SerializeField] private float velocidadSprint;
    [SerializeField] private float suavizadoMovimiento;
    [SerializeField] private float radio;
    

    private float gravity = -11.8f;
  
    public Transform checkGround;

    private Vector3 playerVelocity;

    [SerializeField] private LayerMask Ground;

    [SerializeField] private AudioSource caminar;
    [SerializeField] private AudioClip [] audioCaminar;

   

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        velocidadSprint = velocidadDeMovimiento * velocidadSprint;
    }

    private void Update()
    {   
        Movimiento();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void Movimiento()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        Vector3 movement = Vector3.zero;
        float movementSpeed = 2;

        RaycastHit hit;
        Debug.DrawRay(checkGround.position, Vector3.down, Color.red);

        if (Physics.Raycast(checkGround.position, Vector3.down, out hit, Ground.value))
        {
            if (hit.distance > 0.1f)
            {
                playerVelocity.y += gravity * Time.deltaTime;
            }
        }

        if(hor != 0 || ver != 0)
        {
            int randomIndex = Random.Range(0, audioCaminar.Length);
            AudioClip randomClip = audioCaminar[randomIndex];
            if(!caminar.isPlaying)
            {
                caminar.clip = randomClip;
                caminar.Play();
            }
            
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

        characterController.Move(playerVelocity * Time.deltaTime);
        characterController.Move(movement);
    }
}
