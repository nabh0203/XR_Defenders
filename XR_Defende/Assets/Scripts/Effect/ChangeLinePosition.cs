using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLinePosition : MonoBehaviour
{
    public int index; // ���η������� ������ ������ �ε��� ������ ����

    private LineRenderer target;

    private void Awake()
    {
        target = GetComponent<LineRenderer>();
    }

    public void Call(Vector3 worldPosition)
    {
        //���� �������� ���� �༮���� üũ
        if(target.useWorldSpace)
        {
            //������ǥ�϶��� ���⼭ ó��
            target.SetPosition(index,worldPosition);
        }
        else
        {
            //������ǥ�� �ƴҋ��� ���⼭ ó��
            var localPosition = transform.InverseTransformPoint(worldPosition);
            target.SetPosition(index,localPosition);
        }
    }

}
