using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    public UnityEvent OnGrab; //잡았을때
    public UnityEvent OnRelease; //놓았을때


    public void Grab(SelectEnterEventArgs args)
    {
        var interactor = args.interactorObject;
        if(interactor is XRDirectInteractor)
        {
            OnGrab?.Invoke();
        }
    }

    public void Release(SelectExitEventArgs args)
    {
        var interactor = args.interactorObject;
        if (interactor is XRDirectInteractor)
        {
            OnRelease?.Invoke();
        }
    }
}
