using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpcionesMenu : MonoBehaviour
{
    public void VolverMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Asegúrate de que "MainMenu" esté en la Build Settings
    }
}