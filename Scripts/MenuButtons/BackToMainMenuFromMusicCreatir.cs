using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMainMenuFromMusicCreatir : MonoBehaviour
{
    private SceneLoader sceneLoader;

    void Start() {
        sceneLoader = GameObject.Find("SceneTransition").transform.GetChild(0).GetComponent<SceneLoader>();
    }

    public void BackToMainMenu() {
        GameObject.Destroy(GameObject.Find("MusicCreator"));
        GameObject.Destroy(GameObject.Find("MenuSoundHandler"));
        sceneLoader.LoadScene("MainMenuScene");
    }
}
