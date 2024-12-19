using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private PlayerInputSO playerInputSO;
    [SerializeField] private UIInputSO uiInputSO;
    [SerializeField] private StopUI stopUI;

    [SerializeField] private float maxEnergy;
    private float _currentEnergy;
    
    public UnityEvent<float> OnEnergyChanged;
    public UnityEvent OnEnergyMaxChanged;

    private void Awake()
    {
        uiInputSO.OnMenuEvent += HandleUIEvent;
        _currentEnergy = 0;
    }

    protected override void OnDisable()
    {
        uiInputSO.OnMenuEvent -= HandleUIEvent;
        base.OnDisable();
    }

    public void SetEnergy(float energy)
    {
        _currentEnergy += energy;
        if (_currentEnergy >= maxEnergy)
        {
            OnEnergyMaxChanged?.Invoke();
        }
        OnEnergyChanged?.Invoke(_currentEnergy);
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
