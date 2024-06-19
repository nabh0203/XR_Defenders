using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public class Mob : MonoBehaviour
{
    public UnityEvent OnCreated;
    public UnityEvent OnDestroyed;

    public float destroyDelay = 1f;
    public bool isDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {

        //테스트용 코드

        OnCreated?.Invoke();
        MobManager.Instance.OnSpawned(this);
    }


    public void Destroy()
    {
        if (isDestroyed) return;
        isDestroyed = true;
        Destroy(gameObject, destroyDelay);

        OnDestroyed?.Invoke();

        MobManager.Instance.OnDestroyed(this);
    }
}
