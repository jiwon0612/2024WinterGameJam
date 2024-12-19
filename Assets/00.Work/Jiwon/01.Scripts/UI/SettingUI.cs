using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private float showTime;
    private CanvasGroup _canvasGroup;
    
    private Tween _tween;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        
        HideSettingUI();
        _tween.Complete();
    }

    public void ShowSettingUI()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        transform.DOLocalMoveY(0, showTime);
    }

    public void HideSettingUI()
    {
        _tween = transform.DOLocalMoveY(-Screen.height, showTime)
            .OnComplete(() =>
            {
                _canvasGroup.alpha = 0;
                _canvasGroup.interactable = false;
                _canvasGroup.blocksRaycasts = false;
            });
    }
}
