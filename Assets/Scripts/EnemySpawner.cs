using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;     
    public float spawnInterval = 2f;   // Interval of spawns
    public float spawnDistance = 1f;   // how far away from camera they spawn

    private Camera cam;
    private float timer = 0f;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null) return;

        // random side
        int side = Random.Range(0, 4);

        Vector2 spawnPos = Vector2.zero;

        // Camera boundaries
        float height = cam.orthographicSize;
        float width = height * cam.aspect;

        switch (side)
        {
            case 0: // Top
                spawnPos = new Vector2(
                    Random.Range(-width, width),
                    height + spawnDistance
                );
                break;

            case 1: // Bottom
                spawnPos = new Vector2(
                    Random.Range(-width, width),
                    -height - spawnDistance
                );
                break;

            case 2: // Left
                spawnPos = new Vector2(
                    -width - spawnDistance,
                    Random.Range(-height, height)
                );
                break;

            case 3: // Right
                spawnPos = new Vector2(
                    width + spawnDistance,
                    Random.Range(-height, height)
                );
                break;
        }

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
