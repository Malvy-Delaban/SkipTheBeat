using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject canvasAskTutorial;

    public void LoadScene() {
        canvasAskTutorial.SetActive(true);
    }

    public void QuitGame() {
        // For a build, use this line :
            Application.Quit();
        // For tests in unity, use this line :
        // UnityEditor.EditorApplication.isPlaying = false;
    }
}
