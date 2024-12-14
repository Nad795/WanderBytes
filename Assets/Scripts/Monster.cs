using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IInteractable
{
    public string monsterName; 
    public int maxHP; 
    public int currentHP;
    public int attackDamage; 

    private void Start()
    {
        currentHP = maxHP; 
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
        currentHP -= damage;
        Debug.Log($"{monsterName} menerima {damage} damage! HP tersisa: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{monsterName} telah mati!");
        Destroy(gameObject); 
    }
}
