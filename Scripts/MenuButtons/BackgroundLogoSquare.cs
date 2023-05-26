using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLogoSquare : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool rotationDirection;
    [SerializeField] private float constantRotationSpeed;
    [SerializeField] private float rotationSpeed; 
    [SerializeField] private bool doNotChangeDefaultScale = false;
    [SerializeField] private float baseShapeScaleMultiplier;
    [SerializeField] private float dynamicScaleMultiplier;
    private Vector3 initialScale = Vector3.zero; 

    [Header("Input")]
    [SerializeField] private bool getFromAmplitudeRotation; // true : use amplitude , false use frequency band
    [SerializeField] private bool getFromAmplitudeScale; // true : use amplitude , false use frequency band
    [Range(0, 7)][SerializeField] private int frequencyBand = 0; // Number between 0 and 8, 0 being the lower frequencies

    void Start()
    {
        initialScale = transform.localScale * (doNotChangeDefaultScale ? 1f : 250f * baseShapeScaleMultiplier);
    }

    void Update()
    {
        float valueToGetRotation = getFromAmplitudeRotation ? SpectrumGeneratorWithBuffer.buffAmplitude : SpectrumGeneratorWithBuffer.scaledBuffFreqBand[frequencyBand];
        float valueToGetScale = getFromAmplitudeScale ? SpectrumGeneratorWithBuffer.buffAmplitude : SpectrumGeneratorWithBuffer.scaledBuffFreqBand[frequencyBand];

        if (initialScale != Vector3.zero && dynamicScaleMultiplier > 0f && rotationSpeed > 0f && valueToGetRotation > 0f && valueToGetScale > 0f) {
            transform.localScale = initialScale + (Vector3.one * dynamicScaleMultiplier * valueToGetScale);
            transform.Rotate(0, 0, (constantRotationSpeed + rotationSpeed * 2f * valueToGetRotation * Time.deltaTime) * (rotationDirection ? 1f : -1f));
        }
    }
}
