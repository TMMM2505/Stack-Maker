using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UICNextLevel : UICanvas
{
    public Button btnNextLevel;
    public TextMeshProUGUI txtCoin;
    public Transform imgLight;

    private void Start()
    {
        btnNextLevel.onClick.AddListener(OnClickBtnNextLevel); 
        imgLight.DORotate(new Vector3(0, 0, 360), 4f, RotateMode.FastBeyond360)
                 .SetLoops(-1, LoopType.Incremental)
                 .SetEase(Ease.Linear);
    }
    private void OnClickBtnNextLevel()
    {
        LevelManager.Ins.LoadLevel();
        StartCoroutine(NextLevel());
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1f);
        LevelManager.Ins.NextMapIndex();
        GameManager.Ins.ChangeState(GameState.GamePlay);
        Close(0);
    }
    public void SetCoin()
    {
        txtCoin.text = LevelManager.Ins.GetCoinFromMap().ToString();
    }
}
