using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnSound : MonoBehaviour
{
    [Header("Modifications to apply")]
    [SerializeField] private bool scaleOnHeight;
    [SerializeField] private bool scaleOnWidth;
    [SerializeField] private bool scaleOnDepth;
    [SerializeField] private float multiplier = 0.01f;
    [SerializeField] private float baseMultiplier = 1f;

    [Header("Input")]
    [SerializeField] private bool getFromAmplitude; // true : use amplitude , false use frequency band
    [Range(0, 7)][SerializeField] private int frequencyBand = 0; // Number between 0 and 8, 0 being the bass

    private Vector3 initialtScale;

    // Start is called before the first frame update
    void Start()
    {
        initialtScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float valueToGet = getFromAmplitude ? SpectrumGeneratorWithBuffer.buffAmplitude : SpectrumGeneratorWithBuffer.scaledBuffFreqBand[frequencyBand];

        if (valueToGet >= 0f) {
            float newLocalScaleX = initialtScale.x * baseMultiplier + (initialtScale.x * multiplier * valueToGet);
            float newLocalScaleY = initialtScale.y * baseMultiplier + (initialtScale.y * multiplier * valueToGet);
            float newLocalScaleZ = initialtScale.z * baseMultiplier + (initialtScale.z * multiplier * valueToGet);
            transform.localScale = new (
                ( scaleOnWidth ? newLocalScaleX : initialtScale.x),
                ( scaleOnHeight ? newLocalScaleY : initialtScale.y),
                ( scaleOnDepth ? newLocalScaleZ : initialtScale.z)
            );
        }
    }
}
