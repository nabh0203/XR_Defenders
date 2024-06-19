using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MobManager : MonoBehaviour
{
    private static MobManager instance;
    public static MobManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<MobManager>();
            return instance;
        }
    }

    public UnityEvent<Mob> OnSpawn, OnDestroy;      // 생성 파괴시 연결할 이벤트

    private List<Mob> mobs = new List<Mob>();       // 생성되어 있는 몹들을 저장할 리스트

    private void Awake()
    {
        instance = this;
    }

    // 소환될 때 실행할 메소드
    public void OnSpawned(Mob mob)
    {
        mobs.Add(mob);
        OnSpawn?.Invoke(mob);
    }

    // 죽을 때 소환될 메소드
    public void OnDestroyed(Mob mob)
    {
        if (mobs.Remove(mob))
        {
            OnDestroy?.Invoke(mob);
        }
    }

    public void DestroyAll()
    {
        while (mobs.Count > 0)
        {
            mobs[0]?.Destroy();
        }
    }
}