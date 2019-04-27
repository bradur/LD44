using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewWeaponConfig", menuName = "Custom/New WeaponConfig")]
public class WeaponConfig : ScriptableObject
{

    [SerializeField]
    private Sprite previewPicture;
    public Sprite PreviewPicture { get { return previewPicture; } }

    [SerializeField]
    private string configName;
    public string Name { get { return configName; } }

    [SerializeField]
    private KeyCode keyCode;
    public KeyCode KeyCode { get { return keyCode; } }

    [SerializeField]
    private int basePrice;
    public int BasePrice { get { return basePrice; } }

    [SerializeField]
    private float fireInterval;

    public float FireInterval { get { return fireInterval; } }

    [SerializeField]
    private ProjectileConfig projectile;

    public ProjectileConfig Projectile { get { return projectile; } }

}