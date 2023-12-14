using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0.5f; // Duración del temblor en segundos
    [SerializeField] private float shakeAmplitude = 0.1f; // Magnitud del temblor
    [SerializeField] private float shakeFrequency = 1.0f; // Frecuencia del temblor

    private float shakeElapsedTime = 0f;
    private Vector3 originalPos;

    void Start()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {

        if (shakeElapsedTime > 0)
        {
            // Genera renou aleatori amb el PerlinNoise
            float shakeValue = Time.time * shakeFrequency;
            float perlinNoise = Mathf.PerlinNoise(shakeValue, shakeValue) * 2f - 1f;

            // APlica el valor de renou a la posició de la càmera
            Vector3 pos = originalPos + new Vector3(perlinNoise, perlinNoise, perlinNoise) * shakeAmplitude;

            transform.localPosition = pos;

            // Redueix la duració del tremolor amb el temps
            shakeElapsedTime -= Time.deltaTime;
        }
        else
        {
            // Atura tremolor
            transform.localPosition = originalPos;
            shakeElapsedTime = 0f;
        }
    }


    public void ShakeCamera()
    {
        // Inicia el temblor de la cámara
        shakeElapsedTime = shakeDuration;
    }
}
