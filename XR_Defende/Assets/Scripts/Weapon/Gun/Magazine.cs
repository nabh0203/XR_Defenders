using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Magazine : MonoBehaviour
{
    public int maxBullets = 20; // �ִ� �Ѿ� ����
    public float chargingTime = 2f; // �����ϴµ� �ð�

    private int currentBullets; //���� �Ѿ� ����

    private int CurrentBullets // ���� �Ѿ��� ����(������Ƽ���� �ڵ����� �ۼ�Ʈ ���� �ڵ� ����ϰ� �̺�Ʈ �ڵ鷯�� ȣ�����ֱ� ����)
    {
        get{ return currentBullets; }
        set
        {
            if (value < 0) currentBullets = 0;
            else if (value > maxBullets) currentBullets = maxBullets;
            else currentBullets = value;

            OnBulletChanged?.Invoke(currentBullets);
            OnChargedChange?.Invoke((float)currentBullets / maxBullets);
        }
    }

    public UnityEvent OnReloadStart; // ������ �̺�Ʈ �ڵ鷯
    public UnityEvent OnReloadEnd; //������ ���� �̺�Ʈ �ڵ鷯

    public UnityEvent<int> OnBulletChanged; // �Ѿ� ���� ���� �̺�Ʈ �ڵ鷯
    public UnityEvent<float> OnChargedChange; // �Ѿ� ���� ����� �󸶳� �����ߴ��� �˷��ִ� �̺�Ʈ �ڵ鷯


    private void Start()
    {
        CurrentBullets =maxBullets; //���� ����� �ִ� źâ ����
    }

    public bool Use(int amount = 1)
    {
        if (CurrentBullets >= amount)
        {
            CurrentBullets -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StartReload()
    {
        if(CurrentBullets == maxBullets) return; // ������ �� �ʿ䰡 ������ ����
        StopAllCoroutines();
        StartCoroutine(ReloadProcess());
    }
    public void StopReload()
    {
        StopAllCoroutines();
    }

    IEnumerator ReloadProcess()
    {
        OnReloadStart?.Invoke();
        
        var beginTime = Time.time; // ������ �ð� ���
        var beginBullets =CurrentBullets; // ������ ���۽� �Ѿ˰��� ����
        var needPercent = 1f - ((float)CurrentBullets / maxBullets); // ������ �Ϸ��ϴµ� ���� �ۼ�Ʈ
        var needCahrgingTime = chargingTime * needPercent; // ������ �Ϸ��ϴµ� ���� �ð�


        while (true) 
        { 
            var t  = (Time.time - beginTime) / needCahrgingTime;  //����� �ð��� ������ �ʿ��� �ð����� ������ �����ΰ� ���Ҵ��� Ȯ��            
            if (t >= 1) break; // 1���� ũ�� ����

            CurrentBullets += (int)Mathf.Lerp(beginBullets, maxBullets, t); // 
            yield return null;
        }

        CurrentBullets = maxBullets; // Math.Lerp Ư���� Ȥ�ó� maxBullet�� �ȵǾ� ���� ��츦 ����ؼ�

        OnReloadEnd?.Invoke();
    }
}
