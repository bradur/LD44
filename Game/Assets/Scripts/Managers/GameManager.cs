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

    [SerializeField]
    private Transform worldParent;

    public Transform WorldParent { get { return worldParent; } }
    GameConfig config;

    void Start() {
        Cursor.visible = false;
    }

    void Update(){
        Cursor.visible = false;
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
        tiledMap.Initialize();
        UIInventoryManager.main.UseCrosshair();
        InventoryManager.main.ProcessPurchasedItems();
        InventoryManager.main.UpdateHealth();
        InventoryManager.main.SelectAutomatically();
    }

}
