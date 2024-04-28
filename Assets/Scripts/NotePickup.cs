using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePickup : MonoBehaviour
{
    private bool isPlayerInRange = false; //variable booleana para saber si el jugador está cerca de la nota
    private bool noteCollected = false; //variable booleana para saber si la nota ha sido recolectada

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && !noteCollected && Input.GetKeyDown(KeyCode.E)) //si el jugador está cerca de la nota, la nota no ha sido recolectada y el jugador presiona la tecla E
        {
            CollectNote(); //llama a la función CollectNote
        }
    }

    void OnTriggerEnter(Collider other) //cuando el jugador entra en el área de la nota
    {
        if (other.CompareTag("Player")) //si el objeto que entra en el área es el jugador
        {
            isPlayerInRange = true; //la variable isPlayerInRange se vuelve verdadera
        }
    }

    void OnTriggerExit(Collider other) //cuando el jugador sale del área de la nota
    {
        if (other.CompareTag("Player")) //si el objeto que sale del área es el jugador
        {
            isPlayerInRange = false; //la variable isPlayerInRange se vuelve falsa
        }
    }

    void CollectNote()
    {
        //Acá tenemos que agregar un script de sonido para cuando se agarra

        // Desactiva el objeto de la nota para que desaparezca
        gameObject.SetActive(false);

        // Marca la nota como recolectada para evitar que se recolecte múltiples veces
        noteCollected = true;
    }
}
