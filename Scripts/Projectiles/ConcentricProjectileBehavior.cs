using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcentricProjectileBehavior : ProjectileBehavior
{
    public override void MoveProjectile() {
        Vector3 currentDirection = transform.parent.position - transform.position;
        float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, Time.deltaTime * projectileSpeed);
        CheckForDestination();
    }

    private void CheckForDestination() {
        if (Vector3.Distance(transform.parent.position, transform.position) < 0.1f) {
            Destroy(gameObject);
        }
    }
}
