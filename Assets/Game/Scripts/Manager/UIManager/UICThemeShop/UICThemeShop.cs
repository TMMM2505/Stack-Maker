using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICThemeShop : UICanvas
{
    [SerializeField] private ContentThemeShop contentThemeShop;

    public TextMeshProUGUI txtCoin;
    public Button btnBack;

    private bool changeTheme = false;
    private void Start()
    {
        btnBack.onClick.AddListener(OnClickBtnBack);
        contentThemeShop.clickBuy = SetCoin;
    }

    public void SetCoin()
    {
        txtCoin.text = LevelManager.Ins.GetPlayer().GetCoin().ToString();
    }

    public void SetUp()
    {
        contentThemeShop.SetUp();
        SetCoin();
    }

    private void OnClickBtnBack()
    {
        GameManager.Ins.ChangeState(GameState.GamePlay);
        Close(0);
    }
}
