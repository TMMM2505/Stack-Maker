using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UICGamePlay : UICanvas
{
    public Button btnSetting;
    public Button btnReset;
    public TextMeshProUGUI txtLevel;
    public TextMeshProUGUI txtCoin;

    public Button btnSound;
    public Button btnVibe;
    public Button btnShop;


    private void Start()
    {
        btnSetting.onClick.AddListener(OnClickBtnSetting);
        btnReset.onClick.AddListener(OnClickBtnReset);
        btnSound.onClick.AddListener(OnClickBtnSound);
        btnVibe.onClick.AddListener(OnClickBtnVibe);
        btnShop.onClick.AddListener(OnClickBtnShop);
    }

    private void OnClickBtnReset()
    {
        LevelManager.Ins.LoadLevel();
        StartCoroutine(ResetLevel());
    }
    IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(1f);
        LevelManager.Ins.ResetLevel();
    }
    private void OnClickBtnShop()
    {
        GameManager.Ins.ChangeState(GameState.ThemeShop);
        Close(0);
    }

    private void OnClickBtnVibe()
    {
    }

    private void OnClickBtnSound()
    {
    }

    private void OnClickBtnSetting()
    {
        if(!btnSound.gameObject.active)
        {
            btnSound.gameObject.SetActive(true);
            btnVibe.gameObject.SetActive(true);
        }
        else
        {
            btnSound.gameObject.SetActive(false);
            btnVibe.gameObject.SetActive(false);
        }
    }

    public void SetTxtLevel(int level)
    {
        txtLevel.text = (level + 1).ToString();
    }

    public void SetTxtCoin(int coin)
    {
        txtCoin.text = coin.ToString();
    }

}
