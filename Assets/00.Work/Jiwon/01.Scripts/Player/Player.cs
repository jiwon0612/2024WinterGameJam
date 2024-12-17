using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Entity
{
    [SerializeField] private PlayerInputSO input;

    private void Update()
    {
        GetCompo<EntityMover>().SetMove(input.MoveDirection);
    }
}
