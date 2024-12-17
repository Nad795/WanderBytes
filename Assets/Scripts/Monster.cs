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
        player.TakeDamage(attackDamage);
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        audioManager.PlaySFX(audioManager.attack);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        audioManager.PlaySFX(audioManager.monsterDie);
        Destroy(gameObject);

        MineManager mineManager = FindObjectOfType<MineManager>();
        if (mineManager != null)
        {
            mineManager.MonsterDefeated();
        }
    }
}
