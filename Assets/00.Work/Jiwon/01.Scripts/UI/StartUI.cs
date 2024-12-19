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
        settingUI.ShowSettingUI();
    }
    
    public void ClickExitBtn() => Application.Quit();
}
