using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayVisualizer : MonoBehaviour
{
    public LineRenderer ray; //시각화 할 라인렌더러
    public LayerMask hitRayMask;//레이캐스팅 할 레이어
    public float distance = 100f;//레이캐스팅 할 사정거리 Distance

    public GameObject reticlePoint;//레이가 부딛히는 지점에 표시해줄 오브젝트
    public bool showReticle = true;//라티클 보여줄지말지 구분 변수
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
                //레이에 출동되는 물체가 있을때(배경 포함)
                ray.SetPosition(1, transform.InverseTransformPoint(hitInfo.point));
                ray.enabled = true;

                reticlePoint.transform.position = hitInfo.point;
                reticlePoint.SetActive(showReticle);
            }
            else
            {
                //아무 충돌이 없을때
                ray.enabled = false;
                reticlePoint.SetActive(false);
            }
            yield return null;

        }
    }

}

