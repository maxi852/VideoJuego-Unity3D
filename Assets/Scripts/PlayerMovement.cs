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

    public bool canMove = true;

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
        Debug.Log("canMove" + canMove);
        isOnGround = Physics.CheckSphere(groundCheck.position, 0.2f, groundMask); // Verificar si el personaje está en el suelo

        if (canMove)
        {
            Debug.Log("la wataa");
            // Detectar si el jugador está corriendo
            if (Input.GetKey(KeyCode.LeftShift) && (Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0))
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }
            
        
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
                rb.AddForce(Vector3.up * jumpForce); // Aplicar una fuerza hacia arriba para el salto
                animator.Play("Jump");

            }
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
            } else if (isJumping)
            {
                if (audioSource.clip != jumpSound)
                {
                    Debug.Log("if");
                    audioSource.clip = jumpSound; // Configurar el sonido de salto
                    audioSource.Play(); // Reproducir el sonido de salto
                    isJumping = false;
                }
                else if (!audioSource.isPlaying)
                {
                    Debug.Log("elseif");
                    audioSource.Play();
                    isJumping = false;
                }

            }
        }
        else if (isJumping)
        {
            if (audioSource.clip != jumpSound)
            {
                Debug.Log("if");
                audioSource.clip = jumpSound; // Configurar el sonido de salto
                audioSource.Play(); // Reproducir el sonido de salto
                isJumping = false;
            }
            else if (!audioSource.isPlaying)
            {
                Debug.Log("elseif");
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

        if (canMove)
        {
            Vector3 moveDirection = (verticalInput * Camera.main.transform.forward + horizontalInput * Camera.main.transform.right).normalized; //Para calcular direccion de movimiento del jugador
            float currentSpeed = isRunning ? speed * runSpeedMultiplier : speed; //Determina velocidad actual del jugador
            rb.transform.Translate(moveDirection * currentSpeed * Time.fixedDeltaTime, Space.World); //Mueve al jugador a la direccion calculada con la velocidad calculada
        }
        

        //if (Input.GetKeyDown(KeyCode.G) && isFallen)
        //{
        //    transform.rotation = Quaternion.identity;
        //    rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        //    isFallen = false;
        //}
    }

    //void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.CompareTag("Obstacle1"))
    //    {
    //        rb.constraints = RigidbodyConstraints.None;
    //        isFallen = true;
    //    }
    //}
}