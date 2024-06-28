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

    public float tiempoTranscurrido = 0.0f;

    public PlayerMovement playerMov;

    //Perdiste el juego
    public GameObject blackScreen;
    public GameObject gameOverText;

    //Sonidos de correr del enemigo
    private AudioSource audioSource;
    public AudioClip enemyRun;
    private bool isRunningSoundPlaying = false;

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
    }

    void Update()
    {
        if (!isChasing)
        {
            tiempoTranscurrido += Time.deltaTime;
        }
        //El enemigo esta corriendo al personaje.
        else if (!die)
        {
            // Si no hay un GameObject objetivo definido, salir
            if (targetObject == null) return;

            // calcular la posicion del personaje principal para saber hacia donde ir
            Vector3 direction = targetObject.transform.position - transform.position;

            // La velocidad se mantenga
            direction = direction.normalized;

            // Perseguir al personaje principal
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            //El enemigo rote y nos persiga siempre de frente
            transform.LookAt(targetObject.transform);

            if (!isRunningSoundPlaying)
            {
                audioSource.Play();
                Debug.Log("Reproduciendo sonido de correr");
                isRunningSoundPlaying = true;
            }
            else
            {
                if (isRunningSoundPlaying) //Detener el sonido cuando el enemigo ataca
                {
                    audioSource.Stop();
                    Debug.Log("Deteniendo sonido de correr");
                    isRunningSoundPlaying = false;
                }
            }

        }

        if (tiempoTranscurrido >= 1.0f)
        {
            isChasing = true;
            tiempoTranscurrido = 0.0f;
            float randomX;
            float randomZ = UnityEngine.Random.Range(12.0f, 560.0f);
            Debug.Log(randomZ);
            if (randomZ < 226.0f)
            {
                randomX = UnityEngine.Random.Range(-73.0f, 162.0f);
            }
            else
            {
                randomX = UnityEngine.Random.Range(-73.0f, 300.0f);
            }
            float randomY = 0.0f;

            //transform.position = new Vector3(40.0f, 0.0f, -3.0f);
            //transform.position = new Vector3(randomX, randomY, randomZ);  
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerMov.canMove = false;
            die = true;
            transform.Translate(Vector3.zero);
            ani.SetBool("run", false);
            ani.SetBool("idle", true);

            //targetObject.transform.LookAt(gameObject.transform);

            //mainCamera.transform.LookAt(transform);

            //Vector3 direction = (transform.position - mainCamera.transform.position).normalized;
            //Quaternion lookRotation = Quaternion.LookRotation(direction);
            //mainCamera.transform.rotation = lookRotation;

            //Mandar al personaje posicion donde no hay nada
            //targetObject.transform.position = new Vector3(4f, 2f, -214f);

            //audio
            if (isRunningSoundPlaying) //Detener el sonido cuando el enemigo ataca
            {
                audioSource.Stop();
                Debug.Log("Deteniendo sonido de correr x ataque");
                isRunningSoundPlaying = false;
            }

            Vector3 positionInFront = targetObject.transform.position + targetObject.transform.forward * distanceInFront;
            Debug.Log(positionInFront + " pos");
            Debug.Log(targetObject.transform.forward + " pos");
            transform.position = positionInFront;
            transform.LookAt(targetObject.transform);
            ani.SetBool("idle", false);
            ani.SetBool("Attack1", true);
            // Mostrar pantalla negra y texto de "Perdiste"
            StartCoroutine(ShowGameOverScreen());
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