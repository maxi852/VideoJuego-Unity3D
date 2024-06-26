using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator ani;
    public Quaternion angulo;
    public float grado;

    void Start()
    {
        ani = GetComponent<Animator>();
    }
    public void Comportamiento()
    {
        cronometro += Time.deltaTime;
        if (cronometro >= 4)
        {
            rutina = Random.Range(0, 2);
            cronometro = 0;
        }
        switch (rutina)
        {
            case 0:
                ani.SetBool("idle", true);
                ani.SetBool("walk", false);
                break;
            case 1:
                grado = Random.Range(0, 360);
                angulo = Quaternion.Euler(0, grado, 0);
                rutina++;
                ani.SetBool("idle", false);
                ani.SetBool("walk", true);
                break;
            case 2:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                transform.Translate(Vector3.forward * 0.1f * Time.deltaTime);
                ani.SetBool("idle", false);
                ani.SetBool("walk", true);
                break;
        }
    }
    void Update()
    {
        Comportamiento();
    }
}