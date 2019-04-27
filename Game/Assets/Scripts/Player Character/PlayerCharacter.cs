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

    public void Init()
    {
        gameConfig = ConfigManager.main.GetConfig("GameConfig") as GameConfig;
    }

    public void Shoot()
    {
        InventoryItem item = InventoryManager.main.GetCurrentItem();
        if (item.Ammo > 0)
        {
            if (item.Weapon) {
                item.Weapon.Shoot(-transform.up, transform.position - transform.up);
                item.Ammo -= 1;
                UIInventoryManager.main.UpdateAmmo(item);
            }
        }
    }

    void Update()
    {
        shootTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            if (shootTimer > InventoryManager.main.GetCurrentItem().WeaponConfig.FireInterval)
            {
                shootTimer = 0f;
                Shoot();
            }
        }
    }
}
