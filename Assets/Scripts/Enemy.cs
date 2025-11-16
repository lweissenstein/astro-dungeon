using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int health = 1;
    public float moveSpeed = 2f;

    private Transform player;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void Update()
    {   
        if (player != null)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage = 1)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindAnyObjectByType<ScoreManager>().AddScore(100);

        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);

            SoundEffects sfx = FindAnyObjectByType<SoundEffects>();
            if (sfx != null)
            {
                sfx.PlayBulletHit(transform.position);
            }
            Destroy(other.gameObject); 
        }
    }
}
