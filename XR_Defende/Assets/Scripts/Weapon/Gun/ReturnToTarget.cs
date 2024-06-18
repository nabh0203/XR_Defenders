using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReturnToTarget : MonoBehaviour
{
    public Transform target; // 위치정보
    public float duration = 1f; // 돌아가는데 걸리는 시간
    public AnimationCurve curve = AnimationCurve.EaseInOut(0f,0f,1f,1f); //좀 천천히 이동하다 속도가 붙는 커브선으로 이동


    public UnityEvent OnCompleted; // 타겟으로 돌아가는 이벤트 핸들러

    public void Call()
    {
        if (!gameObject.activeInHierarchy) return;

        StopAllCoroutines();
        StartCoroutine(Process());
    }

    IEnumerator Process()
    {
        if(target == null) yield break;

        var beginTime = Time.time;

        while (true)
        {
            var t = (Time.time - beginTime) / duration;
            if(t >= 1f) break;

            t = curve.Evaluate(t);

            transform.position = Vector3.Lerp(transform.position,target.position, t);

            yield return null;
        }

        transform.position = target.position;

        OnCompleted?.Invoke();
    }
}
