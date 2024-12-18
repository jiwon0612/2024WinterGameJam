using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Animations.Rigging;

public class SmallWingSelver : MonoBehaviour, IRigAnimControl
{
    [Header("Setting")] [SerializeField] private float rotationValue;
    [SerializeField] private bool isRight;

    private EntityRenderer _renderer;
    private List<DampedTransform> _dampedTransforms;

    public GameObject RigObject => gameObject;

    public void InitAnimControl(EntityRenderer renderer)
    {
        _renderer = renderer;

        _dampedTransforms = transform.parent.GetComponentsInChildren<DampedTransform>().ToList();
        if (isRight)
            rotationValue *= -1;
        
        transform.rotation = Quaternion.Euler(0, 0, rotationValue);
    }

    public void SetFoldWing(bool isFolded)
    {
        for (int i = 0; i < _dampedTransforms.Count; i++)
        {
            _dampedTransforms[i].weight = isFolded ? 1 : 0;
        }
    }
}