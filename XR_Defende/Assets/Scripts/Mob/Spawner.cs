using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject mobPrefab;

    //�ڵ����� ���к���
    public bool playOnStart = true; 
    //wave���� ������ ���� �����ϱ� ���� �ʱⰪ
    public float startFactor = 1f;
    //wave���� ������ ������ �ø��� ���� ����
    public float additiveFactor = 0.1f;
    //�� wave �׷츶�� ���� ������
    public float delayPerSpawnGroup = 3f;

    // Start is called before the first frame update
    void Start()
    {
        if (playOnStart) Play();
    }

    //������ �����ϴ� �Լ�
    public void Play()
    {
        //�����ʸ� Ű�� �Լ�
        StartCoroutine(Process());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    IEnumerator Process()
    {
        var factor = startFactor;

        while (true)
        {
            yield return new WaitForSeconds(delayPerSpawnGroup);

            yield return StartCoroutine(SpawnProcess(factor));

            factor += additiveFactor;

        }
    }


    IEnumerator SpawnProcess(float factor)
    {
        var count = Random.Range(factor, factor * 2);

        for(int i = 0; i < count; i++)
        {
            Spawn();

            if(Random.value < 0.2f)
            {
                yield return new WaitForSeconds(Random.Range(0.01f, 0.02f));
            }
        }
    }

    private void Spawn()
    {
        Instantiate(mobPrefab,transform.position,transform.rotation);
    }
}
