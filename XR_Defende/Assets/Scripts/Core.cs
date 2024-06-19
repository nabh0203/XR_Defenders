using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Core : MonoBehaviour
{
    public int maxHP = 10; // �ִ�ü��
    public int hp; // ���� ü��

    public UnityEvent<string> OnHpChanged; // HP�� ��ȭ�� ����� �����ϴ� �̺�Ʈ
    public UnityEvent OnHit; // �ھ Ÿ���� ������
    public UnityEvent OnDestory; //�ھ �ı��Ǿ�����

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
        hp = maxHP; // ������ HP �ʱ�ȭ
        UpdeatUI();
    }
    public void OnTriggerEnter(Collider other)
    {
        //���� �ھ �ٰ��� Ʈ���� ���� �� ���� �ڵ�
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
