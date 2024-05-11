using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    public void Update()
    {
        //Pausar y despausar
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
        }

        //Reiniciar
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //Cerrar
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit(); 
        }
    }
}
