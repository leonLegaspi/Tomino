using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifePlayer : MonoBehaviour
{
    [SerializeField] private Image efectoSangre;

    private float r, g, b, a;

    private void Start()
    {
        r = efectoSangre.color.r;
        g = efectoSangre.color.g;
        b = efectoSangre.color.b;
        a = efectoSangre.color.a;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Arma"))
        {
            a += 0.3f; 
        }
    }
    private void Update()
    {
        if (a >= 1)
            SceneManager.LoadScene(2);

        Debug.Log(a);
        a -= 0.0003f;
        a = Mathf.Clamp(a, 0, 1f);
        ChangeColor();
    }
    private void ChangeColor()
    {
        Color c = new Color(r, g, b, a);

        efectoSangre.color = c;
    }
}
