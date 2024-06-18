using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{

    public LayerMask hittableMask; //히트가 가능하게 할 마스크 
    public GameObject hitEffectPrefab; //히트가 됐을시 위치에서 발생시킬 이펙트
    public Transform shootPoint;//총구

    public float shootDelay = 0.1f;//딜레이
    public float maxdistance = 100f;//사정거리

    public UnityEvent<Vector3> OnShootSuccess; //총 발사를 성공했을때의 위치 정보 저장
    public UnityEvent OnShootFail; // 실패시에 이벤트


    private void Start()
    {
        Stop();
    }
    //방아쇠 당겼을 때
    public void Play()
    {
        StopAllCoroutines();
        StartCoroutine(Process());
    }
    //안 당겼을 때
    public void Stop()
    {
        StopAllCoroutines();
    }
    //연사를 처리하는 코루틴 함수
    IEnumerator Process()
    {
        while (true)
        {
            Shoot();
            yield return null;
        }
    }

    //단발에 대한 코드 처리를 하는 함수
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
