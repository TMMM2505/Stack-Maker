using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {GamePlay, Setting, NextLevel, ThemeShop}
public class GameManager : Singleton<GameManager>
{
    private UICThemeShop _themeShop;
    private GameState currentState;
    private void Start()
    {
        ChangeState(GameState.GamePlay);
    }

    public void ChangeState(GameState newGameState)
    {
        if(currentState != newGameState) { currentState = newGameState; }

        switch (currentState)
        {
            case GameState.GamePlay:
                {
                    GamePlayState();
                    break;
                }
            case GameState.Setting:
                {
                    SettingState();
                    break;
                }
            case GameState.NextLevel:
                {
                    NextLevelState();
                    break;
                }
            case GameState.ThemeShop:
                {
                    ThemeShopState();
                    break;
                }
        }
    }

    private void ThemeShopState()
    {
        _themeShop = UIManager.Ins.OpenUI<UICThemeShop>();
        _themeShop.SetUp();
        LevelManager.Ins.GetPlayer().DeactiveMoving();
    }

    private void NextLevelState()
    {
        UIManager.Ins.CloseUI<UICGamePlay>();
        UICNextLevel uicNextLevel = UIManager.Ins.OpenUI<UICNextLevel>();
        uicNextLevel.SetCoin();
    }

    private void SettingState()
    {
    }

    private void GamePlayState()
    {
        UICGamePlay uicGamePlay = UIManager.Ins.OpenUI<UICGamePlay>();
        uicGamePlay.SetTxtCoin((int)LevelManager.Ins.GetPlayer().GetCoin());
        uicGamePlay.SetTxtLevel(LevelManager.Ins.GetIndexMap());
        StartCoroutine(ActivePlayer());
    }
    IEnumerator ActivePlayer()
    {
        yield return new WaitForSeconds(0.5f);
        LevelManager.Ins.GetPlayer().ActiveMoving();
    }
}
