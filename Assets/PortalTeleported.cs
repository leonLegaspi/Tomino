using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleported : MonoBehaviour
{
    public Transform player, receiver;
    private bool playerIsOverlappng = false;

    private void Update()
    {
        if(playerIsOverlappng)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            //  si esto es verdadero el jugador se esta moviendo a traves del portal
            if(dotProduct < 0f)
            {
                //teleporto aca

                float rotationDiff = Quaternion.Angle(transform.rotation, receiver.rotation);
                rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                //Desplazamiento entre el jugador y el portal
                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = receiver.position + positionOffset;

                playerIsOverlappng = false;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerIsOverlappng = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            playerIsOverlappng = false;
        }
    }

}
