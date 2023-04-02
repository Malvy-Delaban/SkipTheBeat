using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumGeneratorWithBuffer : MonoBehaviour
{
    private static AudioSource myAudioSource; // The source of audio we listen to
    public static float[] freqBand = new float[512]; // Original values of each 512 frequency received
    private float[] normalizedFreqBand = new float[8]; // Frequency regrouped into 8 bands
    private float[] bufferedFreqBand = new float[8]; // The same 8 frequency bands but that smoothly goes down after a beat 

    // Those are the variables we want to use for other component to get a beat responsive visual
    public static float[] scaledFreqBand = new float[8]; // NormalizedFreqBand but the value is contained between 0 and 1, 1 being the highest beat so far in the sound
    public static float[] scaledBuffFreqBand = new float[8]; // Same as scaledFreqBand but with the smooth decrease after beat

    public static float amplitude; // current amplitude of all frequency combined
    public static float buffAmplitude; // Same as amplitude but with the smooth / buffered version
    private static float highestAmplitude; // Highest amplitude found so far
    private static float highestBuffAmplitude; // Same as highestAmplitude but with the smooth / buffered version

    public float defaultHighestAmplitude = 0.5f; // To make the begining of song less violent in scale, we set a default highest value
    private static float[] scaledHighestOfBand = new float[8]; // Internal variable used to to know the highest beat received in this band of frequency
    private static float[] bufferDecrease = new float[8]; // Internal variable used to smoothly decrease the buffered frequency band

    void Start() {
        GetAudioSource();
        SetDefaultHighest();
    }

    void Update() {
        GetSpectrumData();
        GenerateNormalizedBands();
        GenerateBufferedBands();
        GenerateScaledBands();
        GenerateAmplitude();
    }

    void GetAudioSource() {
        myAudioSource = GetComponent<AudioSource>();
    }

    void SetDefaultHighest() {
        // TODO : Fix this, it seems to always have highestAmplitude at 1;
        for (int i = 0; i < 8; i++) {
            highestAmplitude = defaultHighestAmplitude;
            highestBuffAmplitude = defaultHighestAmplitude;
        }
        // TODO : To make the pattern interact even in the calm moments of the sound :
        // Make the highest amplitude lower each frame progressively and when a highest is defined another time, make the decreasing speed reset to slow
    }

    void GetSpectrumData() {
        myAudioSource.GetSpectrumData(freqBand, 0, FFTWindow.Blackman);
    }

    void GenerateNormalizedBands() {
        int count = 0;

        for (int i = 0; i < 8; i++) {
            int originalFreqBandCount = (int) Mathf.Pow(2, i) * 2;
            float average = 0;

            if (i == 7)
                originalFreqBandCount += 2;
            for (int j = 0; j < originalFreqBandCount; j++) {
                average += freqBand[count] * (count + 1); 
                count++;
            }
            average /= count;
            normalizedFreqBand[i] = average * 10;
        }
    }

    void GenerateBufferedBands() {
        for (int i = 0; i < 8; i++) {
            if (normalizedFreqBand[i] >= bufferedFreqBand[i]) {
                bufferedFreqBand[i] = normalizedFreqBand[i];
                bufferDecrease[i] = 0.005f;
            } else {
                bufferedFreqBand[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.2f;
            }
        }
    }

    void GenerateScaledBands() {
        for (int i = 0; i < 8; i++) {
            if (freqBand[i] > scaledHighestOfBand[i])
                scaledHighestOfBand[i] = freqBand[i];
            scaledFreqBand[i] = (freqBand[i] / scaledHighestOfBand[i]);
            scaledBuffFreqBand[i] = (bufferedFreqBand[i] / scaledHighestOfBand[i]);
        }
    }

    void GenerateAmplitude() {
        float amplitudeTemp = 0f;
        float amplitudeBuffTemp = 0f;

        for (int i = 0; i < 8; i++) {
            amplitudeTemp += scaledFreqBand[i];
            amplitudeBuffTemp += scaledBuffFreqBand[i];
        }
        amplitude = amplitudeTemp / 8;
        if (amplitude > highestAmplitude)
            highestAmplitude = amplitude;
        buffAmplitude = amplitudeBuffTemp / 8;
        if (buffAmplitude > highestBuffAmplitude)
            highestBuffAmplitude = buffAmplitude;
    }
}
