using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour, IEntityComponent
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]// 이동 속도
    private float radius = 3f;     // 8자 모양의 반지름
    private float timeElapsed = 0f;

    public void Initialize(Entity entity)
    {

    }

    public void Stop()
    {
        speed = 0f;
    }

    void Update()
    {
        // 시간에 따른 x, y 좌표 계산 (8자 모양 경로)
        timeElapsed += Time.deltaTime * speed;

        float x = radius * Mathf.Sin(timeElapsed);                // x 좌표: Sine 함수
        float y = radius * Mathf.Sin(timeElapsed) * Mathf.Cos(timeElapsed); // y 좌표: Sine * Cosine

        // 오브젝트 위치 업데이트
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
