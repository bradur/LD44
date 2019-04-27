using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewLevelConfig", menuName = "Custom/New LevelConfig")]
public class LevelConfig : ScriptableObject
{

    [SerializeField]
    private TextAsset levelFile;
    public TextAsset LevelFile { get { return levelFile; } }

    [SerializeField]
    private string configName;
    public string Name { get { return configName; } }

    [SerializeField]
    private LevelConfig nextLevel;
    public LevelConfig NextLevel { get { return nextLevel; } }

}