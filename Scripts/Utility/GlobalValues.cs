using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValues : MonoBehaviour
{
    public bool isAskingForTutorial = false;

    void Start() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GlobalValues");
        if (objs.Length > 1)
            Destroy(this.transform);
        else
            DontDestroyOnLoad(this.gameObject);
    }
}
