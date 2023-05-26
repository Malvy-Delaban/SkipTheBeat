using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternTechnoHard1 : PatternDefault
{
    [SerializeField] GameObject homingProjectile;

    void Start() {
        this.Invoke(() => SpawnProjectiles(GetPosOf(posList.TopLeft)), 0.3f);
        this.Invoke(() => SpawnProjectiles(GetMiddlePosOf(posList.TopLeft, posList.TopCenter)), 0.5f);
        this.Invoke(() => SpawnProjectiles(GetPosOf(posList.TopCenter)), 0.7f);
        this.Invoke(() => SpawnProjectiles(GetMiddlePosOf(posList.TopCenter, posList.TopRight)), 0.9f);
        this.Invoke(() => SpawnProjectiles(GetPosOf(posList.TopRight)), 1.1f);

        this.Invoke(() => SpawnProjectiles(GetPosOf(posList.TopRight)), 1.5f);
        this.Invoke(() => SpawnProjectiles(GetMiddlePosOf(posList.TopCenter, posList.TopRight)), 1.7f);
        this.Invoke(() => SpawnProjectiles(GetPosOf(posList.TopCenter)), 1.9f);
        this.Invoke(() => SpawnProjectiles(GetMiddlePosOf(posList.TopLeft, posList.TopCenter)), 2.1f);
        this.Invoke(() => SpawnProjectiles(GetPosOf(posList.TopLeft)), 2.3f);

        this.Invoke(() => SpawnProjectiles(GetPosOf(posList.TopLeft)), 2.5f);
        this.Invoke(() => SpawnProjectiles(GetMiddlePosOf(posList.TopLeft, posList.TopCenter)), 2.5f);
        this.Invoke(() => SpawnProjectiles(GetPosOf(posList.TopCenter)), 2.5f);
        this.Invoke(() => SpawnProjectiles(GetMiddlePosOf(posList.TopCenter, posList.TopRight)), 2.5f);
        this.Invoke(() => SpawnProjectiles(GetPosOf(posList.TopRight)), 2.5f);
    }

    void SpawnProjectiles(Vector3 pos) {
        GameObject projectile = Instantiate(homingProjectile, pos, Quaternion.identity);
        projectile.transform.SetParent(transform);
    }
}