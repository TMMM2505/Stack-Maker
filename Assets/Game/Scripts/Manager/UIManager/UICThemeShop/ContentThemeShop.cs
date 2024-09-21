using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentThemeShop : MonoBehaviour
{
    [SerializeField] private itemThemeShop itemTheme;
    public Action clickBuy;
    public Action<themeMap> selectTheme;

    List<itemThemeShop> itemThemes = new();

    public void SetUp()
    {
        int themeQuantity = LevelManager.Ins.mapData.listMap.Count;
        for(int j = 0; j < themeQuantity; j++)
        {
            for(int i = 0; i < themeQuantity; i++)
            {
                itemTheme = Instantiate(itemTheme);
                itemTheme.transform.SetParent(transform);
                itemTheme.SetUp(LevelManager.Ins.mapData.listMap[i].img, 50 * (i + 1), LevelManager.Ins.mapData.listMap[i].owned, LevelManager.Ins.mapData.listMap[i].theme);
                itemTheme.clickBuy = OnClickBuy;
                itemThemes.Add(itemTheme);
            }
        }
    }

    public void SetSelect(itemThemeShop item)
    {
        for (int i = 0; i < itemThemes.Count; i++)
        {
            if(itemThemes[i] == item)
            {
                itemThemes[i].SetSelected();
                selectTheme?.Invoke(itemThemes[i].GetTheme());
            }
            else
            {
                itemThemes[i].UnSelected();
            }
        }
    }

    public void OnClickBuy()
    {
        clickBuy?.Invoke();
    }
}
