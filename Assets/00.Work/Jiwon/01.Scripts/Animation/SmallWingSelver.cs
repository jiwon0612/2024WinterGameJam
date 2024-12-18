using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Animations.Rigging;

public class SmallWingSelver : MonoBehaviour
{
    [Header("Setting")] 
    [SerializeField] private float rotationValue;
    [SerializeField] private bool isRight;

    private List<DampedTransform> _dampedTransforms;

    private void Awake()
    {
        _dampedTransforms = transform.parent.GetComponentsInChildren<DampedTransform>().ToList();
        if (isRight == false)
        {
            rotationValue *= -1;
        }
        
        SetFoldWing(false);
    }

    private void Start()
    {
        transform.localRotation = Quaternion.Euler(0, 0, rotationValue);
    }

    public void SetFoldWing(bool isFolded)
    {
        for (int i = 0; i < _dampedTransforms.Count; i++)
        {
            _dampedTransforms[i].weight = isFolded ? 1 : 0;
        }
    }
}