using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    private Vector2 angle = new Vector2(-90f * Mathf.Deg2Rad, 0);
    private new Camera camera;
    private Vector2 nearPlaneSize;

    public Transform follow;
    public float maxDistance;
    public Vector2 sensitivity;

     public PlayerMovement activarMovimiento;

    private void Start()
    {
        activarMovimiento.movimiento = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        camera = GetComponent<Camera>();
        CalculateNearPlaneSize();
    }
    private void Update()
    {
        float horMouse = Input.GetAxisRaw("Mouse X");
        float verMouse = Input.GetAxisRaw("Mouse Y");

        if(horMouse != 0)
        {
            angle.x += horMouse * Mathf.Deg2Rad * sensitivity.x;
        }
        if(verMouse != 0)
        {
            angle.y += verMouse * Mathf.Deg2Rad * sensitivity.y;
            angle.y = Mathf.Clamp(angle.y , -70 * Mathf.Deg2Rad, 40 * Mathf.Deg2Rad);
        }
    }

    private void LateUpdate()
    {
        Vector3 direction = new Vector3(Mathf.Cos(angle.x) * Mathf.Cos(angle.y), -Mathf.Sin(angle.y), -Mathf.Sin(angle.x) * Mathf.Cos(angle.y));
        
        RaycastHit hit;
        float distance = maxDistance;
        Vector3 [] points = GetCameraCollisionPoints(direction);

        foreach(Vector3 point in points)
        {
            if(Physics.Raycast(point, direction, out hit, maxDistance))
            {
                distance = Mathf.Min((hit.point - follow.position).magnitude, distance);    
            }
        }
        
        transform.position = follow.position + direction * distance;
        transform.rotation = Quaternion.LookRotation(follow.position - transform.position);
    }
 
    private void CalculateNearPlaneSize()
    {
        float height = Mathf.Tan(camera.fieldOfView * Mathf.Rad2Deg/2) * camera.nearClipPlane;
        float width = height * camera.aspect;

        nearPlaneSize = new Vector2(width, height);
    }
   
    private Vector3[] GetCameraCollisionPoints(Vector3 direction)
    {
        Vector3 position = follow.position;
        Vector3 center = position + direction * (camera.nearClipPlane + 0.2f);

        Vector3 right = transform.right * nearPlaneSize.x;
        Vector3 up = transform.up * nearPlaneSize.y;

        return new Vector3[] 
        {
            center - right + up, 
            center + right + up, 
            center - right - up, 
            center + right - up 
        };
    } 
}
