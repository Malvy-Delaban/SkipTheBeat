using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsCanvas;

    public void OnChangeState() {
        settingsCanvas.SetActive(!settingsCanvas.activeSelf);
    }
}
