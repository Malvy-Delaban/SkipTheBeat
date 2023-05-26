using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public Animator animator;
    private string sceneNameToLoad;

    void Start() {
        if (SceneManager.GetActiveScene().name != "MainMenuScene") {
            animator.SetTrigger("FadeInStart");
        }
    }

    public void LoadScene(string sceneName) {
        sceneNameToLoad = sceneName;
        animator.SetTrigger("FadeOutStart");
    }

    public void OnFadeOutTransitionEnd() {
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
