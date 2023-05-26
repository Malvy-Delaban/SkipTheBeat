using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class ButtonInTrackList : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private SchemeList schemeList;
    private MusicCreator musicCreator;
    private float speedScale = 3f;
    private float timeBeforeParticlesStop = 0.2f;
    private bool isParticleEmitting = false;

    void Start() {
        schemeList = GameObject.Find("SchemesList").GetComponent<SchemeList>();
        musicCreator = GameObject.Find("MusicCreator").GetComponent<MusicCreator>();
        transform.GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(RemovePart);
        transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(MoveToLeft);
        transform.GetChild(1).GetChild(2).GetComponent<Button>().onClick.AddListener(MoveToRight);
    }

    void Update() {
        if (isParticleEmitting == true)
            timeBeforeParticlesStop -= Time.deltaTime;
        if (timeBeforeParticlesStop <= 0 && isParticleEmitting)
            StopParticles();
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, speedScale * Time.deltaTime);
    }

    public void AnimateSpawn() {
        isParticleEmitting = true;
        if (GetComponent<MusicPartContainer>() == null)
            return;
        MusicPart part = GetComponent<MusicPartContainer>().part;    
        if (part.difficulty == MusicPartDifficulty.Intro || part.difficulty == MusicPartDifficulty.Outro)
            return;
        transform.localScale *= 1.8f;
        SpawnParticles();
    }

    private void StopParticles() {
        isParticleEmitting = false;
        if (transform.childCount >= 3 && transform.GetChild(2) != null && transform.GetChild(2).GetComponent<ParticleSystem>()) {
            var emission = transform.GetChild(2).GetComponent<ParticleSystem>().emission;
            emission.rateOverTime = 0;
        }
    }

    private void SpawnParticles() {
        if (transform.childCount >= 3 && transform.GetChild(2) != null && transform.GetChild(2).GetComponent<ParticleSystem>()) {
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetChild(2).GetComponent<ParticleSystem>().startColor = GetComponent<MusicPartContainer>().part.color;
        }
    }

    public void RemovePart() {
        schemeList.RemoveMusicPart(GetComponent<MusicPartContainer>().part.partId);
    }
    
    public void MoveToLeft() {
        musicCreator.MoveToLeft(GetComponent<MusicPartContainer>().part.partId);
    }

    public void MoveToRight() {
        musicCreator.MoveToRight(GetComponent<MusicPartContainer>().part.partId);
    }

    public bool isEditable() {
        MusicPartDifficulty diff = GetComponent<MusicPartContainer>().part.difficulty;

        return diff != MusicPartDifficulty.Intro && diff != MusicPartDifficulty.Outro;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (isEditable())
            transform.GetChild(1).gameObject.SetActive(true);
        if (!musicCreator.CanBeMovedToLeft(GetComponent<MusicPartContainer>().part.partId))
            transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
        else
            transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        if (!musicCreator.CanBeMovedToRight(GetComponent<MusicPartContainer>().part.partId))
            transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
        else
            transform.GetChild(1).GetChild(2).gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
