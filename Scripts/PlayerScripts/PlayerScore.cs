using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private CameraShaker camShaker;
    private SpriteRenderer spriteRenderer;
    private int hitNumber = 0;

    private float timeSpentInvicible = 0f;
    private bool isInvincible = false;
    private bool nextAlphaValueBlink = false; // false = 0, true = 1
    private AudioSource audio;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
    }

    void Update() {
        if (isInvincible) {
            MakeInvicibleBlink();
            timeSpentInvicible += Time.deltaTime;
        }
        if (timeSpentInvicible >= 1.5f) {
            isInvincible = false;
            ChangAlphaColor(1f);
        }
    }

    public void GotHit() {
        if (isInvincible)
            return;
        hitNumber++;
        timeSpentInvicible = 0f;
        isInvincible = true;
        audio.Play();
        StartCoroutine(camShaker.Shake());
    }

    public int GetHitNumbers() {
        return hitNumber;
    }

    private void ChangAlphaColor(float alpha) {
        Color tempCol = spriteRenderer.color;
        tempCol.a = alpha;
        spriteRenderer.color = tempCol;
    }

    private void AddToAlphaColor(float alpha) {
        Color tempCol = spriteRenderer.color;
        tempCol.a += alpha;
        spriteRenderer.color = tempCol;
    }

    private void MakeInvicibleBlink() {
        if (nextAlphaValueBlink == true && spriteRenderer.color.a > 0.98f) {
            nextAlphaValueBlink = false;
            ChangAlphaColor(0.98f);
        } else if (nextAlphaValueBlink == false && spriteRenderer.color.a < 0.02f) {
            nextAlphaValueBlink = true;
            ChangAlphaColor(0.02f);
        } else
            AddToAlphaColor(Time.deltaTime * (nextAlphaValueBlink ? 7f : -7f));
    }
}
