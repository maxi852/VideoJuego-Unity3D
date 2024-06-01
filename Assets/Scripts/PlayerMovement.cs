using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float runSpeedMultiplier = 3.0f;
    private bool isRunning = false;
    private bool isWalking = false;
    private bool isJumping = false; // Indica si el personaje está saltando
    private bool isOnGround = false; // Indica si el personaje está en el suelo
    private bool isFallen = false;
    public Rigidbody rb;
    private Animator animator;
    private AudioSource audioSource;

    public AudioClip walkSound;
    public AudioClip runSound;
    public AudioClip jumpSound; // Sonido de salto

    public float jumpForce = 300.0f; // Fuerza del salto
    public Transform groundCheck; // Punto de chequeo para detectar si el personaje está en el suelo
    public LayerMask groundMask; // Capa de suelo

    private void Start()
    {
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        isOnGround = Physics.CheckSphere(groundCheck.position, 0.1f, groundMask); // Verificar si el personaje está en el suelo

        if (Input.GetKey(KeyCode.LeftShift) && verticalInput > 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (verticalInput > 0 && !isRunning)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        if (isOnGround && Input.GetKeyDown(KeyCode.Space)) // Verificar si el personaje está en el suelo y se presionó la barra espaciadora
        {
            isJumping = true;
            rb.AddForce(Vector3.up * jumpForce); // Aplicar una fuerza hacia arriba para el salto
            audioSource.clip = jumpSound; // Configurar el sonido de salto
            audioSource.Play(); // Reproducir el sonido de salto
        }

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping); // Configurar el parámetro de animación de salto

        // Controlar el sonido de caminar y correr
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
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        Vector3 moveDirection = (verticalInput * Camera.main.transform.forward + horizontalInput * Camera.main.transform.right).normalized;
        float currentSpeed = isRunning ? speed * runSpeedMultiplier : speed;
        transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);

        if (Input.GetKeyDown(KeyCode.G) && isFallen)
        {
            transform.rotation = Quaternion.identity;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            isFallen = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle1"))
        {
            rb.constraints = RigidbodyConstraints.None;
            isFallen = true;
        }
    }
}
