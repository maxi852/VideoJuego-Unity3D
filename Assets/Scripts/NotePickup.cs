using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotePickup : MonoBehaviour
{
    private bool isPlayerInRange = false; //variable booleana para saber si el jugador está cerca de la nota
    private bool noteCollected = false; //variable booleana para saber si la nota ha sido recolectada
    private string note;
    private char letter;
    private int position;
    private static char[] word = new char[8];
    private string completeWord;
    public Text claveTexto;
    private GameObject go;
    // Update is called once per frame
void Start()
    {
        go = GetComponent<GameObject>();
        word[0] = '_';
        word[1] = '_';
        word[2] = '_';
        word[3] = '_';
        word[4] = '_';
        word[5] = '_';
        word[6] = '_';
        word[7] = '_';
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
            CollectNote(letter,position); //llama a la función CollectNote
        }
    }

    void OnTriggerEnter(Collider other) //cuando el jugador entra en el área de la nota
    {
        if (other.CompareTag("Player")) //si el objeto que entra en el área es el jugador
        {
            switch (this.gameObject.tag)
            {
                case "n1":
                    letter = 'U';
                    position = 2;
                    break;
                case "n2":
                    letter = 'n';
                    position = 3;
                    break;
                case "n3":
                    letter = 'D';
                    position = 4;
                    break;
                case "n4":
                    letter = 's';
                    position = 1;
                    break;
                case "n5":
                    letter = 'i';
                    position = 5;
                    break;
                case "n6":
                    letter = 'z';
                    position = 7;
                    break;
                case "n7":
                    letter = 'E';
                    position = 0;
                    break;
                default:
                    letter = 'e';
                    position = 6;
                    break;
            }

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

    void CollectNote(char letter, int position)
    {
        //Acá tenemos que agregar un script de sonido para cuando se agarra

        // Desactiva el objeto de la nota para que desaparezca
        gameObject.SetActive(false);
        Debug.Log("collect");
        // Marca la nota como recolectada para evitar que se recolecte múltiples veces
        noteCollected = true;

        //Actualizo el texto con las letras
        word[position] = letter;
        completeWord = "";
        for (int i = 0; i < word.Length; i++)
        {
            completeWord += word[i];
        }
        Debug.Log(completeWord);
        claveTexto.text = completeWord;
    }
}
