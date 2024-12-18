using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerRenderer : EntityRenderer
{
    public SmallWingRig TailRig { get; private set; }
    public SmallWingRig MiddleWing { get; private set; }
    
    public override void AfterInit()
    {
        TailRig = GetRigComp<SmallWingRig>("TailWingRig");
        MiddleWing = GetRigComp<SmallWingRig>("MiddleWingRig");
    }
    
    
}
