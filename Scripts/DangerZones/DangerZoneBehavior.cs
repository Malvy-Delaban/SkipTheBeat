using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZoneBehavior : MonoBehaviour
{
    private SpriteRenderer sr;
    private float spawnedSince = 0f;
    public bool isEffective = false;
    private bool nextAlphaValueBlink = false; // false = 0, true = 1

    void Start() {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (!isEffective)
            TimerDangerZone();
        else
            UpdateEffective();
    }

    void TimerDangerZone() {
        spawnedSince += Time.deltaTime;
        if (spawnedSince > 2.8f)
            MakeEffective();
        BlinkingEffect();
    }

    void MakeEffective() {
        isEffective = true;
        GetComponent<DamagableObjects>().MakeEffective(true);
        ChangAlphaColor(1f);
        sr.color = Color.red;
    }

    void ChangAlphaColor(float alpha) {
        Color tempCol = sr.color;
        tempCol.a = alpha;
        sr.color = tempCol;
    }

    void AddToAlphaColor(float alpha) {
        Color tempCol = sr.color;
        tempCol.a += alpha;
        sr.color = tempCol;
    }

    void UpdateEffective() {
    }

    void BlinkingEffect() {
        if (nextAlphaValueBlink == true && sr.color.a > 0.98f) {
            nextAlphaValueBlink = false;
            ChangAlphaColor(0.98f);
        } else if (nextAlphaValueBlink == false && sr.color.a < 0.02f) {
            nextAlphaValueBlink = true;
            ChangAlphaColor(0.02f);
        } else {
            AddToAlphaColor(Time.deltaTime * (nextAlphaValueBlink ? 5f : -5f));
        }
    }
}
