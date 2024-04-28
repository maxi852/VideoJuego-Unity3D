using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // Objeto 3D que la cámara seguirá
    public Vector3 offset; // Posición relativa de la cámara al objetivo
    public float rotationSpeed = 5.0f; // Velocidad de rotación de la cámara
    public float verticalRotationLimit = 80.0f; // Límite de rotación vertical de la cámara

    private float RotationHor = 0.0f; // Rotación horizontal de la cámara
    private float RotationVer = 0.0f; // Rotación vertical de la cámara

    private void LateUpdate()
    {
        if (!target)
            return;

        // Rotamos la cámara horizontalmente según el movimiento del mouse en X
        RotationHor += rotationSpeed * Input.GetAxis("Mouse X");

        // Limitamos la rotación vertical de la cámara entre -verticalRotationLimit y verticalRotationLimit
        RotationVer -= rotationSpeed * Input.GetAxis("Mouse Y");
        RotationVer = Mathf.Clamp(RotationVer, -verticalRotationLimit, verticalRotationLimit);

        // Aplicamos la rotación a la cámara
        transform.rotation = Quaternion.Euler(RotationVer, RotationHor, 0.0f);

        // Calculamos la posición de la cámara
        Vector3 desiredPosition = target.position + offset;

        // Movemos la cámara hacia la posición calculada
        transform.position = desiredPosition;
    }
}
