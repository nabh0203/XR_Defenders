using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReturnToTarget : MonoBehaviour
{
    public Transform target; // ��ġ����
    public float duration = 1f; // ���ư��µ� �ɸ��� �ð�
    public AnimationCurve curve = AnimationCurve.EaseInOut(0f,0f,1f,1f); //�� õõ�� �̵��ϴ� �ӵ��� �ٴ� Ŀ�꼱���� �̵�


    public UnityEvent OnCompleted; // Ÿ������ ���ư��� �̺�Ʈ �ڵ鷯

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
