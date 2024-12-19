using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class BulletCollider : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private LayerMask whatIsTarget;
    public float energy;
    public UnityEvent OnDeadEvent;
    public bool isHeal;

    private void OnEnable()
    {
        isHeal = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & whatIsTarget) != 0)
        {
            if (other.TryGetComponent(out EntityUltimateComp comp))
            {
                Debug.Log("zz");
                if (isHeal)
                    comp.SetPlayerValue(energy);
                else
                    comp.SetUltimateValue(energy);
            }

            OnDeadEvent?.Invoke();
            bullet.BulletDetected();
        }
    }
}