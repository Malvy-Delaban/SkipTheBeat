using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternTechnoLow1 : PatternDefault
{
    [SerializeField] GameObject homingProjectile;

    void Start() {
        this.Invoke(() => SpawnProjectiles(GetPosOf(posList.TopLeft)), 0.5f);
        this.Invoke(() => SpawnProjectiles(GetPosOf(posList.TopCenter)), 1.5f);
        this.Invoke(() => SpawnProjectiles(GetPosOf(posList.TopRight)), 2.5f);
    }

    void SpawnProjectiles(Vector3 pos) {
        GameObject projectile = Instantiate(homingProjectile, pos, Quaternion.identity);
        projectile.transform.SetParent(transform);
    }
}
