using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnLookAt : MonoBehaviour
{
    public new Camera camera; //main 카메라 변수
    public Behaviour target;  // 바라볼 대상

    public float thresholdAngel = 30f; // 바라보는 각도
    public float thresholdDuration = 2f; // 시야내에 해당 시간 이상 있어야 활성화

    private bool isLooking = false;  //바라보고 있는 확인하는 bool 변수
    private float showingTime; //시간이 얼마나 지났는지 확인하는 변수

    private void Awake()
    {
        target.enabled = false;
    }
    private void Update()
    {
        var dir = target.transform.position - camera.transform.position; // 카메라가 타겟을 쳐다보고 있는 것으 ㄹ께산하기 위한 vector값
        var angle = Vector3.Angle(camera.transform.position, dir); // 카메라를 기준으로 몇도가 돌아가 있는지 계산

        if(angle <= thresholdAngel)
        {
            //쳐다보고 있는 상태
            if(!isLooking )
            {
                isLooking = true;
                showingTime = Time.time * thresholdDuration;
            }
            else
            {
                // 타겟 ui가 꺼져있고, 현재시간이 최초에 Looking을 판별한 시점부터 2초뒤보다 더 커져 있으면
                if(target.enabled && Time.time >= showingTime)
                {
                    target.enabled = true;
                }
            }
        }
        else
        {
            //쳐다보지 않은 상태일때
            if (isLooking)
            {
                isLooking = false;
                target.enabled = false;
            }
        }
    }


}
