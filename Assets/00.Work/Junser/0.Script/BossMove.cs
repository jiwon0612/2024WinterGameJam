using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour, IEntityComponent
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]// �̵� �ӵ�
    private float radius = 3f;     // 8�� ����� ������
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
        // �ð��� ���� x, y ��ǥ ��� (8�� ��� ���)
        timeElapsed += Time.deltaTime * speed;

        float x = radius * Mathf.Sin(timeElapsed);                // x ��ǥ: Sine �Լ�
        float y = radius * Mathf.Sin(timeElapsed) * Mathf.Cos(timeElapsed); // y ��ǥ: Sine * Cosine

        // ������Ʈ ��ġ ������Ʈ
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
