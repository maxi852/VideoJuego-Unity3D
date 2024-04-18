using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightFollow : MonoBehaviour
{
    public Transform target; // Objeto 3D que la linterna seguir�
    public Vector3 offset; // Posici�n relativa de la linterna al objetivo

    private void LateUpdate()
    {
        if (!target)
            return;

        // Calculamos la posici�n deseada de la linterna relativa al personaje
        Vector3 desiredPosition = target.position + target.forward * offset.z + target.up * offset.y + target.right * offset.x;

        // Movemos la linterna hacia la posici�n deseada
        transform.position = desiredPosition;

        // La direcci�n de la linterna apunta hacia adelante (igual que el personaje)
        transform.forward = target.forward;
    }
}
