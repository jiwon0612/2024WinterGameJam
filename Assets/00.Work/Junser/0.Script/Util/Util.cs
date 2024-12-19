using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static Vector3 CubicBezierCurve(Vector3 start, Vector3 wayPoint1, Vector3 wayPoint2, Vector3 end, float t)
    {
        t = Mathf.Clamp01(t);

        float _1mT = 1 - t;

        Vector3 result = Mathf.Pow(_1mT, 3) * start +
                         3 * Mathf.Pow(_1mT, 2) * t * wayPoint1 +
                         3 * _1mT * Mathf.Pow(t, 2) * wayPoint2 +
                         Mathf.Pow(t, 3) * end;

        return result;
    }
    
    public static float InverseVectorLerp(Vector3 start, Vector3 end, float value)
    {
        if ((end - start).magnitude == 0) return 0f; 

        return (value / (end - start).magnitude);    
    }
}
