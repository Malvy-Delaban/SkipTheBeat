using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightProjectileBehavior : ProjectileBehavior
{
    public override void MoveProjectile() {
        transform.Translate(Vector3.down * Time.deltaTime * projectileSpeed);
    }
}
