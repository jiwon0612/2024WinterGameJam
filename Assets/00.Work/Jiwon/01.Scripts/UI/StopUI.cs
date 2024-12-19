using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class StopUI : UIBase
{
    [SerializeField] private SettingUI setting;
    
    private CanvasGroup _canvasGroup;
    public bool IsActive;

    protected override void Awake()
    {
        base.Awake();
        _canvasGroup = GetComponent<CanvasGroup>();
        
        HideUI();
    }
    
    

    public void ShowUI()
    {
        IsActive = true;
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public void HideUI()
    {
        setting.HideSettingUI();
        IsActive = false;
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
    

    public void ClickStartBtn()
    {
        GameManager.Instance.HandleUIEvent();
    }

    public void ClickSettingBtn()
    {
        setting.ShowSettingUI();
    }

    public void ClickTitleBtn(int num) => SceneManager.LoadScene(num);

    
}
