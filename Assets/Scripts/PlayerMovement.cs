using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; // Velocidad de movimiento
    //public float RotationSpeed = 1.0f;
    public float runSpeedMultiplier = 3.0f; // Multiplicador de velocidad para correr
    private bool isRunning = false; // Indica si el personaje está corriendo
    private bool isWalking = false; // Indica si el personaje está caminando
    private bool isJumping = false; // Indica si el personaje está saltando
    private bool isOnGround = false; // Indica si el personaje está en el suelo
    private bool isFallen = false; // Indica si el personaje ha caído
    public Rigidbody rb; // Componente Rigidbody del personaje
    private Animator animator; // Componente Animator del personaje
    private AudioSource audioSource; // Componente AudioSource del personaje

    public AudioClip walkSound; // Sonido de caminar
    public AudioClip runSound; // Sonido de correr
    public AudioClip jumpSound; // Sonido de salto

    public float jumpForce = 100.0f; // Fuerza del salto
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

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        animator.SetFloat("VelX", horizontalInput);
        animator.SetFloat("VelY", verticalInput);


        isOnGround = Physics.CheckSphere(groundCheck.position, 0.2f, groundMask); // Verificar si el personaje está en el suelo
        Debug.Log("isOnGround: " + isOnGround); // Mensaje de depuración para verificar si el personaje está en el suelo

        if (Input.GetKey(KeyCode.LeftShift) && verticalInput > 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (Mathf.Abs(verticalInput) > 0 && !isRunning) // Usar valor absoluto para incluir tanto W como S
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
            animator.Play("Jump");
            //audioSource.clip = jumpSound; // Configurar el sonido de salto
            //audioSource.Play(); // Reproducir el sonido de salto

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
        rb.transform.Translate(moveDirection * currentSpeed * Time.fixedDeltaTime, Space.World);

        //float rotationY = Input.GetAxis("Mouse X");
        //rb.transform.Rotate(new Vector3(0, rotationY * Time.fixedDeltaTime * RotationSpeed, 0));

        if (Input.GetKeyDown(KeyCode.G) && isFallen)
        {
            transform.rotation = Quaternion.identity;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
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
