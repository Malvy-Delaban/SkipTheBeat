using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBehavior : MonoBehaviour
{
    [SerializeField] protected float projectileSpeed = 10f;
    protected Transform playerTransform;
    private float verticalBounds = 30;
    private float horizontalBounds = 30;

    public virtual void Start()
    {
        playerTransform = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Transform>();
    }

    public virtual void Update()
    {
        MoveProjectile();
        CheckBounds();
    }

    public abstract void MoveProjectile();

    void CheckBounds() {
        if (Mathf.Abs(transform.position.y) > verticalBounds || Mathf.Abs(transform.position.x) > horizontalBounds)
            Destroy(gameObject);
    }
}
