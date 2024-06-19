using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Core : MonoBehaviour
{
    public int maxHP = 10; // 최대체력
    public int hp; // 현재 체력

    public UnityEvent<string> OnHpChanged; // HP에 변화가 생기면 갱신하는 이벤트
    public UnityEvent OnHit; // 코어에 타격이 들어갔을때
    public UnityEvent OnDestory; //코어가 파괴되었을때

    private static Core instance;

    public static Core Instance
    {
        get
        {
            if(instance == null)
                instance = GameObject.FindObjectOfType<Core>();

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        hp = maxHP; // 시작지 HP 초기화
        UpdeatUI();
    }
    public void OnTriggerEnter(Collider other)
    {
        //몹이 코어에 다가와 트리거 됐을 시 실행 코드
        var mob = other.GetComponent<Mob>();
        if (mob != null) 
        {
            OnHit?.Invoke();
            DecreaseHP(1);
            mob.Destroy();
        }
        
    }

    private void DecreaseHP(int amount)
    {
        if (hp <= 0) return;

        hp -= amount;
        if(hp <= 0)
        {
            hp = 0;
            OnDestory?.Invoke();
        }

        UpdeatUI();
    }


    private void UpdeatUI()
    {
        OnHpChanged?.Invoke($"HP : {hp}");
    }


}
