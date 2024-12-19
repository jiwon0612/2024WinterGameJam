using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class ImpulseFeedback : Feedback
{
    private CinemachineImpulseSource _camImpulse;
    [SerializeField] private float impulsePower;
 
    private void Awake()
    {
        _camImpulse = GetComponent<CinemachineImpulseSource>();
    }

    public override void PlayeFeedback()
    {
        _camImpulse.GenerateImpulse(impulsePower);
    }

    public override void StopFeedback()
    {
        
    }
}
