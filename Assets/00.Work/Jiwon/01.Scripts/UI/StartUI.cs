using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StartUI : UIBase
{
    [SerializeField] private SettingUI settingUI;
    
    public void ClickStartBtn(int num)
    {
        SceneManager.LoadScene(num);
    }

    public void ClickSettingBtn()
    {
        transform.DOLocalMoveX(-Screen.width, 0.5f);
        settingUI.transform.DOLocalMoveX(0, 0.5f);
    }
    
    public void ClickExitBtn() => Application.Quit();
}
