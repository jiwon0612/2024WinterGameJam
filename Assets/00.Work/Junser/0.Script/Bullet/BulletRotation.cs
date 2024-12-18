using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRotation : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed;
    private List<Transform> rotateBullet = new List<Transform>();
    [SerializeField]
    private float radious;
    private void SetRadious()
    {
        for (int i = 0; i < rotateBullet.Count; i++)
        {
            float angle = i*360/rotateBullet.Count*Mathf.Deg2Rad;
            Vector3 postion = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f);
            rotateBullet[i].localPosition = postion*radious;
        }
    }
    private void Start()
    {
        foreach (Transform child in transform)
        {
            rotateBullet.Add(child);
        }
        SetRadious();
    }

    protected virtual void Rotate()
    {
        transform.rotation *= Quaternion.Euler(0, 0, _rotationSpeed*Time.deltaTime);
    }

    private void Update()
    {
        Rotate();
    }
}
