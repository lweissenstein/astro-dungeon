using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab; // Dein Bullet-Prefab
    public float fireRate = 0.5f;   // Sekunden zwischen Sch¸ssen

    private float fireTimer = 0f;
    private Vector2 lastDirection = Vector2.down; // Blickrichtung merken

    void Update()
    {
        fireTimer -= Time.deltaTime;

        // --- Blickrichtung aktualisieren ---
        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (move.magnitude > 0.1f)
            lastDirection = move.normalized;

        // --- Automatisches Schieﬂen ---
        if (fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null) return;
        if (lastDirection == Vector2.zero) return;

        // --- Aktives Prefab automatisch finden ---
        Transform activeChild = null;
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                activeChild = child;
                break;
            }
        }
        if (activeChild == null) return;

        // --- BulletSpawnPoint im aktiven Child suchen ---
        Transform spawnPoint = activeChild.Find("BulletSpawnPoint");
        if (spawnPoint == null) return;

        // --- Bullet erstellen ---
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);

        // --- Richtung setzen ---
        Bullet b = bullet.GetComponent<Bullet>();
        if (b != null)
            b.direction = lastDirection.normalized;
    }
}

