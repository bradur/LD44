// Date   : 18.02.2019 22:06
// Project: VillageCaveGame
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CombinedMeshLayer : MonoBehaviour {

    private List<MeshFilter> meshFilters;
    private CombineInstance[] combineInstances;

    private MeshFilter meshFilter;

    private MeshCollider meshCollider;

    public void Initialize(string layerName, int mapHeight) {
        meshFilters = new List<MeshFilter>();
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
        name = layerName;
        transform.position = new Vector3(transform.position.x, mapHeight, transform.position.z);
    }

    void Start () {
    
    }

    void Update () {
    
    }
    public void Build() {
        combineInstances = new CombineInstance[meshFilters.Count];
        for (int index = 0; index < meshFilters.Count; index += 1 ) {
            combineInstances[index].mesh = meshFilters[index].sharedMesh;
            combineInstances[index].transform = meshFilters[index].transform.localToWorldMatrix;
            meshFilters[index].gameObject.SetActive(false);
        }
        meshFilter.mesh = new Mesh();
        meshFilter.mesh.CombineMeshes(combineInstances);
        meshCollider.sharedMesh = meshFilter.mesh;
        name = string.Format("{0} ({1} meshes)", name, meshFilters.Count);
    }

    public void Add(MeshFilter filter) {
        meshFilters.Add(filter);
    }
}
