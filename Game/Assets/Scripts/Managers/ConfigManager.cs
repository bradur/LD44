// Date   : 22.02.2019 21:59
// Project: Game
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ConfigManager : MonoBehaviour {

    public static ConfigManager main;

    private List<Object> configs;

    void Awake() {
        main = this;
        configs = Resources.LoadAll("Configs").ToList();
    }

    void Start () {
    }

    public Object GetConfig(string configName) {
        foreach (Object config in configs) {
            if (config.name == configName) {
                return config;
            }
        }
        return null;
    }
}
