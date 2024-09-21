using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class itemThemeShop : MonoBehaviour
{
    public Image imgMap;
    public Image frame;
    public CheckOwnMap check;
    public Button btnBuy;
    public Button btnTheme;
    public GameObject panel;
    public TextMeshProUGUI txtCoin;
    public GameObject selectedFrame;

    public Action clickBuy;

    private themeMap theme;
    private int coin;
    private bool Owned;

    private void Start()
    {
        btnBuy.onClick.AddListener(OnClickBtnBuy);
        btnTheme.onClick.AddListener(OnClickBtnTheme);

        selectedFrame.gameObject.SetActive(false);
    }

    private void OnClickBtnTheme()
    {
        LevelManager.Ins.ChangeTheme(theme);
    }

    public void SetUp(Sprite map, int coin, bool owned, themeMap theme)
    {
        this.theme = theme;
        Owned = owned;
        imgMap.sprite = map;
        SetState();
        this.coin = coin;
        txtCoin.text = coin.ToString();
    }

    public void SetState()
    {
        if (Owned)
        {
            Color newColor;
            check.SetTick();
            if (ColorUtility.TryParseHtmlString("#FFB500", out newColor))
            {
                frame.color = newColor;
            }
            panel.SetActive(false);
            btnBuy.gameObject.SetActive(false);
        }
        else
        {
            check.SetLock();
            frame.color = Color.gray;
            panel.SetActive(true);
            btnBuy.gameObject.SetActive(true);
        }
    }
    public void OnClickBtnBuy()
    {
        if(LevelManager.Ins.GetPlayer().GetCoin() >= coin)
        {
            Owned = true;
            LevelManager.Ins.GetPlayer().SetCoin((int)LevelManager.Ins.GetPlayer().GetCoin() - coin);
            SetState();
            clickBuy?.Invoke();
        }
    }
    public void SetSelected()
    {
        selectedFrame.gameObject.SetActive(true);
    }
    public void UnSelected()
    {
        selectedFrame.gameObject.SetActive(false);
    }
    public themeMap GetTheme() => theme;
}
