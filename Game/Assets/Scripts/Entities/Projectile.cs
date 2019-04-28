// Date   : 27.04.2019 16:41
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Rigidbody rb;

    private ProjectileConfig config;

    private float lifeTimer = 0f;
    private float afterBlowUpTimer = 0f;
    private float afterBlowUpInterval = 0.4f;

    private float blowUpCheckInterval = 0.1f;
    private float blowUpCheckTimer = 0f;

    private bool blownUp = false;

    [SerializeField]
    private GameObject particles;

    [SerializeField]
    private Animator animator;

    Vector2 myDirection;

    public void Init(ProjectileConfig projectileConfig, Vector2 direction, Vector2 newPosition, Transform origin)
    {
        config = projectileConfig;
        spriteRenderer.sprite = config.RealWorldSprite;
        transform.position = newPosition;

        if (projectileConfig.Mine)
        {
            transform.SetParent(GameManager.main.WorldParent);
        }
        else if (projectileConfig.Melee)
        {
            transform.SetParent(origin);
            transform.rotation = origin.rotation;
            myDirection = direction;
        }
        else
        {
            transform.SetParent(GameManager.main.WorldParent);
            Shoot(direction);
            myDirection = direction;
        }
    }

    public void Shoot(Vector2 direction)
    {
        rb.velocity = direction * config.Speed;
    }

    void CheckExplosion() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, config.ExplosionRadius, config.AffectedByExplosionLayer);
        foreach(Collider collider in colliders) {
            if (collider != null) {
                Vector2 heading = collider.transform.position - transform.position;
                Vector2 direction = heading / heading.magnitude;
                RaycastHit hit;
                Debug.DrawRay(transform.position, direction, Color.magenta, config.ExplosionRadius + 1f);
                if (Physics.Raycast(
                    transform.position,
                    direction,
                    out hit,
                    config.ExplosionRadius / 2f
                )) {
                    if (hit.collider.gameObject.tag == "Enemy") {
                        Enemy enemy = hit.collider.GetComponent<Enemy>();
                        if (enemy) {
                            enemy.TakeDamage(config, direction * config.PushForce);
                        }
                    } else if (hit.collider.gameObject.tag == "DestroyableWall") {
                        Destroy(hit.collider.gameObject);
                    } else if (hit.collider.gameObject.tag == "Player") {
                        PlayerCharacter player = hit.collider.GetComponent<PlayerCharacter>();
                        player.TakeDamage(config.Damage);
                    }
                }
            }
        }
    }
    public void BlowUp() {
        particles.SetActive(true);
        animator.enabled = true;
        CheckExplosion();
        blownUp = true;
    }

    void Update()
    {
        if (config.LifeTime >= 0)
        {
            lifeTimer += Time.deltaTime;
            if (lifeTimer > config.LifeTime)
            {
                Destroy(gameObject);
            }
        } else if (blownUp) {
            blowUpCheckTimer += Time.deltaTime;
            if (blowUpCheckTimer > blowUpCheckInterval) {
                CheckExplosion();
            }
            afterBlowUpTimer += Time.deltaTime;
            if (afterBlowUpTimer > afterBlowUpInterval) {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (config.PushForce > 0)
            {
                enemy.TakeDamage(config, myDirection * config.PushForce);
            }
            else
            {
                enemy.TakeDamage(config);
            }
        }
        else if (collision.gameObject.tag == "Player")
        {
            PlayerCharacter player = collision.gameObject.GetComponent<PlayerCharacter>();
            player.TakeDamage(config.Damage);
        }
        if (!config.Mine) {
            Destroy(gameObject);
        }

    }
}
