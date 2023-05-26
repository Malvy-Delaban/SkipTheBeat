using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternTechnoVeryHard2 : PatternDefault
{
    [SerializeField] GameObject horizontalDangerZone;
    [SerializeField] GameObject movingUpwardPlatform;

    void Start() {
        SpawnHorizontalDangerZone();
        Invoke("SpawnPlatforms", 0.3f);
    }

    void SpawnHorizontalDangerZone() {
        GameObject dangerZone = Instantiate(horizontalDangerZone, new Vector3(0f, 9f, 10f), Quaternion.identity);
        dangerZone.transform.parent = transform;
    }

    void SpawnPlatforms() {
        var randomNumber = Random.Range(-20f, 20f);
        var randomDelay = Random.Range(0.2f, 0.4f);

        GameObject plat = Instantiate(movingUpwardPlatform, new Vector3(randomNumber, -7f, 0f), Quaternion.identity);
        plat.transform.SetParent(transform);
        Invoke("SpawnPlatforms", randomDelay);
    }
}