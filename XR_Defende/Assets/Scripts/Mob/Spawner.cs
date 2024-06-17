using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject mobPrefab;

    //자동시작 구분변수
    public bool playOnStart = true; 
    //wave마다 몹생성 수를 설정하기 위한 초기값
    public float startFactor = 1f;
    //wave마다 몹생성 비율을 올리기 위한 변수
    public float additiveFactor = 0.1f;
    //몹 wave 그룹마다 생성 딜레이
    public float delayPerSpawnGroup = 3f;

    // Start is called before the first frame update
    void Start()
    {
        if (playOnStart) Play();
    }

    //스포너 구동하는 함수
    public void Play()
    {
        //스포너를 키는 함수
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
