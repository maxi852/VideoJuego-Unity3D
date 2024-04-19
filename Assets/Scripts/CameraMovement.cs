using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // Objeto 3D que la c�mara seguir�
    public Vector3 offset; // Posici�n relativa de la c�mara al objetivo
    public float rotationSpeed = 5.0f; // Velocidad de rotaci�n de la c�mara

    private float RotationHor = 0.0f; // Rotaci�n horizontal de la c�mara

    private void LateUpdate()
    {
        if (!target)
            return;

        //Rotamos la c�mara horizontalmente seg�n el movimiento del mouse
        RotationHor += rotationSpeed * Input.GetAxis("Mouse X");

        // Aplicamos la rotaci�n a la c�mara
        transform.rotation = Quaternion.Euler(0.0f, RotationHor, 0.0f);

        // Calculamos la posici�n de la c�mara
        Vector3 desiredPosition = target.position + offset;

        // Movemos la c�mara hacia la posici�n 
        transform.position = desiredPosition;
    }
}
