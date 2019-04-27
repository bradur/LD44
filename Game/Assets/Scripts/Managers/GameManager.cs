// Date   : 27.04.2019 16:57
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager main;


    void Awake() {
        main = this;
    }

    [SerializeField]
    private TiledMap tiledMap;
    GameConfig config;

    public void StartNextLevel() {
        if (config == null) {
            config = ConfigManager.main.GetConfig("GameConfig") as GameConfig;
            config.CurrentLevel = config.FirstLevel;
        } else {
            config.CurrentLevel = config.CurrentLevel.NextLevel;
            if (config.CurrentLevel == null) {
                Debug.Log("Game over!");
            }
        }
        tiledMap.Initialize();
        InventoryManager.main.ProcessPurchasedItems();
    }

}
