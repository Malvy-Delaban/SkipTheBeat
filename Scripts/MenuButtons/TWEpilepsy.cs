using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TWEpilepsy : MonoBehaviour
{
    [SerializeField] private GameObject EN;
    [SerializeField] private GameObject FR;
    [SerializeField] private GameObject ENButton;
    [SerializeField] private GameObject warningBackground;
    [SerializeField] private GameObject logoBackground;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private AudioSource audio;

    private float currentAlpha = 0f;
    private float currentVolume = 0f;
    private bool isDisplayingEN = true;
    private bool isDisplayingFR = false;

    void Start() {
        audio.volume = 0.2f;
        logoBackground.SetActive(false);
        MainMenu.SetActive(false);
        FR.SetActive(false);
        ENButton.SetActive(true);
        EN.GetComponent<CanvasGroup>().alpha = 0f;
        FR.GetComponent<CanvasGroup>().alpha = 0f;
    }

    void Update() {
        if (isDisplayingEN)
            EN.GetComponent<CanvasGroup>().alpha = Mathf.SmoothDamp(EN.GetComponent<CanvasGroup>().alpha, 1f, ref currentAlpha, 1.5f);
        if (isDisplayingFR)
            FR.GetComponent<CanvasGroup>().alpha = Mathf.SmoothDamp(FR.GetComponent<CanvasGroup>().alpha, 1f, ref currentAlpha, 1.5f);
        audio.volume = Mathf.SmoothDamp(audio.volume, 1f, ref currentVolume, 3f);
    }

    public void ContinueToFR() {
        isDisplayingEN = false;
        EN.GetComponent<CanvasGroup>().alpha = 0f;
        ENButton.SetActive(false);
        FR.SetActive(true);
        isDisplayingFR = true;
        currentAlpha = 0f;
    }

    public void ContinueToMenu() {
        isDisplayingFR = false;
        logoBackground.SetActive(true);
        MainMenu.SetActive(true);
        audio.volume = 1f;
        Destroy(gameObject);
    }
}
