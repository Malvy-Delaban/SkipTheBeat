using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogBoxClose : MonoBehaviour
{
    void Start() {
        GetComponent<Button>().onClick.AddListener(ClickedButton);
    }

    private void ClickedButton() {
        transform.parent.parent.gameObject.SetActive(false);
    }
}
