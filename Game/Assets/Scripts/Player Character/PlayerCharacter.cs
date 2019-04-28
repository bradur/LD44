// Date   : 27.04.2019 16:56
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour
{

    private GameConfig gameConfig;

    private float shootTimer = 99f;
    private float shootInterval = 1f;

    private int health = 100;

    public void Init()
    {
        gameConfig = ConfigManager.main.GetConfig("GameConfig") as GameConfig;
        health = InventoryManager.main.GetHealth();
    }

    public void SetHealth(int newHealth) {
        health = newHealth;
    }

    public void Shoot()
    {
        InventoryItem item = InventoryManager.main.GetCurrentItem();
        if (item.Ammo > 0 || item.UnlimitedAmmo || item.WeaponConfig.Projectile.Mine)
        {
            if (item.Weapon) {
                item.Weapon.Shoot(-transform.up, transform.position - (transform.up * 0.2f), transform);
            }
        }
    }

    public void TakeDamage(int damage) {
        health -= damage;
        InventoryManager.main.UpdateHealth(health);
        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        GameManager.main.ShowYouDiedScreen();
        Debug.Log("You died!");
    }

    void Update()
    {
        shootTimer += Time.deltaTime;
        if (Time.timeScale > 0f && (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)))
        {
            InventoryItem selectedItem = InventoryManager.main.GetCurrentItem();
            if (selectedItem != null && selectedItem.WeaponConfig != null && shootTimer > selectedItem.WeaponConfig.FireInterval)
            {
                shootTimer = 0f;
                Shoot();
            }
        }
    }
}
