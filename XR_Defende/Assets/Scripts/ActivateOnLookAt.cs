using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnLookAt : MonoBehaviour
{
    public new Camera camera; //main ī�޶� ����
    public Behaviour target;  // �ٶ� ���

    public float thresholdAngel = 30f; // �ٶ󺸴� ����
    public float thresholdDuration = 2f; // �þ߳��� �ش� �ð� �̻� �־�� Ȱ��ȭ

    private bool isLooking = false;  //�ٶ󺸰� �ִ� Ȯ���ϴ� bool ����
    private float showingTime; //�ð��� �󸶳� �������� Ȯ���ϴ� ����

    private void Awake()
    {
        target.enabled = false;
    }
    private void Update()
    {
        var dir = target.transform.position - camera.transform.position; // ī�޶� Ÿ���� �Ĵٺ��� �ִ� ���� �������ϱ� ���� vector��
        var angle = Vector3.Angle(camera.transform.position, dir); // ī�޶� �������� ��� ���ư� �ִ��� ���

        if(angle <= thresholdAngel)
        {
            //�Ĵٺ��� �ִ� ����
            if(!isLooking )
            {
                isLooking = true;
                showingTime = Time.time * thresholdDuration;
            }
            else
            {
                // Ÿ�� ui�� �����ְ�, ����ð��� ���ʿ� Looking�� �Ǻ��� �������� 2�ʵں��� �� Ŀ�� ������
                if(target.enabled && Time.time >= showingTime)
                {
                    target.enabled = true;
                }
            }
        }
        else
        {
            //�Ĵٺ��� ���� �����϶�
            if (isLooking)
            {
                isLooking = false;
                target.enabled = false;
            }
        }
    }


}
