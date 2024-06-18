using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlahLineRenderer : MonoBehaviour
{
    public float duration = 0.05f; //지속시간

    private LineRenderer target;

    private void Awake()
    {
        target = GetComponent<LineRenderer>();
    }

    public void Call()
    {
        StopAllCoroutines();
        StartCoroutine(Process());
    }

    IEnumerator Process()
    {
        target.enabled = true;
        yield return new WaitForSeconds(duration);
        target.enabled = false;
    }
}
