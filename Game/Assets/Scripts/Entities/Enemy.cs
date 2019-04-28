// Date   : 27.04.2019 21:18
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private EnemyConfig config;
    private GameConfig gameConfig;

    int health = 0;

    private bool shootingEnabled = false;

    private PlayerPosition playerPosition;

    private float shootingTimer = 99f;

    private Weapon weapon;

    private WeaponConfig weaponConfig;
    [SerializeField]
    private FollowPlayer followPlayer;


    [SerializeField]
    private Rigidbody rb;
    private bool dead = false;

    public void Init() {
        gameConfig = ConfigManager.main.GetConfig("GameConfig") as GameConfig;
        health = config.Health;
        playerPosition = ConfigManager.main.GetConfig("PlayerPosition") as PlayerPosition;
        weapon = Instantiate(gameConfig.WeaponPrefab);
        weapon.Init(config.WeaponConfig);
        weaponConfig = config.WeaponConfig;
        weapon.transform.parent = GameManager.main.WorldParent;
    }


    public void EnableShooting()
    {

        shootingEnabled = true;
    }

    public void DisableShooting()
    {
        shootingEnabled = false;
    }

    public void Shoot(Vector2 direction, Vector2 position) {
        weapon.ShootAsEnemy(direction, position, transform);
    }

   public void TakeDamage(ProjectileConfig projectileConfig, Vector2 pushBack) {
        health -= projectileConfig.Damage;
        followPlayer.GetPushed();
        followPlayer.GetHit();
        rb.AddForce(pushBack * 10f, ForceMode.Impulse);
        if (health <= 0) {
            if (!dead) {
                dead = true;
                GameManager.main.KillEnemy(projectileConfig);
                Destroy(gameObject);
            }
        }
        SoundManager.main.PlaySound(SoundType.EnemyHit);
    }

    public void TakeDamage(ProjectileConfig projectileConfig) {
        followPlayer.GetHit();
        health -= projectileConfig.Damage;
        if (health <= 0) {
            Debug.Log("Killing enemy: " + gameObject);
            if (!dead) {
                dead = true;
                GameManager.main.KillEnemy(projectileConfig);
                Destroy(gameObject);
            }
        }
        SoundManager.main.PlaySound(SoundType.EnemyHit);
    }

    void Update() {
        shootingTimer += Time.deltaTime;

        if (shootingEnabled && weapon != null && weaponConfig != null) {

            if (shootingTimer > weaponConfig.FireInterval) {
                shootingTimer = 0f;
                Shoot(playerPosition.GetDirection(transform.position), transform.position + (transform.right * 0.4f));
            }
        }
    }

}
