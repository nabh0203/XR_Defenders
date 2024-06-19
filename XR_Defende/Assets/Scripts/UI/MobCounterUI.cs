using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MobCounterUI : MonoBehaviour
{
    private int killCount;      // 죽인 몹수
    private int spawnCount;     // 소환된 몹수;

    private TextMeshProUGUI textUI;         // 첫번째 행 정보들을 담을 텍스트메쉬프로

    private void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }

    private void UpdateUI()
    {
        if (!enabled)
            return;

        textUI.text = $"Kill/Alive/Spawn\n{killCount}/{spawnCount - killCount}/{spawnCount}";
    }

    private void OnEnable()
    {
        killCount = spawnCount = 0;
        UpdateUI();
    }

    public void OnSpawn()
    {
        spawnCount++;
        UpdateUI();
    }

    public void OnKill()
    {
        killCount++;
        UpdateUI();
    }
}