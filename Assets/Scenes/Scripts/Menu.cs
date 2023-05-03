using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject MenuPrincipal, MenuAjustes, MenuStaff, MenuInicio, MenuControles;
    public void Jugar()
    {
        SceneManager.LoadScene(1);
    }

    public void Ajustes()
    {
        MenuPrincipal.SetActive(false);
        MenuAjustes.SetActive(true);
    }

    public void VolverAjustes()
    {
        MenuPrincipal.SetActive(true);
        MenuAjustes.SetActive(false);
    }

    public void Staff()
    {
        MenuPrincipal.SetActive(false);
        MenuStaff.SetActive(true);
    }

    public void VolverStaff()
    {
        MenuPrincipal.SetActive(true);
        MenuStaff.SetActive(false);
    }

    public void Continuar()
    {
        MenuInicio.SetActive(false);
        MenuPrincipal.SetActive(true);
    }

    public void Controles()
    {
        MenuAjustes.SetActive(false);
        MenuControles.SetActive(true);
    }

    public void VolverControles()
    {
        MenuAjustes.SetActive(true);
        MenuControles.SetActive(false);
    }
    public void Salir()
    {
        Application.Quit();
    }
}
