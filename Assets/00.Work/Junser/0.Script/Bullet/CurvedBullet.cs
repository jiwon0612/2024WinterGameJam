using UnityEngine;

public class CurvedBullet : Bullet
{
    private float bezierScale=1;

    protected override void SetRealPos()
    {
        bezierScale = (_targetPos - _lunchPos).magnitude/30;
        float currentReached = Util.InverseVectorLerp(_lunchPos, _targetPos, _basePos);
        realPos = Util.CubicBezierCurve(
            new Vector3(0, 0, 0),
            new Vector3(0, 15f, 5f) + Random.insideUnitSphere,
            new Vector3(0,1, 7.5f) + Random.insideUnitSphere,
            new Vector3(0,0,30),
            currentReached) * bezierScale;

        print(realPos);
    }
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        int segmentCount = 20;
        Vector3 previousPoint = Vector3.zero;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_lunchPos, _targetPos);
        bezierScale = (_targetPos - _lunchPos).magnitude / 30;
        for (int i = 1; i <= segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            Vector3 pointOnCurve = Util.CubicBezierCurve(new Vector3(0, 0, 0),
            new Vector3(0, 15f, 5f),
            new Vector3(0, 1, 7.5f),
            new Vector3(0, 0, 30), 
            t)*bezierScale;

            // Draw line between the previous point and the current point
            Debug.DrawLine(previousPoint, pointOnCurve, Color.red);
            previousPoint = pointOnCurve;
        }
    }
#endif
}
