using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Vector2 sensitivity;

    public PlayerMovement activarMovimiento;

    private void Start()
    {
        activarMovimiento.movimiento = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        float horMouse = Input.GetAxisRaw("Mouse X");
        float verMouse = Input.GetAxisRaw("Mouse Y");
    }
}
