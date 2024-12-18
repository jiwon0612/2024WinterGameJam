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
    void Update()
    {
        

        int segmentCount = 20;
        Vector3 previousPoint = Vector3.zero;

        for (int i = 1; i <= segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            Vector3 pointOnCurve = CubicBezierCurve(new Vector3(0, 0, 0),
            new Vector3(0, 15f, 5f),
            new Vector3(0, 1, 7.5f),
            new Vector3(0, 0, 30), t);

            // Draw line between the previous point and the current point
            Debug.DrawLine(previousPoint, pointOnCurve, Color.red);
            previousPoint = pointOnCurve;
        }
    }
    public static float InverseVectorLerp(Vector3 a, Vector3 b, Vector3 value)
    {
        Vector3 ab = b - a;
        Vector3 av = value - a;

        if (ab.magnitude == 0) return 0f; 

        float dotProduct = Vector3.Dot(av, ab.normalized); 
        return Mathf.Clamp01(dotProduct / ab.magnitude);    
    }
}
