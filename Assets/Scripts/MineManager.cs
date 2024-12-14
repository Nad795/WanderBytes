using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wanderbytes; // Tambahkan namespace Wanderbytes

public class MineManager : MonoBehaviour
{
    public string mineName; // Nama mine ini
    private int remainingMonsters; // Jumlah monster yang tersisa

    private void Start()
    {
        // Hitung semua monster yang ada di scene ini
        remainingMonsters = FindObjectsOfType<Monster>().Length;
        Debug.Log($"Mine {mineName} memiliki {remainingMonsters} monster.");
    }

    public void MonsterDefeated()
    {
        remainingMonsters--;
        Debug.Log($"Monster dikalahkan! Sisa monster: {remainingMonsters}");

        if (remainingMonsters <= 0)
        {
            CompleteMine();
        }
    }

    private void CompleteMine()
    {
        // Tandai mine ini sebagai selesai menggunakan GameState.Instance
        if (Wanderbytes.GameState.Instance != null)
        {
            Wanderbytes.GameState.Instance.CompleteMine(mineName);
        }

        Debug.Log($"Semua monster di {mineName} telah dikalahkan!");
    }
}

