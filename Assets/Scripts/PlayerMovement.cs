using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; // Velocidad de movimiento
    public float runSpeedMultiplier = 3.0f; // Multiplicador de velocidad para correr
    private bool isRunning = false; // Indica si el personaje está corriendo
    private bool isWalking = false; // Indica si el personaje está caminando
    private bool isJumping = false; // Indica si el personaje está saltando
    private bool isOnGround = false; // Indica si el personaje está en el suelo
    //private bool isFallen = false; // Indica si el personaje ha caído
    public Rigidbody rb; // Componente Rigidbody del personaje
    private Animator animator; // Componente Animator del personaje
    private AudioSource audioSource; // Componente AudioSource del personaje

    public AudioClip walkSound; // Sonido de caminar
    public AudioClip runSound; // Sonido de correr
    public AudioClip jumpSound; // Sonido de salto
    public AudioClip dieSound; // Sonido de muerte

    public float jumpForce = 600.0f; // Fuerza del salto
     public float fallSpeed = 100.0f; // Velocidad de caída
    public Transform groundCheck; // Punto de chequeo para detectar si el personaje está en el suelo
    public LayerMask groundMask; // Capa de suelo

    public bool canMove = true;

    public GameObject Win;
    public GameObject BlackScreen;
    private void Start()
    {
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        jumpForce = 20f;

        if (Win != null)
        {
            Win.SetActive(false);
            BlackScreen.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        animator.SetFloat("VelX", horizontalInput);
        animator.SetFloat("VelY", verticalInput);
        isOnGround = Physics.CheckSphere(groundCheck.position, 0.2f, groundMask); // Verificar si el personaje está en el suelo

        if (canMove) //animaciones
        {
            // Detectar si el jugador está corriendo sea en A S W D
            if (Input.GetKey(KeyCode.LeftShift) && (Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0))
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }

            // Detectar si el jugador está caminando sea en A S W D
            if (Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0)
            {
                isWalking = !isRunning;
            }
            else
            {
                isWalking = false;
            }

            if (isOnGround && Input.GetKeyDown(KeyCode.Space)) // Verificar si el personaje está en el suelo y se presionó la barra espaciadora
            {
                isJumping = true;
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z); // Ajustar la velocidad vertical para el salto
                animator.Play("Jump");
            }
        }

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping); // Configurar el parámetro de animación de salto

        // Controlar el sonido de caminar y correr
        if (canMove)
        {
            if (isWalking)
            {
                if (audioSource.clip != walkSound)
                {
                    audioSource.clip = walkSound;
                    audioSource.Play();
                }
                else if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else if (isRunning)
            {
                if (audioSource.clip != runSound)
                {
                    audioSource.clip = runSound;
                    audioSource.Play();
                }
                else if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else if (isJumping)
            {
                if (audioSource.clip != jumpSound)
                {
                    audioSource.clip = jumpSound; // Configurar el sonido de salto
                    audioSource.Play(); // Reproducir el sonido de salto
                    isJumping = false;
                }
                else if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                    isJumping = false;
                }
            }
            else
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }
        }

        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        if (canMove) //Script que hace que el pj se mueva
        {
            Vector3 moveDirection = (verticalInput * Camera.main.transform.forward + horizontalInput * Camera.main.transform.right).normalized; //Para calcular direccion de movimiento del jugador
            float currentSpeed = isRunning ? speed * runSpeedMultiplier : speed; //Determina velocidad actual del jugador
            rb.transform.Translate(moveDirection * currentSpeed * Time.fixedDeltaTime, Space.World); //Mueve al jugador a la direccion calculada con la velocidad calculada
        }

        if (rb.velocity.y < 0)
        {
            rb.AddForce(Vector3.down * fallSpeed, ForceMode.Acceleration);
        }
        //if (Input.GetKeyDown(KeyCode.G) && isFallen)
        //{
        //    transform.rotation = Quaternion.identity;
        //    rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        //    isFallen = false;
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SpotyGirl"))
        {
            Win.SetActive(true);
            BlackScreen.SetActive(true);
            canMove = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) //si el objeto que entra en el área es el jugador
        {
            Debug.Log("d");
            audioSource.clip = dieSound; // Configurar el sonido de salto
            audioSource.Play(); // Reproducir el sonido de salto
        }
    }
    }