using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public MapDataConfig mapData;
    [SerializeField] private CameraFollower camera;
    [SerializeField] private Player player;
    [SerializeField] private Loader loader;

    private Map currentMap;
    private themeMap currentTheme;

    private int indexMap;
    private void Start()
    {
        currentTheme = (themeMap)0;
        indexMap = mapData.listMap[(int)currentTheme].map[indexMap].id;
        Setup();
    }
    public void Setup()
    {
        MapData itemMapData = mapData.listMap[(int)currentTheme];

        if (currentMap && currentMap.gameObject)
        {
            Destroy(currentMap.gameObject);
        }
        currentMap = Instantiate(itemMapData.map[indexMap].prefabMap);
        currentMap.OnInit();

        player.OnInit(currentMap.GetStartPoint());
        player.winAction = OnFinish;
        player.gotoGoal = SetCameraOnFinish;
        player.OnInit(currentMap.GetStartPoint());

        camera.OnStart();
    }
    private void SetCameraOnFinish()
    {
        camera.OnFinish();
    }
    public void OnFinish()
    {
        currentMap.OnFinish();
        player.AddCoin(mapData.listMap[(int)currentTheme].map[indexMap].coin);
        GameManager.Ins.ChangeState(GameState.NextLevel);
    }
    public void NextMapIndex()
    {
        if (indexMap < mapData.listMap[(int)currentTheme].map.Count - 1)
        {
            indexMap++;
            Setup();
        }
    }
    public Player GetPlayer() { return player; }
    public float GetCoinFromMap() => mapData.listMap[(int)currentTheme].map[indexMap].coin;
    public int GetIndexMap() { return indexMap; }
    public void LoadLevel()
    {
        loader.StartLoading();
    }
    public void ResetLevel()
    {
        if(currentMap.gameObject)
        {
            Destroy(currentMap.gameObject);
        }

        player.ResetPlayer();

        Setup();
    }
    public void ChangeTheme(themeMap newThemeMap)
    {
        if(currentMap.gameObject)
        {
            Destroy(currentMap.gameObject);
        }

        currentTheme = newThemeMap;
        ResetLevel();
    }
}
