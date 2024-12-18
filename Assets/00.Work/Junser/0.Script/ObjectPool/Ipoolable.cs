using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Ipoolable
{
    public string PoolName { get; }
    public GameObject ObjectPrefab { get; }
    public void ResetItem();
}
