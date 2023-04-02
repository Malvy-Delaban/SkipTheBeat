using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeVisualizerBehavior : MonoBehaviour
{
    [SerializeField] private int frequencySample = 0;
    [SerializeField] private float initialScale = 1f;
    [SerializeField] private float scaleMultiplier = 1f;
    [SerializeField] private bool useBufferedValues = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (useBufferedValues)
            transform.localScale = new Vector3(transform.localScale.x, initialScale + SpectrumGeneratorWithBuffer.scaledBuffFreqBand[frequencySample] * scaleMultiplier, transform.localScale.z);
        else
            transform.localScale = new Vector3(transform.localScale.x, initialScale + SpectrumGeneratorWithBuffer.scaledFreqBand[frequencySample] * scaleMultiplier, transform.localScale.z);
    }
}
