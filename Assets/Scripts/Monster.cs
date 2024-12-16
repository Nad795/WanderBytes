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
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
        audioManager.PlaySFX(audioManager.attack);
        currentHP -= damage;
        Debug.Log($"{monsterName} menerima {damage} damage! HP tersisa: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
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
