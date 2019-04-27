// Date   : 27.04.2019 16:41
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Rigidbody rb;

    private ProjectileConfig config;

    private float lifeTimer = 0f;
    public void Init(ProjectileConfig projectileConfig, Vector2 direction, Vector2 newPosition) {
        config = projectileConfig;
        spriteRenderer.sprite = config.PreviewPicture;
        transform.position = newPosition;
        Shoot(direction);
    }

    public void Shoot(Vector2 direction) {
        rb.velocity = direction * config.Speed;
    }

    void Update () {
        lifeTimer += Time.deltaTime;
        if (lifeTimer > config.LifeTime) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Enemy") {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(config.Damage);
        }
        Destroy(gameObject);
    }
}
