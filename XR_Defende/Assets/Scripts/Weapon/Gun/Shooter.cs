using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{

    public LayerMask hittableMask; //��Ʈ�� �����ϰ� �� ����ũ 
    public GameObject hitEffectPrefab; //��Ʈ�� ������ ��ġ���� �߻���ų ����Ʈ
    public Transform shootPoint;//�ѱ�

    public float shootDelay = 0.1f;//������
    public float maxdistance = 100f;//�����Ÿ�

    public UnityEvent<Vector3> OnShootSuccess; //�� �߻縦 ������������ ��ġ ���� ����
    public UnityEvent OnShootFail; // ���нÿ� �̺�Ʈ


    private void Start()
    {
        Stop();
    }
    //��Ƽ� ����� ��
    public void Play()
    {
        StopAllCoroutines();
        StartCoroutine(Process());
    }
    //�� ����� ��
    public void Stop()
    {
        StopAllCoroutines();
    }
    //���縦 ó���ϴ� �ڷ�ƾ �Լ�
    IEnumerator Process()
    {
        while (true)
        {
            Shoot();
            yield return null;
        }
    }

    //�ܹ߿� ���� �ڵ� ó���� �ϴ� �Լ�
    public void Shoot()
    {
        if(Physics.Raycast(shootPoint.position,shootPoint.forward,out RaycastHit hitInfo, maxdistance, hittableMask))
        {
            Instantiate(hitEffectPrefab, hitInfo.point, Quaternion.identity);

            var hitObject = hitInfo.transform.GetComponent<Hittable>();
            hitObject?.Hit();

            OnShootSuccess?.Invoke(hitInfo.point);
        }
        else
        {
            var hitPoint = shootPoint.position + shootPoint.forward * maxdistance;
            OnShootSuccess?.Invoke(hitPoint);
        }
    }
}
