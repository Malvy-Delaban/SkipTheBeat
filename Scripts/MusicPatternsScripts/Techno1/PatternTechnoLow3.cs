using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternTechnoLow3 : PatternDefault
{
    [SerializeField] GameObject verticalDangerZone;
    [SerializeField] GameObject goingLeftPlatform;

    void Start() {
        SpawnPlatforms();
        Invoke("SpawnVerticalDangerZone", 1f);
    }

    void SpawnVerticalDangerZone() {
        GameObject dangerZone = Instantiate(verticalDangerZone, new Vector3(-18f, 8f, 10f), Quaternion.identity);
        dangerZone.transform.parent = transform;
    }

    void SpawnPlatforms() {
        GameObject plat = Instantiate(goingLeftPlatform, new Vector3(30f, -5f, 0f), Quaternion.identity);
        plat.transform.parent = transform;
        Invoke("SpawnPlatforms", 0.6f);
    }
}