// Date   : 27.04.2019 16:57
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static GameManager main;


    void Awake()
    {
        main = this;
    }

    [SerializeField]
    private TiledMap tiledMap;

    private Transform worldParent;

    public Transform WorldParent { get { return worldParent; } }
    GameConfig config;

    void Start() {
        Cursor.visible = false;
    }

    void Update(){
        Cursor.visible = false;
    }

    public void KillEnemy(ProjectileConfig projectile) {
        Debug.Log(string.Format("Enemy was killed by {0}", projectile.Name));
        numberOfEnemies -= 1;
        if (numberOfEnemies <= 0) {
            Destroy(worldParent.gameObject);
            UIInventoryManager.main.ClearItems();
            tiledMap.ResetContainers();
            InventoryManager.main.Init();
            InventoryManager.main.ShowShop();
            /*
            InventoryManager.main.ResetHealth();
            InventoryManager.main.ResetPurchasedItems();
            InventoryManager.main.ProcessPurchasedItems();
            InventoryManager.main.UpdateHealth();
            UIShop.main.SetCurrency(InventoryManager.main.GetHealth());*/
        }
    }

    int numberOfEnemies = 0;
    public void SetNumberOfEnemies(int number) {
        numberOfEnemies = number;
    }

    public void StartNextLevel()
    {
        if (config == null)
        {
            config = ConfigManager.main.GetConfig("GameConfig") as GameConfig;
            config.CurrentLevel = config.FirstLevel;
        }
        else
        {
            config.CurrentLevel = config.CurrentLevel.NextLevel;
            if (config.CurrentLevel == null)
            {
                Debug.Log("Game over!");
            }
        }
        worldParent = Instantiate(config.ContainerPrefab);
        worldParent.name = config.CurrentLevel.Name;
        tiledMap.Initialize();
        UIInventoryManager.main.UseCrosshair();
        InventoryManager.main.ProcessPurchasedItems();
        InventoryManager.main.UpdateHealth();
        InventoryManager.main.SelectAutomatically();
    }

}
