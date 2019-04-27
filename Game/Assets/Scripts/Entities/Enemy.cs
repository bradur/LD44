// Date   : 27.04.2019 21:18
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private EnemyConfig config;

    int health = 0;
    public void Init() {
        health = config.Health;
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
