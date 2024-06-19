using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SurvivalTimeUI : MonoBehaviour
{
    private float startTime;                // 시작시간 기록 변수

    private TextMeshProUGUI textUI;         // 두번째 행 저장할 텍스트 메쉬 프로

    private void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        textUI.text = $"Survival Time\n{Time.time - startTime:0.0}s";
    }
}