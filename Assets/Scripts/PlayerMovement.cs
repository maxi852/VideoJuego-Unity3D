using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Velocidad de movimiento del personaje
    public float speed = 5.0f;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        // Capturamos la entrada de teclado
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculamos la direcci�n de movimiento basada en la rotaci�n de la c�mara
        Vector3 moveDirection = (verticalInput * Camera.main.transform.forward + horizontalInput * Camera.main.transform.right).normalized;

        // Movemos al personaje en la direcci�n calculada
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }
}
