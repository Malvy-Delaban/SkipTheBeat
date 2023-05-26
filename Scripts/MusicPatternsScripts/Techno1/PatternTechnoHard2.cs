using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternTechnoHard2 : PatternDefault
{
    [SerializeField] GameObject homingProjectile;
    [SerializeField] GameObject movingUpwardPlatform;

    void Start() {
        SpawnPlatforms();
        this.Invoke(() => SpawnProjectiles(GetPosOf(posList.TopLeft)), 0.6f);
        this.Invoke(() => SpawnProjectiles(GetPosOf(posList.TopCenter)), 1.7f);
        this.Invoke(() => SpawnProjectiles(GetPosOf(posList.TopRight)), 2.8f);
    }

    void SpawnPlatforms() {
        var randomNumber = Random.Range(-20f, 20f);
        var randomDelay = Random.Range(0.5f, 1f);

        GameObject plat = Instantiate(movingUpwardPlatform, new Vector3(randomNumber, -7f, 0f), Quaternion.identity);
        plat.transform.SetParent(transform);
        Invoke("SpawnPlatforms", randomDelay);
    }

    void SpawnProjectiles(Vector3 pos) {
        GameObject projectile = Instantiate(homingProjectile, pos, Quaternion.identity);
        projectile.transform.SetParent(transform);
    }
}