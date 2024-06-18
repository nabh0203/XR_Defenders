using UnityEngine.Events;
using UnityEngine;

public class Hittable : MonoBehaviour
{
    public UnityEvent OnHit;

    public void Hit()
    {
        OnHit?.Invoke();
    }

}
