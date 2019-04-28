// Date   : 29.12.2018 12:52
// Project: VillageCaveGame
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "NewGameConfig", menuName = "Custom/New GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Player")]

    [SerializeField]
    private float playerSpeed = 10f;
    public float PlayerSpeed { get { return playerSpeed; } }

    [Header("Level management")]
    [SerializeField]
    private LevelConfig firstLevel;

    public LevelConfig FirstLevel { get { return firstLevel; } }

    [SerializeField]
    private LevelConfig currentLevel;
    public LevelConfig CurrentLevel { get { return currentLevel; } set { currentLevel = value; } }


    [Header("Prefabs")]
    [SerializeField]
    private CombinedMeshLayer combinedMeshLayerPrefab;

    public CombinedMeshLayer CombinedMeshLayerPrefab { get { return combinedMeshLayerPrefab; } }

    [SerializeField]
    private MeshTileMap meshTileMapPrefab;

    public MeshTileMap MeshTileMapPrefab { get { return meshTileMapPrefab; } }

    [SerializeField]
    private GameObject wallPrefab;
    public GameObject WallPrefab { get { return wallPrefab; } }

    [SerializeField]
    private Transform containerPrefab;
    public Transform ContainerPrefab { get { return containerPrefab; } }

    [SerializeField]
    private Weapon weaponPrefab;
    public Weapon WeaponPrefab { get { return weaponPrefab; } }

    [SerializeField]
    private Projectile projectilePrefab;
    public Projectile ProjectilePrefab { get { return projectilePrefab; } }


    [SerializeField]
    private UIInventoryItem uiInventoryItemPrefab;
    public UIInventoryItem UIInventoryItemPrefab { get { return uiInventoryItemPrefab; } }

    [SerializeField]
    private UIShopItem uiShopItemPrefab;
    public UIShopItem UIShopItemPrefab { get { return uiShopItemPrefab; } }

}