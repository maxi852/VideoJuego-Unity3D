using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightMovement : MonoBehaviour
{
    public Transform target; // Objeto 3D que el spotlight seguir�
    public Vector3 offset; // Posici�n relativa del spotlight al objetivo

    private Light spotlight; 
    public float defaultIntensity = 30.0f; 
    public float modifiedIntensity = 0.0f;

    private void Start()
    {
        spotlight = GetComponent<Light>();
        spotlight.intensity = defaultIntensity;
    }

    private void LateUpdate()
    {
        if (!target)
            return;

        // Calculamos la posici�n deseada del spotlight relativa al personaje
        Vector3 desiredPosition = target.position + target.forward * offset.z + target.up * offset.y + target.right * offset.x;

        // Movemos el spotlight hacia la posici�n deseada
        transform.position = desiredPosition;

        // La direcci�n del spotlight apunta hacia el personaje
        transform.LookAt(target);

        // Rotamos el spotlight horizontalmente seg�n el movimiento del mouse
        float rotationSpeed = 2.0f; // Puedes ajustar la velocidad de rotaci�n seg�n sea necesario
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.RotateAround(target.position, Vector3.up, mouseX);
        if (Input.GetKeyDown(KeyCode.F))

        {
            // condicion para ver en que intensidad se encuentra la lintera y modificar a la otra opcion
            spotlight.intensity = (spotlight.intensity == defaultIntensity) ? modifiedIntensity : defaultIntensity;
        }
    }
}
