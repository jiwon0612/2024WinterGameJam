using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private float showTime;
    private CanvasGroup _canvasGroup;
    
    private Slider _masterSlider;
    private Slider _sfxSlider;
    private Slider _bgmSlider;
    
    private Tween _tween;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _masterSlider = transform.Find("MasterVolume").GetComponent<Slider>();
        _sfxSlider = transform.Find("SFXVolume").GetComponent<Slider>();
        _bgmSlider = transform.Find("BGMVolume").GetComponent<Slider>();
        
        _masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        _sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 0.5f);
        _bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        
        HideSettingUI();
        _tween.Complete();
    }

    public void ShowSettingUI()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        transform.DOLocalMoveY(0, showTime).SetUpdate(true);
    }

    public void HideSettingUI()
    {
        _tween = transform.DOLocalMoveY(-Screen.height, showTime).SetUpdate(true)
            .OnComplete(() =>
            {
                _canvasGroup.alpha = 0;
                _canvasGroup.interactable = false;
                _canvasGroup.blocksRaycasts = false;
            });
    }
}
