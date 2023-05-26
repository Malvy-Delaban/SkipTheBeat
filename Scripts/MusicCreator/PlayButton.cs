using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private MusicCreator musicCreator;
    private bool isActive = false;

    void Update() {
        if (musicCreator.IsValid() && !isActive) {
            MakeActive();
        } else if (!musicCreator.IsValid() && isActive) {
            MakeDisabled();
        }
    }

    void MakeActive() {
        isActive = true;
        transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<Button>().interactable = true;
    }

    void MakeDisabled() {
        isActive = false;
        transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<Button>().interactable = false;
    }
}
