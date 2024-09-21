using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MapDataConfig", menuName = "Scriptable Objects/map Data Config", order = 1)]

public class MapDataConfig : ScriptableObject
{
    public List<MapData> listMap = new();
}

[Serializable]
public class MapData
{
    public bool owned;
    public themeMap theme;
    public Sprite img;
    public List<ItemMap> map;
}

[Serializable]
public class ItemMap
{
    public int id;
    public float coin;
    public Map prefabMap;
}

[Serializable]
public enum themeMap
{
    theme1 = 0, 
    theme2 = 1,
}