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

        //�׽�Ʈ�� �ڵ�

        Invoke(nameof(Destroy), 3f);

        OnCreated?.Invoke();
    }


    public void Destroy()
    {
        if (isDestroyed) return;
        isDestroyed = true;

        //destroyParticle.Play();
        //destroyAudio.Play();

        //enviromentParticle.Stop();
        //agent.enabled = false;
        //modelGameObject.SetActive(false);

        Destroy(gameObject, destroyDelay);

        OnDestroyed?.Invoke();
    }
}
