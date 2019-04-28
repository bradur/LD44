using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewEnemyConfig", menuName = "Custom/New EnemyConfig")]
public class EnemyConfig : ScriptableObject
{

    [SerializeField]
    private int health;
    public int Health { get { return health; } }
    [SerializeField]
    private string configName;
    public string Name { get { return configName; } }

    [SerializeField]
    private WeaponConfig weaponConfig;
    public WeaponConfig WeaponConfig { get { return weaponConfig; } }

}