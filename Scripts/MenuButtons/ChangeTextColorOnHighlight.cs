using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class ChangeTextColorOnHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Color highlightTextColor;
    [SerializeField] private Color originalTextColor;
    private TMP_Text text;
    private ParticleSystem particles;
    private Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        text = transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        particles = transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
        particles.Stop();
        text.color = originalTextColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.GetChild(1).gameObject.SetActive(true);
        particles.Play();
        text.color = highlightTextColor;
        transform.localScale = initialScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        particles.Stop();
        text.color = originalTextColor;
        transform.localScale = initialScale;
    }
}
