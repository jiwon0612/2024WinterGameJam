using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletCollider : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private LayerMask whatIsTarget;
    public float energy;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & whatIsTarget) != 0)
        {
            if (other.TryGetComponent(out EntityUltimateComp comp))
                comp.SetUltimateValue(energy);
            
            bullet.BulletDetected();
        }
    }
}