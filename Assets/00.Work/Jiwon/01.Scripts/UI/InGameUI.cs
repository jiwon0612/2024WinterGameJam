using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : UIBase
{
    [SerializeField] private Image hpImage;
    [SerializeField] private Image ultImage;
    [SerializeField] private Image dashImage;
    [SerializeField] private Image attackImage;

    protected override void Awake()
    {
        base.Awake();
        SetUltimate(0);
    }

    public void SetHP(float hp)
    {
        hpImage.fillAmount = hp;
    }

    public void SetUltimate(float ultimate)
    {
        ultImage.fillAmount = ultimate;
    }

    public void SetDash(float dash)
    {
        dashImage.fillAmount = dash;
    }

    public void SetAttack(float damage)
    {
        attackImage.fillAmount = damage;
    }
}
