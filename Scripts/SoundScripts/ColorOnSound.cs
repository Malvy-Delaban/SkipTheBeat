using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorOnSound : MonoBehaviour
{
    [SerializeField] private Gradient colorGradient;

    [Header("Input")]
    [SerializeField] private bool getFromAmplitude; // true : use amplitude , false use frequency band
    [Range(0, 7)][SerializeField] private int frequencyBand = 0; // Number between 0 and 8, 0 being the bass

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var colors = GetComponent<Button>().colors;
        float valueToGet = getFromAmplitude ? SpectrumGeneratorWithBuffer.buffAmplitude : SpectrumGeneratorWithBuffer.scaledBuffFreqBand[frequencyBand];

        if (valueToGet >= 0f) {
            colors.normalColor = colorGradient.Evaluate(valueToGet);
            GetComponent<Button>().colors = colors;
        }        
    }
}
