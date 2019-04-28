using UnityEngine;
using TiledSharp;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

public class TiledMap : MonoBehaviour
{
    private int spawnedObjects = 0;

    private float stepInPixels = 64f;

    private List<Transform> containers = new List<Transform>();
    private GameConfig gameConfig;

    private int enemyNumber;

    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera playerCamera;

    public void Initialize()
    {
        gameConfig = ConfigManager.main.GetConfig("GameConfig") as GameConfig;
        // Load all GridObjectConfigs from the folder
        XDocument mapX = XDocument.Parse(gameConfig.CurrentLevel.LevelFile.ToString());
        TmxMap map = new TmxMap(mapX);
        DrawLayers(map);
        SpawnObjects(map);
    }

    private void DrawLayers(TmxMap map)
    {
        int mapHeight = map.Height;

        foreach (TmxLayer layer in map.Layers)
        {
            string layerType = layer.Name;
            if (layerType == "Ground")
            {
                MeshTileMap meshTileMap = Instantiate(gameConfig.MeshTileMapPrefab);
                meshTileMap.Init(layer, map);
            }
            else if (layerType == "Wall")
            {
                CombinedMeshLayer combinedMeshLayer = SpawnWallLayer(layer, mapHeight);
            }
        }
    }

    private CombinedMeshLayer SpawnWallLayer(TmxLayer layer, int mapHeight)
    {
        GameObject wallPrefab = gameConfig.WallPrefab;
        CombinedMeshLayer meshLayer = Instantiate(gameConfig.CombinedMeshLayerPrefab);
        meshLayer.Initialize(layer.Name, mapHeight);
        Transform wallContainer = GetContainer("Walls");
        meshLayer.transform.SetParent(wallContainer);
        foreach (TmxLayerTile tile in layer.Tiles)
        {
            if (tile.Gid != 0)
            {
                GameObject wall = Instantiate(wallPrefab);
                int x = tile.X;
                int y = tile.Y;
                wall.transform.position = new Vector2(x, y);
                MeshFilter meshFilter = wall.GetComponent<MeshFilter>();
                if (meshFilter != null)
                {
                    meshLayer.Add(meshFilter);
                }
                wall.transform.SetParent(GetContainer("DudWalls"));
            }
        }
        meshLayer.Build();
        return meshLayer;
    }

    private void SpawnObjects(TmxMap map)
    {
        enemyNumber = 0;
        int mapHeight = map.Height;
        foreach (TmxObjectGroup objectGroup in map.ObjectGroups)
        {
            foreach (TmxObject tmxObject in objectGroup.Objects)
            {
                int xPos = (int)(tmxObject.X / stepInPixels);
                int yPos = (int)(mapHeight - (tmxObject.Y - stepInPixels) / stepInPixels);
                SpawnObject(
                    xPos,
                    yPos,
                    tmxObject
                );
            }
        }
        GameManager.main.SetNumberOfEnemies(enemyNumber);
    }


    private void SpawnObject(int x, int y, TmxObject tmxObject)
    {
        ObjectConfig objectConfig = ConfigManager.main.GetObjectConfig(tmxObject.Name);
        GameObject gameObject = Instantiate(objectConfig.Prefab);
        if (gameObject.tag == "Player") {
            PlayerCharacter player = gameObject.GetComponent<PlayerCharacter>();
            playerCamera.Follow = player.transform;
            player.Init();
        }
        if (gameObject.tag == "Enemy") {
            Enemy enemy = gameObject.GetComponent<Enemy>();
            enemy.Init();
            enemyNumber += 1;
        }
        gameObject.transform.position = new Vector2(x, y);
        gameObject.transform.SetParent(GameManager.main.WorldParent);
    }

    public void ResetContainers() {
        containers.Clear();
    }

    private Transform GetContainer(string containerName)
    {
        Transform newContainer = null;
        foreach (Transform container in containers)
        {
            if (container.name == containerName)
            {
                newContainer = container;
                break;
            }
        }
        if (newContainer == null)
        {
            newContainer = Instantiate(gameConfig.ContainerPrefab);
            newContainer.name = containerName;
            newContainer.SetParent(GameManager.main.WorldParent);
            containers.Add(newContainer);
        }
        return newContainer;
    }


}
