using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Screamer : MonoBehaviour
{
    [SerializeField] private GameObject ObjectScreamer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            SceneManager.LoadScene(2);
        }
    }
}
