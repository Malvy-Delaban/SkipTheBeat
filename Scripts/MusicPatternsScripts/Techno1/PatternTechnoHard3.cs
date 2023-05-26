using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternTechnoHard3 : PatternDefault
{
    [SerializeField] GameObject verticalDangerZone;
    [SerializeField] GameObject goingLeftPlatform;
    [SerializeField] GameObject goingRightPlatform;

    void Start() {
        SpawnPlatformsToLeft();
        SpawnPlatformsToRight();
        Invoke("SpawnVerticalDangerZone", 1f);
    }

    void SpawnVerticalDangerZone() {
        GameObject dangerZone1 = Instantiate(verticalDangerZone, new Vector3(-18f, 8f, 10f), Quaternion.identity);
        GameObject dangerZone2 = Instantiate(verticalDangerZone, new Vector3(18f, 8f, 10f), Quaternion.identity);
        dangerZone1.transform.parent = transform;
        dangerZone2.transform.parent = transform;
    }

    void SpawnPlatformsToLeft() {
        GameObject plat = Instantiate(goingLeftPlatform, new Vector3(30f, -5f, 0f), Quaternion.identity);
        plat.transform.parent = transform;
        Invoke("SpawnPlatformsToLeft", Random.Range(0.8f, 0.9f));
    }

    void SpawnPlatformsToRight() {
        GameObject plat = Instantiate(goingRightPlatform, new Vector3(-30f, -2f, 0f), Quaternion.identity);
        plat.transform.parent = transform;
        Invoke("SpawnPlatformsToRight", Random.Range(0.7f, 0.9f));
    }
}