using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PatternHandler : MonoBehaviour
{
    [SerializeField] private GameObject track;
    [SerializeField] private GameObject EndOfGameMenu;
    private AudioSource audioSource;
    public List<MusicPart> parts;
    private float currentTimerInPart = -1f;
    private int currentIndex = 0;

    void Update() {
        if (currentTimerInPart < 0f)
            return;
        currentTimerInPart += Time.deltaTime;
        float currentPercent = currentTimerInPart / GetCurrentPartDuration(currentIndex);
        track.transform.GetChild(currentIndex).transform.GetChild(0).GetComponent<Image>().fillAmount = 1f - currentPercent;
    }

    void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
        MusicCreator musicCreator = GameObject.Find("MusicCreator").GetComponent<MusicCreator>();
        parts = musicCreator.parts;
        this.Invoke(() => PlayPattern(0), 3f);
        Invoke("DestroyMenuSoundHandler", 3f);
    }

    void PlayPattern(int patterIndex) {
        currentTimerInPart = 0f;
        currentIndex = patterIndex;
        PlayMusicPattern(patterIndex);
        PlayMusicPart(patterIndex);
        if (patterIndex + 1 >= parts.Count)
            Invoke("FinishSong", GetCurrentPartDuration(patterIndex) + 1.5f);
        else
            this.Invoke(() => PlayPattern(patterIndex + 1), GetCurrentPartDuration(patterIndex));
    }

    float GetCurrentPartDuration(int index) {
        return parts[index].songAsset.length;
    }

    void FinishSong() {
        EndOfGameMenu.SetActive(true);
        int playerScore = GameObject.Find("Player").GetComponent<PlayerScore>().GetHitNumbers();
        EndOfGameMenu.GetComponent<EndGameMenu>().SetNumber(playerScore);
    }

    void PlayMusicPattern(int index) {
        foreach (Transform child in transform)
            GameObject.Destroy(child.gameObject);
        GameObject newPattern = Instantiate(parts[index].prefabPattern, Vector3.zero, Quaternion.identity);
    }

    void PlayMusicPart(int index) {
        audioSource.clip = parts[index].songAsset;
        audioSource.Play();
    }

    public MusicPart GetCurrentPart() {
        return parts[currentIndex];
    }
}