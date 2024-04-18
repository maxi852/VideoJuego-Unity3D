using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightFollow : MonoBehaviour
{
    public Transform target; // Objeto 3D que la linterna seguirá
    public Vector3 offset; // Posición relativa de la linterna al objetivo

    private void LateUpdate()
    {
        if (!target)
            return;

        // Calculamos la posición deseada de la linterna relativa al personaje
        Vector3 desiredPosition = target.position + target.forward * offset.z + target.up * offset.y + target.right * offset.x;

        // Movemos la linterna hacia la posición deseada
        transform.position = desiredPosition;

        // La dirección de la linterna apunta hacia adelante (igual que el personaje)
        transform.forward = target.forward;
    }
}
