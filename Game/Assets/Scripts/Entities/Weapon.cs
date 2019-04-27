// Date   : 27.04.2019 16:53
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    private WeaponConfig config;

    private Projectile projectile;

    private GameConfig gameConfig;

    private InventoryItem item;

    public void Init(InventoryItem item) {
        gameConfig = ConfigManager.main.GetConfig("GameConfig") as GameConfig;
        config = item.WeaponConfig;
        item.Weapon = this;
        this.item = item;
    }

    public void Shoot(Vector2 direction, Vector2 position) {
        projectile = Instantiate(gameConfig.ProjectilePrefab);
        projectile.Init(config.Projectile, direction, position);
    }

    void Select() {
        InventoryManager.main.Select(item);
    }

    void Update () {
        if (Input.GetKeyDown(config.KeyCode)) {
            Select();
        }
    }
}