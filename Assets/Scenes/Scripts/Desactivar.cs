using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desactivar : MonoBehaviour
{
    public GameObject prefabCamera, cameraCinematic, dolly, cm, cubo;
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Cinematica"))
        {
            Destrroy();
        }
    }
    void Destrroy()
    {
        cameraCinematic.gameObject.SetActive(false);
        dolly.gameObject.SetActive(false);
        cameraCinematic.gameObject.SetActive(false);
        prefabCamera.SetActive(true);
        Destroy(cubo);
    }
}
