using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wanderbytes;

public class MineManager : MonoBehaviour
{
    public string mineName;
    private int remainingMonsters;

    private void Start()
    {
        remainingMonsters = FindObjectsOfType<Monster>().Length;
    }

    public void MonsterDefeated()
    {
        remainingMonsters--;

        if (remainingMonsters <= 0)
        {
            CompleteMine();
        }
    }

    private void CompleteMine()
    {
        if (Wanderbytes.GameState.Instance != null)
        {
            if (Wanderbytes.GameState.Instance.mineStatus.ContainsKey(mineName))
            {
                Wanderbytes.GameState.Instance.CompleteMine(mineName);
            }
        }
    }
}

