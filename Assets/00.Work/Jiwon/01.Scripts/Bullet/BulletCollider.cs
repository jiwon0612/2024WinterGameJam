using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class BulletCollider : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private LayerMask whatIsTarget;
    [SerializeField] private float energy;
    public UnityEvent OnDeadEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & whatIsTarget) != 0)
        {
            if (other.TryGetComponent(out EntityUltimateComp comp))
                comp.SetUltimateValue(energy);
            
            OnDeadEvent?.Invoke();
            bullet.BulletDetected();
        }
    }
}