using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject endOfGameCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private AudioSource audioSourc;
    [SerializeField] private GameObject settingsCanvas;
    private SceneLoader sceneLoader;

    void Start() {
        sceneLoader = GameObject.Find("SceneTransition").transform.GetChild(0).GetComponent<SceneLoader>();
    }

    void Update() {
        if (Input.GetKeyDown("escape") && !endOfGameCanvas.activeSelf) {
            if (pauseCanvas.activeSelf)
                SetUnpauseGame();
            else
                SetPauseGame();
        }
    }

    public void ChangeSettingsState() {
        settingsCanvas.SetActive(!settingsCanvas.activeSelf);
    }

    private void SetPauseGame() {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        audioSourc.Pause();
    }

    public void SetUnpauseGame() {
        pauseCanvas.SetActive(false);
        if (settingsCanvas.activeSelf)
            ChangeSettingsState();
        Time.timeScale = 1f;
        audioSourc.Play(0);
    }

    public void GoToMainMenu() {
        GameObject mh = GameObject.Find("MusicCreator");
        if (mh != null)
            GameObject.Destroy(mh);
        Time.timeScale = 1f;
        sceneLoader.LoadScene("MainMenuScene");
    }

    public void GoToMusicCreator() {
        GameObject mh = GameObject.Find("MusicCreator");
        if (mh != null)
            GameObject.Destroy(mh);
        Time.timeScale = 1f;
        sceneLoader.LoadScene("MusicCreation");
    }
}
