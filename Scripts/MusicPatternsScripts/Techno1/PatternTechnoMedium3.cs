using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternTechnoMedium3 : PatternDefault
{
    [SerializeField] private GameObject barToAvoidPrefab;

    void Start() {
        this.Invoke(() => SpawnBar(), 0.4f);
    }

    void SpawnBar() {
        GameObject newBar = Instantiate(barToAvoidPrefab, GetPosByRandom(), Quaternion.identity);
        newBar.transform.SetParent(transform);
        this.Invoke(() => SpawnBar(), Random.Range(0.6f, 1.1f));
    }

    Vector3 GetPosByRandom() {
        Vector3[] pos =  {
            GetPosOf(posList.TopLeft),
            GetPosOf(posList.TopCenter),
            GetPosOf(posList.TopRight)
        };

        return pos[(int)Random.Range(0, 2)];
    }
}