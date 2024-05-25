using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotePickup : MonoBehaviour
{
    private bool isPlayerInRange = false; //variable booleana para saber si el jugador está cerca de la nota
    private bool noteCollected = false; //variable booleana para saber si la nota ha sido recolectada
    private string note; 
    private char[] word = new char[3];
    private string completeWord;
    public Text claveTexto;
    private GameObject go;
    // Update is called once per frame
void Start()
    {
        go = GetComponent<GameObject>();
        word[0] = '-';
        word[1] = '-';
        word[2] = '-';
        completeWord = "";
        for (int i = 0; i < word.Length; i++)
            {
                completeWord += word[i];
            }
        claveTexto.text = completeWord;
        
    }

    void Update()
    {
        
        if (isPlayerInRange && !noteCollected && Input.GetKeyDown(KeyCode.E)) //si el jugador está cerca de la nota, la nota no ha sido recolectada y el jugador presiona la tecla E
        {

            Debug.Log(note);
            CollectNote(note); //llama a la función CollectNote
        }
    }

    void OnTriggerEnter(Collider other) //cuando el jugador entra en el área de la nota
    {
        if (other.CompareTag("Player")) //si el objeto que entra en el área es el jugador
        {
            string objetoColisionado;

            switch (this.gameObject.tag)
            {
                case "n1":
                    note = "n1";
                    objetoColisionado = "n1";
                    break;
                case "n2":
                    objetoColisionado = "n2";
                    break;
                case "n3":
                    objetoColisionado = "n3";
                    break;
                default:
                    objetoColisionado = "Objeto desconocido";
                    break;
            }

            Debug.Log(objetoColisionado + " ha colisionado con humano");
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

    void CollectNote(string note)
    {
        //Acá tenemos que agregar un script de sonido para cuando se agarra

        // Desactiva el objeto de la nota para que desaparezca
        gameObject.SetActive(false);
        Debug.Log("collect");
        // Marca la nota como recolectada para evitar que se recolecte múltiples veces
        noteCollected = true;
        if (note == "n1"){
            
            word[1] = 'o';
            completeWord = "";
            for (int i = 0; i < word.Length; i++)
            {
                completeWord += word[i];
            }
            Debug.Log(completeWord);
            claveTexto.text = completeWord;
        }
    }
}
