using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayVisualizer : MonoBehaviour
{
    public LineRenderer ray; //�ð�ȭ �� ���η�����
    public LayerMask hitRayMask;//����ĳ���� �� ���̾�
    public float distance = 100f;//����ĳ���� �� �����Ÿ� Distance

    public GameObject reticlePoint;//���̰� �ε����� ������ ǥ������ ������Ʈ
    public bool showReticle = true;//��ƼŬ ������������ ���� ����
    private void Awake()
    {
        Off();
        
    }
    public void On()
    {
        StopAllCoroutines();
        StartCoroutine(Process());

    }

    public void Off()
    {
        StopAllCoroutines();
        
        ray.enabled = false;
        reticlePoint.SetActive(false);
    }
    
    IEnumerator Process()
    {
        while (true)
        {
            if(Physics.Raycast(transform.position, transform.forward,out RaycastHit hitInfo,distance ,hitRayMask)) 
            {
                //���̿� �⵿�Ǵ� ��ü�� ������(��� ����)
                ray.SetPosition(1, transform.InverseTransformPoint(hitInfo.point));
                ray.enabled = true;

                reticlePoint.transform.position = hitInfo.point;
                reticlePoint.SetActive(showReticle);
            }
            else
            {
                //�ƹ� �浹�� ������
                ray.enabled = false;
                reticlePoint.SetActive(false);
            }
            yield return null;

        }
    }

}

