using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewObjectConfig", menuName = "Custom/New ObjectConfig")]
public class ObjectConfig : ScriptableObject
{

    [SerializeField]
    private GameObject prefab;
    public GameObject Prefab { get { return prefab; } }

    [SerializeField]
    private string configName;
    public string Name { get { return configName; } }

}