using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class Bomb : MonoBehaviour
{
    public enum State
    {
        Idle, // 대기
        Drop  // 대기중이 아닐때
    }

    public float explosionRadius; // 폭발반경
    public LayerMask explosionhittableMask; // 폭발을 일으킬 대상의 레이어마스크

    public float recyleDelay = 1f; // 재생성 시간

    public UnityEvent OnExplosion;
    public UnityEvent OnRecycle;

    private State state;

    public void Drop()
    {
        state = State.Drop;
        if (gameObject.activeInHierarchy) StartCoroutine(ExplosionTimerProcess());
    }
    IEnumerator ExplosionTimerProcess()
    {
        yield return new WaitForSeconds(3f);

        Explosion();
    }

    public void Throw()
    {
        var interactable = GetComponent<XRGrabInteractable>();
        interactable.interactionManager.CancelInteractableSelection((IXRSelectInteractable)interactable);

        var rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(new Vector3(0f, 150f, 300f));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (state == State.Idle) return;

        StopAllCoroutines();
        Explosion();
    }
    private void Explosion()
    {
        var overlaps = Physics.OverlapSphere(transform.position, explosionRadius, explosionhittableMask, QueryTriggerInteraction.Collide);

        foreach (var overlap in overlaps) 
        {
            var hitObject = overlap.GetComponent<Hittable>();
            hitObject?.Hit();
        }

        OnExplosion?.Invoke(); // 폭발이 일어난것을 알림
        //재생성 기능
        Invoke(nameof(Recycle), recyleDelay);
    }

    private void Recycle()
    {
        state = State.Idle;

        OnRecycle?.Invoke();    //다시 재생성을 알림
    }

}
