using System.Collections;
using UnityEngine;

public class GirlBehavior : MonoBehaviour
{
    private AudioSource audioSource; // Referencia al componente AudioSource
    public AudioClip girlCryingSound; // Clip de sonido de niña llorando
    public float cryInterval = 15.0f; // Intervalo de tiempo entre repeticiones

    // Distancias personalizadas
    public float minDistance = 200.0f; // Distancia mínima
    public float normalDistance = 800.0f; // Distancia normal

    // Volumen personalizado para cada distancia
    public float minVolume = 1.0f;
    public float normalVolume = 0.5f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Obtener el componente AudioSource

        // Asegurarse de que el sonido no se reproduzca al inicio
        audioSource.playOnAwake = false;
        audioSource.clip = girlCryingSound; // Asignar el clip de sonido

        // Configurar el AudioSource para sonido 3D
        audioSource.spatialBlend = 1.0f; // 1.0f para 3D
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic; // Usar Linear Rolloff

        // Iniciar la corrutina para ajustar el volumen del sonido en tiempo de ejecución
        StartCoroutine(AdjustVolumeRoutine());
        StartCoroutine(PlayCryingSoundRoutine());
    }

    private IEnumerator AdjustVolumeRoutine()
    {
        while (true)
        {
            // Calcular la distancia al jugador
            float distance = Vector3.Distance(transform.position, Camera.main.transform.position);

            // Ajustar el volumen según la distancia
            float volume = CalculateVolume(distance);

            // Establecer el volumen en el AudioSource
            audioSource.volume = volume;

            yield return null;
        }
    }

    private IEnumerator PlayCryingSoundRoutine()
    {
        while (true)
        {
            PlayCryingSound();
            yield return new WaitForSeconds(cryInterval); // Esperar el intervalo de tiempo
        }
    }

    private void PlayCryingSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private float CalculateVolume(float distance)
    {
        if (distance < minDistance)
        {
            return minVolume;
        }
        else if (distance < normalDistance)
        {
            return normalVolume;
        }
        else
        {
            // Si la distancia es mayor o igual a normalDistance, podrías devolver un valor por defecto o lo que consideres apropiado.
            return 0.0f; // Por ejemplo, si quieres que el volumen sea 0 fuera del rango de normalDistance.
        }
    }
}
