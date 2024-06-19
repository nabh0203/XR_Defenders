using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TeleportActionHandler : MonoBehaviour
{
    public InputActionReference inputActionRef;

    public UnityEvent OnShow; //레이 생성
    public UnityEvent OnHide; //레이 비생성

    private void OnEnable()
    {
        inputActionRef.action.performed += Action_performed;
        inputActionRef.action.canceled += Action_canceled;
    }

    private void OnDisable()
    {
        inputActionRef.action.performed -= Action_performed;
        inputActionRef.action.canceled -= Action_canceled;
    }
    private void Action_performed(InputAction.CallbackContext obj)
    {
        StartCoroutine(DelayCall(OnShow));
    }

    private void Action_canceled(InputAction.CallbackContext obj)
    {
        StartCoroutine(DelayCall(OnHide));
    }

    IEnumerator DelayCall(UnityEvent e)
    {
        yield return null;
        e?.Invoke();
    }
}
