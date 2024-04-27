using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Libreria para poder usar SceneManager(cambiar de escenas)

public class MainMenu : MonoBehaviour
{
    public void EscenaJuego()
    {
        SceneManager.LoadScene("SampleScene"); //Cargo la escena SampleScene que es donde tengo el juego
    }

    public void Salir()
    {
        Application.Quit(); //Salir del juego
    }
}
