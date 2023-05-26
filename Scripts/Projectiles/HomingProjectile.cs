using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : ProjectileBehavior
{
    private float timeBeforeLockingDirection = 2f;
    private float currentTimerBeforeLockingPosition;
    private Vector3 lastKnownPlayerPosition;
    private Vector3 lastDirection;
    private float projectileSpeedBeforeDirectionLock = 1f;
    private float currentSpeed;
    private float refSpeed = 0f;
    private float timeToReachMaxSpeed = 0.5f;
    private bool isEffective = false;

    public override void Start() {
        base.Start();

        currentSpeed = projectileSpeedBeforeDirectionLock;
        lastKnownPlayerPosition = playerTransform.position;
        lastDirection = playerTransform.position - transform.position;
        currentTimerBeforeLockingPosition = timeBeforeLockingDirection;
    }

    public override void Update() {
        base.Update();

        if (currentTimerBeforeLockingPosition >= 0) {
            currentTimerBeforeLockingPosition -= Time.deltaTime;
        } else {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, projectileSpeed, ref refSpeed, timeToReachMaxSpeed);
            if (!isEffective) {
                isEffective = true;
                GetComponent<DamagableObjects>().MakeEffective(true);
            }
        }
    }

    public override void MoveProjectile() {
        
        if (currentTimerBeforeLockingPosition > 0) {
            Vector3 currentDirection = playerTransform.position - transform.position;
            lastKnownPlayerPosition = playerTransform.position;
            lastDirection = currentDirection;
            float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        var tar = transform.position + (lastDirection * 10f);
        transform.position = Vector3.MoveTowards(transform.position, tar, Time.deltaTime * currentSpeed);
    }
}
