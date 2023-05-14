using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarBoxCollider : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;

    public void Activar()
    {
       boxCollider.gameObject.SetActive(true);
    }

    public void Desactivar()
    {
        boxCollider.gameObject.SetActive(false);
    }
}
