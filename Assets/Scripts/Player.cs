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

        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);
        if (collider != null)
        {
            // Cek apakah collider adalah Monster
            Monster monster = collider.GetComponent<Monster>();
            if (monster != null)
            {
                int playerAttackDamage = 50;
                Debug.Log($"Player menyerang {monster.monsterName} dengan {playerAttackDamage} damage!");
                monster.TakeDamage(playerAttackDamage);

                monster.Attack(this);
            }
            else
            {
                // Cek apakah collider adalah Bed (kasur)
                Bed bed = collider.GetComponent<Bed>();
                if (bed != null)
                {
                    Debug.Log("Player sedang beristirahat di kasur!");
                    bed.Interact();
                }
                else
                {
                    Debug.Log("Tidak ada objek yang bisa diinteraksi.");
                }
            }
        }
        else
        {
            Debug.Log("Tidak ada objek di depan player.");
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
        if (Physics2D.OverlapCircle(transform.position, 0.01f, mine1Layer) != null)
        {
            if (!Wanderbytes.GameState.Instance.IsMineCompleted("Mine Vampire"))
            {
                Debug.Log("Memasuki Mine Vampire...");
                SceneManager.LoadScene("Mine Vampire");
            }
            else
            {
                Debug.Log("Mine Vampire sudah selesai, Anda tidak bisa masuk lagi.");
            }
        }
        else if (Physics2D.OverlapCircle(transform.position, 0.01f, mine2Layer) != null)
        {
            if (!Wanderbytes.GameState.Instance.IsMineCompleted("Mine 2"))
            {
                Debug.Log("Memasuki Mine Dragon...");
                SceneManager.LoadScene("Mine 2");
            }
            else
            {
                Debug.Log("Mine Dragon sudah selesai, Anda tidak bisa masuk lagi.");
            }
        }
        else if (Physics2D.OverlapCircle(transform.position, 0.01f, mine3Layer) != null)
        {
            if (!Wanderbytes.GameState.Instance.IsMineCompleted("Mine 3"))
            {
                Debug.Log("Memasuki Mine Babi Ngepet...");
                SceneManager.LoadScene("Mine 3");
            }
            else
            {
                Debug.Log("Mine Babi Ngepet sudah selesai, Anda tidak bisa masuk lagi.");
            }
        }
        else if (Physics2D.OverlapCircle(transform.position, 0.01f, mine4Layer) != null)
        {
            if (!Wanderbytes.GameState.Instance.IsMineCompleted("Mine 4"))
            {
                Debug.Log("Memasuki Mine Demon...");
                SceneManager.LoadScene("Mine 4");
            }
            else
            {
                Debug.Log("Mine Demon sudah selesai, Anda tidak bisa masuk lagi.");
            }
        }
        else if (Physics2D.OverlapCircle(transform.position, 0.01f, mine5Layer) != null)
        {
            if (!Wanderbytes.GameState.Instance.IsMineCompleted("Mine 5"))
            {
                Debug.Log("Memasuki Mine Boss...");
                SceneManager.LoadScene("Mine 5");
            }
            else
            {
                Debug.Log("Mine Boss sudah selesai, Anda tidak bisa masuk lagi.");
            }
        }
        else if (Physics2D.OverlapCircle(transform.position, 0.01f, houseLayer) != null)
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
