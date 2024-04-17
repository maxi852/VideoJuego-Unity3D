using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // Objeto 3D que la cámara seguirá
    public Vector3 offset; // Posición relativa de la cámara al objetivo
    public float rotationSpeed = 5.0f; // Velocidad de rotación de la cámara

    private float RotationHor = 0.0f; // Rotación horizontal de la cámara

    private void LateUpdate()
    {
        if (!target)
            return;

        //Rotamos la cámara horizontalmente según el movimiento del mouse
        RotationHor += rotationSpeed * Input.GetAxis("Mouse X");

        // Aplicamos la rotación a la cámara
        transform.rotation = Quaternion.Euler(0.0f, RotationHor, 0.0f);

        // Calculamos la posición de la cámara
        Vector3 desiredPosition = target.position + offset;

        // Movemos la cámara hacia la posición 
        transform.position = desiredPosition;
    }
}
