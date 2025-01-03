using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Feedback : MonoBehaviour
{
    public abstract void PlayeFeedback();
    public abstract void StopFeedback();

    private void OnDisable()
    {
        StopFeedback();
    }
}
