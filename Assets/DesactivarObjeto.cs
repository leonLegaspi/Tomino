using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarObjeto : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            obj.SetActive(false);
        }
    }
}
