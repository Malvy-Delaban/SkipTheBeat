using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed;
    [SerializeField] private GameObject canvasTutorial;
    private int index;

    void Start() {
        StartDialogue();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
            if (text.text == lines[index]) {
                NextLine();
            } else {
                StopAllCoroutines(); // Must be a better way to do this ...
                text.text = lines[index];
            }
        }

    }

    private void StartDialogue() {
        index = 0;
        text.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine() {
        foreach (char c in lines[index].ToCharArray()) {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine() {
        if (index < lines.Length - 1) {
            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        } else {
            canvasTutorial.SetActive(false);
            GlobalValues glb = GameObject.Find("GlobalValues").GetComponent<GlobalValues>();
            glb.isAskingForTutorial = false;
        }
    } 

}
