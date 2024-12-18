using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu ( menuName = "SO/BulletSet")]
public class BulletSetSO : ScriptableObject
{
    public List<Bullet> bullets;
    public ShotType shotType;

    [HideIfEnum("shotType", (int) ShotType.POINT, (int) ShotType.CICLE)]
    public Vector3 mainPoint;

    [HideIfEnum("shotType", (int)ShotType.POINT, (int) ShotType.CICLE,(int) ShotType.CICLE_UNTARGET)]
    public Vector3 subPoint;

    public float shotDelay;

    [HideIfEnum("shotType", (int)ShotType.POINT, (int) ShotType.AREA)]
    public float radius;

    public void Shot(Vector3 targetPos, Vector3 lunchpos)
    {
        switch (shotType)
        {
            case ShotType.POINT:
                ShotSet(targetPos, lunchpos); 
                break;
            case ShotType.CICLE:
                ShotSet(targetPos, radius, lunchpos);
                break;
            case ShotType.CICLE_UNTARGET:
                ShotSet(mainPoint, radius, lunchpos);
                break;
            case ShotType.AREA:
                ShotSet(mainPoint, subPoint, lunchpos);
                break;
        }
    }
    public IEnumerator ShotSet(Vector3 shotpoint, Vector3 lunchPos)//점 타격
    {
        foreach (Bullet bullet in bullets)
        {
            bullet.Lunch(shotpoint, lunchPos);
            yield return new WaitForSeconds(shotDelay);
        }
        yield return null;
    }
    public IEnumerator ShotSet(Vector3 center, float radius, Vector3 lunchPos)//원 타격
    {
        foreach (Bullet bullet in bullets)
        {
            Vector3 position = Random.insideUnitSphere*radius;
            bullet.Lunch(center + position, lunchPos);
            yield return new WaitForSeconds(shotDelay);
        }
        yield return null;
    }
    public IEnumerator ShotSet(Vector3 shotClamp1, Vector3 shotClamp2, Vector3 lunchPos)//면 타격
    {
        foreach (Bullet bullet in bullets)
        {
            Vector3 position =new Vector3( Random.Range(shotClamp1.x, shotClamp2.x), Random.Range(shotClamp2.y, shotClamp2.y), 0);
            bullet.Lunch(position, lunchPos);
            yield return new WaitForSeconds(shotDelay);
        }
        yield return null;
    }

    public enum ShotType
    {
        POINT,
        CICLE,
        CICLE_UNTARGET,
        AREA
    }
}
