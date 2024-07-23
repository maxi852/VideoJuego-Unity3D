using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyScript : MonoBehaviour
{
    // Referencia al GameObject1
    public GameObject targetObject;

    public Animator ani;

    // Velocidad de movimiento
    public float speed = 5f;
    //public float rotationSpeed = 5.0f;
    private bool isChasing = false; // Indica si el personaje est� corriendo
    private bool die = false; // Indica si el personaje est� corriendo
    public float distanceInFront = 10.0f;

    public PlayerMovement playerMov;

    //Cantida de notas recogidas
    public int notesPickedUp = 0;

    //Tomo al enemigo
    public GameObject enemy;

    //Perdiste el juego
    public GameObject blackScreen;
    public GameObject gameOverText;

    //Sonidos de correr del enemigo
    private AudioSource audioSource;
    public AudioClip enemyRun;
    private bool isRunningSoundPlaying = false;

    private float distancePlayer; // Distancia con el player

    public GameObject deathPanel;

    public CameraMovement cameraMov;

    void Start()
    {
        ani = GetComponent<Animator>();
        ani.SetBool("run", true);

        //Pantalla negra de que perdiste
        blackScreen.SetActive(false); //Ocultar pantalla negra
        gameOverText.SetActive(false); //Ocultar texto de game over

        //AudioSource de correr enemigo
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = enemyRun;
        audioSource.loop = true;

        //Velocidad inicial del enemigo
        speed = 15f;
        //transform.position = new Vector3(230.0f, 0.0f, 200.0f);
        transform.position = new Vector3(-12.83f,-100.0f, 30.14f);

        deathPanel.SetActive(false);

    }

    void Update()
    {
        if (notesPickedUp == 8)
        {
            enemy.SetActive(false);   
        }
        else if (!isChasing)
        {
            StartCoroutine(SetEnemyPosition('r')); //Funcion con parametro R, es el tiempo que va a estar esperando para salir
        }
        //El enemigo esta corriendo al personaje.
        else if (!die)
        {
            Debug.Log("chasing");
            // Si no hay un GameObject objetivo definido, salir
            if (targetObject == null) return; // a modo de precaución

            // calcular la posicion del personaje principal para saber hacia donde ir
            Vector3 direction = targetObject.transform.position - transform.position;

            // La velocidad se mantenga
            direction = direction.normalized;

            // Perseguir al personaje principal
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            //El enemigo rote y nos persiga siempre de frente
            transform.LookAt(targetObject.transform);

            StartCoroutine(SetEnemyPosition('w'));//Funcion con parametro W, es el tiempo que te persigue y aumento de velocidad (es el else)

            distancePlayer = Vector3.Distance(transform.position, targetObject.transform.position);

            if (distancePlayer < 150)
            {
                Debug.Log("Cerca");
                if (!isRunningSoundPlaying)
                {
                    Debug.Log("H");
                    audioSource.Play();
                    isRunningSoundPlaying = true;
                }
            }
            else
            {
                if (isRunningSoundPlaying)
                {
                    audioSource.Stop();
                    isRunningSoundPlaying = false;
                }
            }
        }
    }
     IEnumerator SetEnemyPosition(char action) {
        switch (notesPickedUp)
        {
            case 0:
                if(action == 'r')
                {
                    if (isRunningSoundPlaying)
                    {
                        audioSource.Stop();
                        isRunningSoundPlaying = false;
                    }
                    yield return new WaitForSeconds(1.0f);
                    Debug.Log("aloha?");
                    
                    if (!isChasing)
                    {
                        //transform.position = new Vector3(230.0f, 0.0f, 200.0f);
                        transform.position = new Vector3(-12.83f, 0.0f, 30.14f);
                        isChasing = true;
                    }
                } else
                {
                    yield return new WaitForSeconds(40.0f);
                    isChasing = false;
                    transform.position = new Vector3(230.0f, 0.0f, 200.0f);
                } 
                break;
            case 1:
                if (action == 'r')
                {
                    if (isRunningSoundPlaying)
                    {
                        audioSource.Stop();
                        isRunningSoundPlaying = false;
                    }
                    yield return new WaitForSeconds(13.0f);
                    isChasing = true;
                }
                else
                {
                    yield return new WaitForSeconds(40.0f);
                    isChasing = false;
                    transform.position = new Vector3(230.0f, 0.0f, 200.0f);
                }
                break;
            case 2:
                if (action == 'r')
                {
                    if (isRunningSoundPlaying)
                    {
                        audioSource.Stop();
                        isRunningSoundPlaying = false;
                    }
                    yield return new WaitForSeconds(12.0f);
                    speed = 16f;
                    isChasing = true;
                }
                else
                {
                    yield return new WaitForSeconds(40.0f);
                    isChasing = false;
                    transform.position = new Vector3(230.0f, 0.0f, 200.0f);
                }
                break;
            case 3:
                if (action == 'r')
                {
                    if (isRunningSoundPlaying)
                    {
                        audioSource.Stop();
                        isRunningSoundPlaying = false;
                    }
                    yield return new WaitForSeconds(12.0f);
                    speed = 16f;
                    isChasing = true;
                }
                else
                {
                    yield return new WaitForSeconds(50.0f);
                    isChasing = false;
                    transform.position = new Vector3(230.0f, 0.0f, 200.0f);
                }
                break;
            case 4:
                if (action == 'r')
                {
                    if (isRunningSoundPlaying)
                    {
                        audioSource.Stop();
                        isRunningSoundPlaying = false;
                    }
                    yield return new WaitForSeconds(12.0f);
                    speed = 17f;
                    isChasing = true;
                }
                else
                {
                    yield return new WaitForSeconds(50.0f);
                    isChasing = false;
                    transform.position = new Vector3(230.0f, 0.0f, 200.0f);
                }
                break;
            case 5:
                if (action == 'r')
                {
                    if (isRunningSoundPlaying)
                    {
                        audioSource.Stop();
                        isRunningSoundPlaying = false;
                    }
                    yield return new WaitForSeconds(12.0f);
                    speed = 17f;
                    isChasing = true;
                }
                else
                {
                    yield return new WaitForSeconds(50.0f);
                    isChasing = false;
                    transform.position = new Vector3(230.0f, 0.0f, 200.0f);
                }
                break;
            case 6:
                if (action == 'r')
                {
                    if (isRunningSoundPlaying)
                    {
                        audioSource.Stop();
                        isRunningSoundPlaying = false;
                    }
                    yield return new WaitForSeconds(12.0f);
                    speed = 18f;
                    isChasing = true;
                }
                else
                {
                    yield return new WaitForSeconds(55.0f);
                    isChasing = false;
                    transform.position = new Vector3(230.0f, 0.0f, 200.0f);
                }
                break;
            case 7:
                if (action == 'r')
                {
                    if (isRunningSoundPlaying)
                    {
                        audioSource.Stop();
                        isRunningSoundPlaying = false;
                    }
                    yield return new WaitForSeconds(10.0f);
                    speed = 19f;
                    isChasing = true;
                }
                else
                {
                    yield return new WaitForSeconds(60.0f);
                    isChasing = false;
                    transform.position = new Vector3(230.0f, 0.0f, 200.0f);
                }
                break;
            default:
                transform.position = new Vector3(230.0f, 0.0f, 200.0f);
                break;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //si el objeto que entra en el área es el jugador
        {
            playerMov.canMove = false; //Detener el movimiento del jugador
            die = true; //El enemigo ataca
            transform.Translate(Vector3.zero); //Detener el movimiento del enemigo
            ani.SetBool("run", false); //Detener la animación de correr
            ani.SetBool("idle", true); //Activar la animación de idle

            //audio
            if (isRunningSoundPlaying) //Detener el sonido cuando el enemigo ataca
            {
                audioSource.Stop();
                //Debug.Log("Deteniendo sonido de correr x ataque");
                isRunningSoundPlaying = false;
            }

           

            Vector3 positionInFront = targetObject.transform.position + targetObject.transform.forward * distanceInFront; //Calcular la posición del enemigo en frente del jugador
           // Debug.Log(positionInFront + " pos"); 
           // Debug.Log(targetObject.transform.forward + " pos");

            transform.position = positionInFront; //Mover al enemigo a la posición calculada
            transform.LookAt(targetObject.transform); //Rotar al enemigo para que mire al jugador


            ani.SetBool("idle", false); //Desactivar la animación de idle
            ani.SetBool("Attack1", true); //Activar la animación de ataque

            deathPanel.SetActive(true);
            // Mostrar pantalla negra y texto de "Perdiste"
            //StartCoroutine(ShowGameOverScreen()); //llamamos a la función que muestra la pantalla de "Perdiste"
        }
        IEnumerator ShowGameOverScreen()
        {
            // Esperar un poco antes de mostrar la pantalla de "Perdiste" para permitir que se reproduzca la animación de ataque
            yield return new WaitForSeconds(2.0f);

            // Activar la pantalla negra y el texto de "Perdiste"
            blackScreen.SetActive(true);
            gameOverText.SetActive(true);
        }
    }
}