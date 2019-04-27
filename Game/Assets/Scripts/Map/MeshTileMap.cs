using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TiledSharp;

//[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class MeshTileMap : MonoBehaviour
{
    private Vector2[] tiles;

    private List<Vector3> vertices = new List<Vector3>();

    private List<Vector3> normals = new List<Vector3>();

    private List<int> triangles = new List<int>();

    private List<Vector2> uv = new List<Vector2>();
    private int squareCount;
    private Mesh mesh;

    private TmxLayer layer;

    private float tileUnit;


    private int width;

    private GameConfig config;


    public void Init(TmxLayer tmxLayer, TmxMap map)
    {
        config = ConfigManager.main.GetConfig("GameConfig") as GameConfig;
        layer = tmxLayer;
        width = map.Width;
        GenerateMesh();
        DeterminePosition();
    }

    private void DeterminePosition() {
        transform.position = new Vector3(0.5f,  width - 0.5f, 0f);
    }

    public void GenerateMesh()
    {
        int tiles = 8;
        tileUnit = 1.0f / tiles;
        GetTiles(tiles);
        LoadMap();
        UpdateMesh();
    }

    void GetTiles(int numTiles)
    {

        // Unity & Tiled texture mapping:
        /*
         Unity:
         x -> 0  1  2  3 
           .-------------.
         0 | 12 13 14 15 |
         1 | 09 10 11 11 |
         2 | 04 05 06 07 |
         3 | 00 01 02 03 |
           '-------------'
         y
         
         so:
         x = 3, y = 3 is 03
         x = 3, y = 0 is 15
         
         Tiled:

         x -> 0  1  2  3 
           .-------------.
         0 | 00 01 01 03 |
         1 | 04 05 06 07 |
         2 | 08 09 10 11 |
         3 | 12 13 14 15 |
           '-------------'
         y
        */
        tiles = new Vector2[numTiles * numTiles];
        for (int z = 0; z < numTiles; z++)
        {
            for (int x = 0; x < numTiles; x++)
            {
                tiles[x + numTiles * z] = new Vector2(x, numTiles - z - 1);
            }
        }
    }


    void LoadMap()
    {
        foreach (TmxLayerTile tile in layer.Tiles)
        {
            // WARGNING: may hang unity (MANY prints!)
            //print(mapData.tiles[i].tileSetId + " " + mapData.tiles[i].x + " " + mapData.tiles[i].y + "-----------------");
            //print("");
            //print(mapData.tiles[i].tileSetId);
            GenerateTile(tile);
        }
    }

    void GenerateTile(TmxLayerTile tile)
    {

        if (tile.Gid < 0) {
            return;
        }
        float x = tile.X;
        float z = tile.Y;
        Vector3 normal = Vector3.up;
        vertices.Add(new Vector3(-x, 0, z));
        vertices.Add(new Vector3(-x + 1, 0, z));
        vertices.Add(new Vector3(-x + 1, 0, z - 1));
        vertices.Add(new Vector3(-x, 0, z - 1));
        normals.Add(normal);
        normals.Add(normal);
        normals.Add(normal);
        normals.Add(normal);

        triangles.Add(squareCount * 4);
        triangles.Add((squareCount * 4) + 1);
        triangles.Add((squareCount * 4) + 3);
        triangles.Add((squareCount * 4) + 1);
        triangles.Add((squareCount * 4) + 2);
        triangles.Add((squareCount * 4) + 3);

        Vector2 texture = tiles[tile.Gid - 1];
        float fiddleLeft = 0.005f;
        float fiddleRight = 0.005f;
        float fiddleTop = 0.005f;
        float fiddleBottom = 0.005f;
        float left = tileUnit * texture.x + tileUnit - fiddleLeft;
        float top = tileUnit * texture.y + fiddleTop;
        float right = tileUnit * texture.x + fiddleRight;
        float bottom = tileUnit * texture.y + tileUnit - fiddleBottom;

        uv.Add(new Vector2(left, top));
        uv.Add(new Vector2(right, top));
        uv.Add(new Vector2(right, bottom));
        uv.Add(new Vector2(left, bottom));
        squareCount++;
    }


    void UpdateMesh()
    {
        mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.normals = normals.ToArray();
        mesh.uv = uv.ToArray();
        mesh.RecalculateNormals();

        squareCount = 0;
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshCollider meshCollider = GetComponent<MeshCollider>();

        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;

        vertices.Clear();
        triangles.Clear();
        normals.Clear();
        uv.Clear();

    }

}