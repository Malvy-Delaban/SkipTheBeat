using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepBetweenScene : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
