using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapTilling : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private Transform root;
    [SerializeField] private List<TerrainCollider> maps;
    [SerializeField] private float speed;
    [SerializeField] private float minValue;
    
    private void Update()
    {
        root.position += new Vector3(0,0,-speed * Time.deltaTime);

        if (maps[0].transform.position.z < minValue)
        {
            maps[0].transform.position += new Vector3(0,0,4000);
            var item = maps[0];
            maps.Remove(item);
            maps.Add(item);
            Debug.Log("D");
        }
            
    }
}
