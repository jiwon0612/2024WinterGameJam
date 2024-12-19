using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAiming : MonoBehaviour, IEntityComponent
{
    private Entity _entity;
    private float _zValue;
    
    public void Initialize(Entity entity)
    {
        _entity = entity;
        _zValue = transform.position.z;
    }

    public void SetPosition(Vector2 mouseInput)
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(mouseInput.x, mouseInput.y, _zValue));
        
        transform.position = new Vector3(point.x, point.y, _zValue);
    }
}
