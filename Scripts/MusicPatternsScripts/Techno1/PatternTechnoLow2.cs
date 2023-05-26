using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternTechnoLow2 : PatternDefault
{
    [SerializeField] GameObject simpleProjectile;

    void Start() {
        float timeToSpawnProjectile = 0f;

        while (timeToSpawnProjectile <= 5f) {
            this.Invoke(() => SpawnProjectiles(), timeToSpawnProjectile);
            timeToSpawnProjectile += 0.4f;
        }
    }

    void SpawnProjectiles() {
        Vector3 pos = player.transform.position;
        pos.y += 16f;
        GameObject projectile = Instantiate(simpleProjectile, pos, Quaternion.identity);
        projectile.transform.SetParent(transform);
    }
}