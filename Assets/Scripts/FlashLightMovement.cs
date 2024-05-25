using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightMovement : MonoBehaviour
{
    public Transform target; // Objeto 3D que el spotlight seguir�
    public Vector3 offset; // Posici�n relativa del spotlight al objetivo

    private Light spotlight; // Componente de luz del spotlight
    public float defaultIntensity = 5.0f; // Intensidad por defecto del spotlight (osea linterna prendida)
    public float modifiedIntensity = 0.0f; // Intensidad modificada del spotlight (osea linterna apagada)
    public float modifiedIntensityHouse = 0.0f; // Intensidad modificada del spotlight (osea linterna apagada)

    private Transform houseTransform; // Almacena la referencia al objeto "House"
    private Transform blake; // Almacena la referencia al objeto "Blake"
    private Transform trees; // Almacena la referencia al objeto "Blake"

    private float distanciaX; // Almacena la distancia en el eje X
    private bool near = false;

    private void Start()
    {
        spotlight = GetComponent<Light>(); //agarramos el componente de luz del spotlight
        spotlight.intensity = defaultIntensity; //inicializamos la intensidad del spotlight, osea prendida

        houseTransform = GameObject.Find("house_aband (1)").transform;
        blake = GameObject.Find("The Adventurer Blake").transform;
    }

    private void LateUpdate()
    {
        if (!target)
            return;

        // Calculamos la posici�n deseada del spotlight relativa al personaje --> seguir el personaje
        Vector3 desiredPosition = target.position + target.forward * offset.z + target.up * offset.y + target.right * offset.x;

        // Movemos el spotlight hacia la posici�n deseada --> seguir el personaje
        transform.position = desiredPosition;

        // La direcci�n del spotlight apunta hacia el personaje --> seguir el personaje
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

        distanciaX = Vector3.Distance(blake.position, houseTransform.position);
        if (distanciaX < 36)
        {
            spotlight.intensity = 1f;
            near = true;
        }
        else if (distanciaX < 42)
        {
            spotlight.intensity = 4f;
            near = true;
        }
        else if (distanciaX < 47)
        {
            spotlight.intensity = 7f;
            near = true;
        }
        else if (distanciaX < 52)
        {
            spotlight.intensity = 10f;
            near = true;
        }
        else if (distanciaX < 56)
        {
            spotlight.intensity = modifiedIntensityHouse;
            near = true;
        } else
        {
            if (near) 
            {
                spotlight.intensity = (spotlight.intensity == defaultIntensity) ? modifiedIntensity : defaultIntensity;
                near = false;
            }
            
        }

    }
}
