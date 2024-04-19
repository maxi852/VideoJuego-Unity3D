using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Velocidad de movimiento del personaje
    public float speed = 5.0f;

    public float runSpeedMultiplier = 3.0f; // Multiplicador de velocidad al correr
    private bool isRunning = false; // Indica si el personaje est� corriendo

    private void Start()
    {
        Cursor.visible = false; // para que no se vea el mouse
    }

    private void Update() 
    {
        // Capturamos la entrada de teclado
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

         // Verificar si se está corriendo (SHIFT + W)
        if (Input.GetKey(KeyCode.LeftShift) && verticalInput > 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        // Calculamos la dirección de movimiento basada en la rotación de la cámara
        Vector3 moveDirection = (verticalInput * Camera.main.transform.forward + horizontalInput * Camera.main.transform.right).normalized;

        // Aplicar multiplicador de velocidad si está corriendo
        float currentSpeed = isRunning ? speed * runSpeedMultiplier : speed;

        // Movemos al personaje en la dirección calculada
        transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);
    }
}
