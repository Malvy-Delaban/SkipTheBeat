using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] private Slider slider;

    void Start() {
        float currentValue = 0f;
        audioMixer.GetFloat("volume", out currentValue);
        slider.value = currentValue;
    }

    public void SetVolume(float volume) {
        audioMixer.SetFloat("volume", volume);
    }
}
