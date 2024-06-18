using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Magazine : MonoBehaviour
{
    public int maxBullets = 20; // 최대 총알 개수
    public float chargingTime = 2f; // 충전하는데 시간

    private int currentBullets; //현재 총알 개수

    private int CurrentBullets // 현재 총알의 개수(프로퍼티에서 자동으로 퍼센트 등을 자동 계산하고 이벤트 핸들러를 호출해주기 위해)
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

    public UnityEvent OnReloadStart; // 재장전 이벤트 핸들러
    public UnityEvent OnReloadEnd; //재장전 종료 이벤트 핸들러

    public UnityEvent<int> OnBulletChanged; // 총알 개수 변경 이벤트 핸들러
    public UnityEvent<float> OnChargedChange; // 총알 개수 변경시 얼마나 충전했는지 알려주는 이벤트 핸들러


    private void Start()
    {
        CurrentBullets =maxBullets; //최초 실행시 최대 탄창 갯수
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
        if(CurrentBullets == maxBullets) return; // 재장전 할 필요가 없으면 리턴
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
        
        var beginTime = Time.time; // 재장전 시간 기록
        var beginBullets =CurrentBullets; // 재장전 시작시 총알개수 저장
        var needPercent = 1f - ((float)CurrentBullets / maxBullets); // 충전을 완료하는데 남은 퍼센트
        var needCahrgingTime = chargingTime * needPercent; // 충전을 완료하는데 남은 시간


        while (true) 
        { 
            var t  = (Time.time - beginTime) / needCahrgingTime;  //진행된 시간을 충전에 필요한 시간으로 나누면 몇프로가 남았는지 확인            
            if (t >= 1) break; // 1보다 크면 종료

            CurrentBullets += (int)Mathf.Lerp(beginBullets, maxBullets, t); // 
            yield return null;
        }

        CurrentBullets = maxBullets; // Math.Lerp 특성상 혹시나 maxBullet이 안되어 있으 경우를 대비해서

        OnReloadEnd?.Invoke();
    }
}
