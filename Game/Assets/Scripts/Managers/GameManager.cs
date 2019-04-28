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
    private GameObject youDiedScreen;

    [SerializeField]
    private GameObject menuScreen;


    [SerializeField]
    private GameObject wellDoneScreen;


    private Transform worldParent;

    public Transform WorldParent { get { return worldParent; } }
    GameConfig config;

    private bool inGame = false;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Cursor.visible = false;
        if (inGame && Input.GetKeyDown(KeyCode.Escape))
        {
            ShowMenu();
        }
    }

    public void ShowMenu()
    {
        Time.timeScale = 0f;
        menuScreen.SetActive(true);
    }

    public void HideMenu()
    {
        menuScreen.SetActive(false);
    }

    public void BackToGame()
    {
        Time.timeScale = 1f;
        HideMenu();
        UIInventoryManager.main.UseCrosshair();
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
    }

    public void Restart(bool levelEnd)
    {
        if (levelEnd)
        {
            wellDoneScreen.SetActive(false);
            ShowShop();
            UIShop.main.ShowNextLevelButton();
        }
        else
        {
            HideMenu();
            HideYouDiedScreen();
            ShowShop();
        }
    }

    public void ShowYouDiedScreen()
    {
        inGame = false;
        youDiedScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HideYouDiedScreen()
    {
        youDiedScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowShop()
    {
        Destroy(worldParent.gameObject);
        UIInventoryManager.main.ClearItems();
        tiledMap.ResetContainers();
        InventoryManager.main.ResetPurchasedItems();
        //InventoryManager.main.ProcessPurchasedItems();
        InventoryManager.main.Init();
        InventoryManager.main.ShowShop();
        InventoryManager.main.ResetHealth();
        InventoryManager.main.UpdateHealth();
        SoundManager.main.FadeGameToMenu();
        inGame = false;
    }

    public void KillEnemy(ProjectileConfig projectile)
    {
        Debug.Log(string.Format("Enemy was killed by {0}", projectile.Name));
        numberOfEnemies -= 1;
        Debug.Log("Number Going down: "+ numberOfEnemies);
        if (numberOfEnemies <= 0)
        {
            inGame = false;
            wellDoneScreen.SetActive(true);
            /*
            InventoryManager.main.ResetHealth();
            InventoryManager.main.ResetPurchasedItems();
            InventoryManager.main.ProcessPurchasedItems();
            InventoryManager.main.UpdateHealth();
            UIShop.main.SetCurrency(InventoryManager.main.GetHealth());*/
        }
    }

    int numberOfEnemies = 0;
    public void SetNumberOfEnemies(int number)
    {
        numberOfEnemies = number;
        Debug.Log("Number: "+ numberOfEnemies);
    }

    public void StartCurrentLevel()
    {
        if (!InventoryManager.main.PurchasesHaveBeenMade())
        {
            return;
        }
        InventoryManager.main.HideShop();
        if (config == null)
        {
            config = ConfigManager.main.GetConfig("GameConfig") as GameConfig;
            config.CurrentLevel = config.FirstLevel;
        }
        if (worldParent != null)
        {
            Destroy(worldParent.gameObject);
        }
        SoundManager.main.FadeMenuToGame();
        worldParent = Instantiate(config.ContainerPrefab);
        worldParent.name = config.CurrentLevel.Name;
        tiledMap.Initialize();
        UIInventoryManager.main.UseCrosshair();
        InventoryManager.main.ProcessPurchasedItems();
        InventoryManager.main.UpdateHealth();
        InventoryManager.main.SelectAutomatically();
        inGame = true;
    }

    public void StartNextLevel()
    {
        if (!InventoryManager.main.PurchasesHaveBeenMade())
        {
            return;
        }
        InventoryManager.main.HideShop();
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
        SoundManager.main.FadeMenuToGame();
        worldParent = Instantiate(config.ContainerPrefab);
        worldParent.name = config.CurrentLevel.Name;
        tiledMap.Initialize();
        UIInventoryManager.main.UseCrosshair();
        InventoryManager.main.ProcessPurchasedItems();
        InventoryManager.main.UpdateHealth();
        InventoryManager.main.SelectAutomatically();
        inGame = true;
    }

}
