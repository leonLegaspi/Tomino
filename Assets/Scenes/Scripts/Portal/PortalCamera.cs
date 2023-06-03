using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera, portal, otherPortal;

    private void Update()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position; // Desplazamiento
        transform.position = portal.position + playerOffsetFromPortal;
        
        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation); // Diferencia de angulos

        Quaternion portalRotationDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
        Vector3 newCameraDirection = portalRotationDifference * playerCamera.forward; //Obtengo la direccion en la que debo mirar
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }

}
