using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCheck : MonoBehaviour
{
    private Collider2D col;

    void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
    }

    void Update()
    {
    }

    public void ChangePlatformState(bool isConsistent) {
        col.isTrigger = (isConsistent ? false : true);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "TraversableCheck")
            ChangePlatformState(true);
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "TraversableCheck")
            ChangePlatformState(false);
    }
}
