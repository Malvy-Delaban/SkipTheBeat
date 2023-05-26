using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskTutorial : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private GameObject tutorialCanvas;

    void Update() {
        if (Input.GetKeyDown("escape"))
            tutorialCanvas.SetActive(false);
    }

    public void AcceptTutorial() {
        GlobalValues glb = GameObject.Find("GlobalValues").GetComponent<GlobalValues>();
        glb.isAskingForTutorial = true;
        LoadMusicCreation();
    }

    
    public void SkipTutorial() {
        LoadMusicCreation();
    }

    private void LoadMusicCreation() {
        tutorialCanvas.SetActive(false);
        sceneLoader.LoadScene("MusicCreation");
    }
}
