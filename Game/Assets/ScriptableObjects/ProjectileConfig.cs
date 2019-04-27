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
    private int basePrice;
    public int BasePrice { get { return basePrice; } }

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

}