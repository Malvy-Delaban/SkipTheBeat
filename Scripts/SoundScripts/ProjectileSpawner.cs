using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject parentConcentricCircle;
    [SerializeField] private GameObject traversablePlatform;
    [SerializeField] private GameObject concentricProjectile;
    [SerializeField] private GameObject horizontalDangerZone;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float cooldown = 0.05f;
    [SerializeField] private float currentCooldown = 0f;
    private int spawnNumber = 8;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") == true) {
            SpawnHorizontalDangerZone();
            Invoke("SpawnPlatforms", 0.1f);
        }
    }

    void SpawnConcentricCircle() {
        for (int i = 0; i < spawnNumber; i++)  {
            var proj = Instantiate(concentricProjectile, new Vector3(18, 0, 0), Quaternion.identity);
            proj.transform.parent = parentConcentricCircle.transform;
            parentConcentricCircle.transform.Rotate(0, 0, (360 / spawnNumber));
        }
    }

    void SpawnHorizontalDangerZone() {
        Instantiate(horizontalDangerZone, new Vector3(0f, -4f, 10f), Quaternion.identity);
        Instantiate(horizontalDangerZone, new Vector3(0f, 3f, 10f), Quaternion.identity);
    }

    void SpawnPlatforms() {
        var randomNumber = Random.Range(1f, 1.8f);

        var plat = Instantiate(traversablePlatform, new Vector3(30f, -2f, 0f), Quaternion.identity);
        plat.transform.localScale = new Vector3(8f + (2f * Mathf.InverseLerp(1f, 1.8f, randomNumber)), 1, 1);
        print(0.8f + (0.2f * Mathf.InverseLerp(1f, 1.8f, randomNumber)));
        Invoke("SpawnPlatforms", randomNumber);
    }
}
