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
    private int spawnNumber = 8;
    private bool isPatternStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") == true && !isPatternStarted) {
            isPatternStarted = true;
            SpawnHorizontalDangerZone();
            Invoke("SpawnPlatforms", 0f);
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
        Instantiate(horizontalDangerZone, new Vector3(0f, -5.5f, 10f), Quaternion.identity);
        // Instantiate(horizontalDangerZone, new Vector3(0f, 3f, 10f), Quaternion.identity);
    }

    void SpawnPlatforms() {
        var randomNumber = Random.Range(0.8f, 1.4f);

        var plat = Instantiate(traversablePlatform, new Vector3(30f, -3.5f, 0f), Quaternion.identity);
        plat.transform.localScale = new Vector3(8f + (2f * Mathf.InverseLerp(1f, 1.8f, randomNumber)), 1, 1);
        Invoke("SpawnPlatforms", randomNumber);
    }
}
