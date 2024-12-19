using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private PlayerInputSO playerInputSO;
    [SerializeField] private UIInputSO uiInputSO;
    [SerializeField] private StopUI stopUI;

    private void Awake()
    {
        uiInputSO.OnMenuEvent += HandleUIEvent;
    }

    protected override void OnDisable()
    {
        uiInputSO.OnMenuEvent -= HandleUIEvent;
        base.OnDisable();
    }

    public void HandleUIEvent()
    {
        if (stopUI.IsActive)
        {
            playerInputSO.SetActive(true);
            stopUI.HideUI();
            Time.timeScale = 1;
            
        }
        else if (!stopUI.IsActive)
        {
            playerInputSO.SetActive(false);
            Time.timeScale = 0;
            stopUI.ShowUI();
        }
    }
}
