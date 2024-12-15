using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Wanderbytes;

public class Player : MonoBehaviour, IInteractable
{
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;
    private Animator animator;
    public LayerMask solidBGLayer;
    public LayerMask interactableLayer;
    public LayerMask mine1Layer;
    public LayerMask mine2Layer;
    public LayerMask mine3Layer;
    public LayerMask mine4Layer;
    public LayerMask mine5Layer;
    public LayerMask houseLayer;
    public LayerMask outdoorLayer;

    public int maxHP = 500; 
    public int currentHP; 

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHP = maxHP; 
    }

    public void HandleUpdate()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }

        animator.SetBool("isMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }
    }

    public void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        Debug.DrawLine(transform.position, interactPos, Color.red, 1f);

        var colliderMonster = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);
        // Cek apakah collider adalah Monster
        Monster monster = colliderMonster.GetComponent<Monster>();
        if (monster != null)
        {
            int playerAttackDamage = 50;
            Debug.Log($"Player menyerang {monster.monsterName} dengan {playerAttackDamage} damage!");
            monster.TakeDamage(playerAttackDamage);

            monster.Attack(this);
        }

        var colliderBed = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);
        // Cek apakah collider adalah Monster
        Bed bed = colliderBed.GetComponent<Bed>();
        if (bed != null)
        {
            Debug.Log("Player sedang beristirahat di kasur!");
            bed.Interact();
        }
        
        var colliderNPC = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);
        // Cek apakah collider adalah Monster
        NPC npc = colliderNPC.GetComponent<NPC>();
        if (npc != null)
        {
            npc.Interact();
        }
    }


    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;

        ChangeScene();
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidBGLayer | interactableLayer) != null)
        {
            return false;
        }
        return true;
    }

    private void ChangeScene()
    {
        var mines = new Dictionary<LayerMask, string>
        {
            {mine1Layer, "Mine Vampire"},
            {mine2Layer, "Mine 2"},
            {mine3Layer, "Mine 3"},
            {mine4Layer, "Mine 4"},
            {mine5Layer, "Mine 5"}
        };

        foreach (var mine in mines)
        {
            if(Physics2D.OverlapCircle(transform.position, 0.01f, mine.Key) != null)
            {
                string mineName = mine.Value;

                if (!Wanderbytes.GameState.Instance.completed.Contains(mineName))
                {
                    Debug.Log($"Memasuki {mineName}...");
                    SceneManager.LoadScene(mineName);
                }
                else
                {
                    Debug.Log($"{mineName} sudah selesai, Anda tidak bisa masuk lagi.");
                }
                return;
            }
        }

        if (Physics2D.OverlapCircle(transform.position, 0.01f, houseLayer) != null)
        {
            Debug.Log("PINDAH RUANGAN WOY!!!");
            SceneManager.LoadScene("Rumah");
        }
        else if (Physics2D.OverlapCircle(transform.position, 0.01f, outdoorLayer) != null)
        {
            Debug.Log("PINDAH RUANGAN WOY!!!");
            SceneManager.LoadScene("Outdoor");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log($"Player menerima {damage} damage! HP tersisa: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player telah mati!");
        // Tambahkan logika game over di sini
    }

    public void ResetHP()
    {
        currentHP = maxHP; 
        Debug.Log("Player telah beristirahat dan HP telah dipulihkan!");
    }
}
