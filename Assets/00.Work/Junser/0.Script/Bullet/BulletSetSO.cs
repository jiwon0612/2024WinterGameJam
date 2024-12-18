
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu ( menuName = "SO/BulletSet")]
public class BulletSetSO : ScriptableObject
{
    public Bullet defaltBullet;
    public ShotType shotType;
    public int shotAmount;
    public string _poolNameSet;

    [HideIfEnum("shotType", (int) ShotType.POINT, (int) ShotType.CICLE)]
    public Vector3 mainPoint;

    [HideIfEnum("shotType", (int)ShotType.POINT, (int) ShotType.CICLE,(int) ShotType.CICLE_UNTARGET)]
    public Vector3 subPoint;

    public float shotDelay;

    [HideIfEnum("shotType", (int)ShotType.POINT, (int) ShotType.AREA)]
    public float radius;

    public IEnumerator Shot(Vector3 targetPos, Vector3 lunchpos)
    {
        switch (shotType)
        {
            case ShotType.POINT:
                return ShotSet(targetPos, lunchpos);
            case ShotType.CICLE:
                return ShotSet(targetPos, radius, lunchpos);
            case ShotType.CICLE_UNTARGET:
                return ShotSet(mainPoint, radius, lunchpos);
            case ShotType.AREA:
                return ShotSet(mainPoint, subPoint, lunchpos);
            default:
                return null;
        }
    }
    public IEnumerator ShotSet(Vector3 shotpoint, Vector3 lunchPos)//점 타격
    {
        for (int i = 0; i<shotAmount;i++)
        {
            Bullet singleAmmo = PoolManager.Instance.Pop(_poolNameSet) as Bullet;
            singleAmmo.gameObject.SetActive(true);
            singleAmmo.Lunch(shotpoint, lunchPos);
            yield return new WaitForSeconds(shotDelay);
        }
        yield return null;
    }
    public IEnumerator ShotSet(Vector3 center, float radius, Vector3 lunchPos)//원 타격
    {
        for (int i = 0; i < shotAmount; i++)
        {
            Bullet singleAmmo = PoolManager.Instance.Pop(_poolNameSet) as Bullet;
            Vector3 position = Random.insideUnitCircle * radius;
            singleAmmo.gameObject.SetActive(true);
            singleAmmo.Lunch(center + position, lunchPos);
            yield return new WaitForSeconds(shotDelay);
        }
        yield return null;
    }
    public IEnumerator ShotSet(Vector3 shotClamp1, Vector3 shotClamp2, Vector3 lunchPos)//면 타격
    {
        for (int i = 0; i < shotAmount; i++)
        {
            Bullet singleAmmo = PoolManager.Instance.Pop(_poolNameSet) as Bullet;
            Vector3 position =new Vector3( Random.Range(shotClamp1.x, shotClamp2.x), Random.Range(shotClamp2.y, shotClamp2.y), 0);
            singleAmmo.gameObject.SetActive(true);
            singleAmmo.Lunch(position, lunchPos);
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
