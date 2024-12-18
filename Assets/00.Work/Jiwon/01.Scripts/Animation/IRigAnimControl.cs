using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IRigAnimControl
{
    public GameObject RigObject { get;}
    public void InitAnimControl(EntityRenderer renderer);
}
