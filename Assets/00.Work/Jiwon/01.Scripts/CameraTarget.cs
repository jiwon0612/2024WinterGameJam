using System;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private Transform target;
    private float zValue;

    private void Awake()
    {
        zValue = transform.position.z;
    }

    private void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, zValue);
    }
}
