using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Transform target; // Objeto 3D que el spotlight seguir�
    public Transform target2;
public Vector3 offset; // Posición relativa de la cámara al objetivo
    private void Start()
    {
        target = GetComponent<Transform>(); 
    }

    private void LateUpdate()
    {
        float rotationSpeed = 2.0f; // Puedes ajustar la velocidad de rotaci�n seg�n sea necesario
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.RotateAround(target.position, Vector3.up, mouseX);
    }
}
