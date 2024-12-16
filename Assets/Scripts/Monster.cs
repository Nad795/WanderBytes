using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IInteractable
{
    public string monsterName; 
    public int maxHP; 
    public int currentHP;
    public int attackDamage; 

    AudioManager audioManager;

    private void Start()
    {
        currentHP = maxHP;
        GameObject audioObject = GameObject.FindGameObjectWithTag("Audio");
        if (audioObject != null)
        {
            audioManager = audioObject.GetComponent<AudioManager>();
            if (audioManager == null)
                Debug.LogError("Komponen AudioManager tidak ditemukan pada GameObject dengan tag 'Audio'!");
        }
        else
        {
            Debug.LogError("GameObject dengan tag 'Audio' tidak ditemukan di scene!");
        }
    }

    public void Interact()
    {
        Debug.Log($"{monsterName} diserang oleh Player!");
        TakeDamage(20); 

        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            Attack(player);
        }
    }

    public void Attack(Player player)
    {
        Debug.Log($"{monsterName} menyerang Player dengan {attackDamage} damage!");
        player.TakeDamage(attackDamage);
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage; // Kurangi HP monster
        audioManager.PlaySFX(audioManager.attack);
        Debug.Log($"{monsterName} menerima {damage} damage! HP tersisa: {currentHP}");

        if (currentHP <= 0)
        {
            Die(); // Panggil fungsi Die jika HP monster habis
        }
    }

    private void Die()
    {
        audioManager.PlaySFX(audioManager.monsterDie);
        Debug.Log($"{monsterName} telah mati!");
        Destroy(gameObject);

        MineManager mineManager = FindObjectOfType<MineManager>();
        if (mineManager != null)
        {
            mineManager.MonsterDefeated();
        }
    }
}
