using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class SmallWingRig : MonoBehaviour, IRigAnimControl
{
    private EntityRenderer _renderer;
    private List<SmallWingSelver> _selvers;
    
    public GameObject RigObject => gameObject;
    
    public void InitAnimControl(EntityRenderer renderer)
    {
        _renderer = renderer;
        _selvers = GetComponentsInChildren<SmallWingSelver>().ToList();
    }

    public void SetFold(bool isFold)
    {
        for (int i = 0; i < _selvers.Count; i++)
        {
            _selvers[i].SetFoldWing(isFold);
        }
    }
}
