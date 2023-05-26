using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternTechnoVeryHard1 : PatternDefault
{
    [SerializeField] private GameObject concentricProjectile;
    [SerializeField] private GameObject traversablePlatform;
    private int spawnNumber = 4;
    private Transform parentConcentricCircle;

    void Start() {
        parentConcentricCircle = transform.GetChild(0);
        Invoke("StartPattern", 1);
    }

    void StartPattern() {
        int distance = 25;
        SpawnPlatforms();
        transform.position = GetPosOf(posList.CenterCenter);
        SpawnConcentricCircle(distance);
        for (distance = 32; distance < 34;)
            SpawnConcentricCircle(distance++);
        for (distance = 41; distance < 43;)
            SpawnConcentricCircle(distance++);
        spawnNumber = 10;
        distance = 52;
        for (; distance <= 60; spawnNumber += 1)
            SpawnConcentricCircle(distance++);
    }

    void SpawnPlatforms() {
        GameObject pla1 = Instantiate(traversablePlatform, GetMiddlePosOf(posList.TopCenter, posList.CenterCenter), Quaternion.identity);
        GameObject pla2 = Instantiate(traversablePlatform, GetPosOf(posList.CenterCenter), Quaternion.identity);
        GameObject pla3 = Instantiate(traversablePlatform, GetMiddlePosOf(posList.CenterCenter, posList.BottomCenter), Quaternion.identity);
        pla1.transform.parent = transform;
        pla2.transform.parent = transform;
        pla3.transform.parent = transform;
        pla1.transform.localScale = new(8f, 0.6f, 1f);
        pla3.transform.localScale = new(8f, 0.6f, 1f);
        LowerPlatform(pla1, 0.8f);
        LowerPlatform(pla2, 0.5f);
        LowerPlatform(pla3, 0.25f);
    }

    void LowerPlatform(GameObject pla, float val) {
        Vector3 newPos = pla.transform.position;
        newPos.y -= val;
        pla.transform.position = newPos;
    }

    void Update() {
        parentConcentricCircle.Rotate(0, 0, Time.deltaTime * 25f);
    }

    void SpawnConcentricCircle(int distance) {
        for (int i = 0; i < spawnNumber; i++)  {
            var proj = Instantiate(concentricProjectile, new Vector3(distance, 0, 0), Quaternion.identity);
            proj.transform.parent = parentConcentricCircle;
            parentConcentricCircle.Rotate(0, 0, (360 / spawnNumber));
        }
    }
}