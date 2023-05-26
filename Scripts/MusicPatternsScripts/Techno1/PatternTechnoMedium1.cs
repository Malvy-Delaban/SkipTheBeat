using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternTechnoMedium1 : PatternDefault
{
    [SerializeField] GameObject horizontalDangerZone;
    [SerializeField] GameObject traversablePlatform;

    void Start() {
        SpawnPlatforms();
        Invoke("SpawnHorizontalDangerZone", 1f);
    }

    void SpawnHorizontalDangerZone() {
        GameObject dangerZone = Instantiate(horizontalDangerZone, new Vector3(0f, -5f, 10f), Quaternion.identity);
        dangerZone.transform.parent = transform;
    }

    void SpawnPlatforms() {
        var randomNumber = Random.Range(0.8f, 1.4f);

        GameObject plat = Instantiate(traversablePlatform, new Vector3(30f, -3.5f, 0f), Quaternion.identity);
        plat.transform.localScale = new Vector3(8f + (2f * Mathf.InverseLerp(1f, 1.8f, randomNumber)), 1, 1);
        plat.transform.parent = transform;
        Invoke("SpawnPlatforms", randomNumber);
    }
}