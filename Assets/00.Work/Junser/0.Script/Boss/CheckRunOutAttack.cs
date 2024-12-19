using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRunOutAttack : MonoBehaviour
{
    [SerializeField] float damage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<EntityUltimateComp>(out EntityUltimateComp ultimate))
        {
            ultimate.SetUltimateValue(damage);
        }
    }
}
