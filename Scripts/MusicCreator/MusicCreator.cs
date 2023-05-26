using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;



public class MusicCreator : MonoBehaviour
{
    public List<MusicPart> parts;
    public LevelSong levelSong;
    [SerializeField] private int pointsNeeded;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private GameObject musicHandler;
    [SerializeField] private GameObject musicHandlerPrefab;
    [SerializeField] private GameObject tutorialCanvas;

    private void Start() {
        parts = new List<MusicPart>();
        GetLevelSong(0);
        AddIntroAndOutro();
        musicHandler = GameObject.Find("MenuSoundHandler");
        if (musicHandler == null)
            CreateMusicHandler();
        Invoke("CheckForTutorial", 2f);
    }

    private void CheckForTutorial() {
        if (GameObject.Find("GlobalValues") != null && GameObject.Find("GlobalValues").GetComponent<GlobalValues>().isAskingForTutorial == true)
            tutorialCanvas.SetActive(true);
    }

    private void CreateMusicHandler() {
        musicHandler = Instantiate(musicHandlerPrefab, Vector3.zero, Quaternion.identity);
    }

    public bool IsMusicFull() {
        return parts.Count >= 10 ? true : false;
    }

    private void Swap(int indexA, int indexB)
    {
        MusicPart tmp = parts[indexA];
        parts[indexA] = parts[indexB];
        parts[indexB] = tmp;
    }

    public bool CanBeMovedToLeft(int partId) {
        int index = parts.FindIndex(e => e.partId == partId);

        if (index <= 0)
            return false;
        if (parts[index - 1].difficulty == MusicPartDifficulty.Intro)
            return false;
        return true;
    }

    public bool CanBeMovedToRight(int partId) {
        int index = parts.FindIndex(e => e.partId == partId);

        if (index >= parts.Count - 1)
            return false;
        if (parts[index + 1].difficulty == MusicPartDifficulty.Outro)
            return false;
        return true;
    }

    public void MoveToLeft(int partId) {
        int index = parts.FindIndex(e => e.partId == partId);
        Swap(index, index - 1);
    }

    public void MoveToRight(int partId) {
        int index = parts.FindIndex(e => e.partId == partId);
        Swap(index, index + 1);
    }

    private void GetLevelSong(int indexLevel) {
        levelSong = GetComponent<MusicPartsList>().levelSongs[indexLevel];
    }

    private void AddIntroAndOutro() {
        List<MusicPart> availableParts = levelSong.parts;
        if (availableParts.Exists(e => e.difficulty == MusicPartDifficulty.Intro))
            parts.Add(availableParts.Find(e => e.difficulty == MusicPartDifficulty.Intro));
        if (availableParts.Exists(e => e.difficulty == MusicPartDifficulty.Outro))
            parts.Add(availableParts.Find(e => e.difficulty == MusicPartDifficulty.Outro));
    }

    public int GetCurrentPoints() {
        int points = 0;

        foreach (MusicPart part in parts)
            points += part.givingPoints;
        return points;
    }

    public int GetPointsNeeded() {
        return pointsNeeded;
    }

    public float GetCurrentDuration() {
        float totalDuration = 0;

        foreach (MusicPart part in parts)
            totalDuration += part.songAsset.length;
        return totalDuration;
    }

    public void AddPart(MusicPartContainer newPart) {
        parts.Insert(parts.Count - 1, newPart.part);
    }

    public void RemovePart(int partId) {
        int id = parts.FindIndex(e => e.partId == partId);
        parts.RemoveAt(id);
    }

    public bool IsValid() {
        if (GetCurrentPoints() >= pointsNeeded)
            return true;
        return false;
    }

    public void FinishSong() {
        if (IsValid()) {
            sceneLoader.LoadScene("LevelScene");
            AudioSourceManager man = musicHandler.GetComponent<AudioSourceManager>();
            AudioSource src = musicHandler.GetComponent<AudioSource>();
            StartCoroutine(AudioSourceManager.StartFade(src, 2f));
            Invoke("DestroyMenuSoundHandler", 2f);
        }
    }

    private void DestroyMenuSoundHandler() {
        GameObject.Destroy(GameObject.Find("MenuSoundHandler"));
    }
}
