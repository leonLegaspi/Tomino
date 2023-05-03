using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSensor : MonoBehaviour
{
    private int contador;
    private bool entre;
    [SerializeField] private PlayerMovement player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            contador++;
            player.ContactCount(contador);
            entre = true;
            Debug.Log(contador);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground") && entre)
        {
            contador--;
            player.ContactCount(contador);
            entre = false;
            Debug.Log(contador);
        }
    }
}
