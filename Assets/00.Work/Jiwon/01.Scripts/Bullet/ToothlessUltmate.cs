using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ToothlessUltmate : Bullet
{
    private void Start()
    {
        OnDeadEvent.AddListener(Hit);
    }

    public void Hit()
    {
        GameManager.Instance.GameClaer();
    }
}
