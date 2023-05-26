using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableObjects : MonoBehaviour
{
    private PlayerScore playerScore;
    public bool isEffective = false;

    void Start() {
        playerScore = GameObject.Find("Player").GetComponent<PlayerScore>();
    }

    public void MakeEffective(bool newState) {
        isEffective = newState;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && isEffective == true) {
            playerScore.GotHit();
        }
    }
}
