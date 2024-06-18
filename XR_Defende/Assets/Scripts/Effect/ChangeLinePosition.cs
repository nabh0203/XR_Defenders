using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLinePosition : MonoBehaviour
{
    public int index; // 라인런데러의 시작인 끝인지 인덱스 저장할 변수

    private LineRenderer target;

    private void Awake()
    {
        target = GetComponent<LineRenderer>();
    }

    public void Call(Vector3 worldPosition)
    {
        //월드 포지션을 쓰는 녀석인지 체크
        if(target.useWorldSpace)
        {
            //월드좌표일때는 여기서 처리
            target.SetPosition(index,worldPosition);
        }
        else
        {
            //월드좌표가 아닐떄는 여기서 처리
            var localPosition = transform.InverseTransformPoint(worldPosition);
            target.SetPosition(index,localPosition);
        }
    }

}
