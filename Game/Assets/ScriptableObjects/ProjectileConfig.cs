using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewProjectileConfig", menuName = "Custom/New ProjectileConfig")]
public class ProjectileConfig : ScriptableObject
{

    [SerializeField]
    private Sprite previewPicture;
    public Sprite PreviewPicture { get { return previewPicture; } }

    [SerializeField]
    private Sprite realWorldSprite;
    public Sprite RealWorldSprite { get { return realWorldSprite; } }

    [SerializeField]
    private Projectile projectilePrefab;

    public Projectile ProjectilePrefab { get { return projectilePrefab; } }

    [SerializeField]
    private int basePrice;
    public int BasePrice { get { return basePrice; } }

    [SerializeField]
    private bool melee = false;
    public bool Melee { get { return melee; } }

    [SerializeField]
    private bool mine = false;
    public bool Mine { get { return mine; } }
    [SerializeField]
    private string configName;
    public string Name { get { return configName; } }

    [SerializeField]
    private int damage;

    public int Damage { get { return damage; } }

    [SerializeField]
    private float speed = 1f;
    public float Speed { get { return speed; } }

    [SerializeField]
    private float lifeTime = 5f;
    public float LifeTime { get { return lifeTime; } }

    [SerializeField]
    private float pushForce = 0f;
    public float PushForce { get { return pushForce; } }

    [SerializeField]
    private float explosionRadius = 0f;
    public float ExplosionRadius { get { return explosionRadius; } }

    [SerializeField]
    private LayerMask affectedByExplosionLayer;
    public LayerMask AffectedByExplosionLayer { get { return affectedByExplosionLayer; } }

}