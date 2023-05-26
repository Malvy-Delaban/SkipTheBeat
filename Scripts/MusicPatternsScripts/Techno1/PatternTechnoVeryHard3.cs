using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternTechnoVeryHard3 : PatternDefault
{
    [SerializeField] private GameObject goingLeftPlatform;
    [SerializeField] private GameObject traversablePlatform;
    [SerializeField] private GameObject horizontalDangerZone;
    [SerializeField] private GameObject verticalDangerZone;

    void Start() {
        SpawnDangerZone(new Vector3(0f, -6.5f, 30f), horizontalDangerZone);
        SpawnDangerZone(new Vector3(0f, 10f, 30f), horizontalDangerZone);
        SpawnDangerZone(new Vector3(-19.2f, 0f, 30f), verticalDangerZone);
        SpawnDangerZone(new Vector3(19.2f, 0f, 30f), verticalDangerZone);
        SpawnPlatforms();
        SpawnMovingPlatforms();
    }

    void SpawnPlatforms() {
        GameObject pla3 = Instantiate(traversablePlatform, GetMiddlePosOf(posList.CenterCenter, posList.BottomCenter), Quaternion.identity);
        pla3.transform.parent = transform;
        pla3.transform.localScale = new(8f, 0.6f, 1f);
        LowerPlatform(pla3, 0.25f);
    }

    void LowerPlatform(GameObject pla, float val) {
        Vector3 newPos = pla.transform.position;
        newPos.y -= val;
        pla.transform.position = newPos;
    }

    void SpawnMovingPlatforms() {
        GameObject plat = Instantiate(goingLeftPlatform, new Vector3(30f, -2.5f, 0f), Quaternion.identity);
        plat.transform.parent = transform;
        Invoke("SpawnMovingPlatforms", Random.Range(0.3f, 0.7f));
    }

    void SpawnDangerZone(Vector3 pos, GameObject prefab) {
        GameObject dangerZone = Instantiate(prefab, pos, Quaternion.identity);
        dangerZone.transform.parent = transform;
    }
}