using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            Monster monster = collider.GetComponent<Monster>();
            int playerAttackDamage = 50; 
            Debug.Log($"Player menyerang {monster.monsterName} dengan {playerAttackDamage} damage!");
            monster.TakeDamage(playerAttackDamage);

            monster.Attack(this); 
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
            Debug.Log("PINDAH RUANGAN WOY!!!");
            SceneManager.LoadScene("Mine Vampire");
        }
        else if (Physics2D.OverlapCircle(transform.position, 0.01f, mine2Layer) != null)
        {
            Debug.Log("PINDAH RUANGAN WOY!!!");
            SceneManager.LoadScene("Mine 2");
        }
        else if (Physics2D.OverlapCircle(transform.position, 0.01f, mine3Layer) != null)
        {
            Debug.Log("PINDAH RUANGAN WOY!!!");
            SceneManager.LoadScene("Mine 3");
        }
        else if (Physics2D.OverlapCircle(transform.position, 0.01f, mine4Layer) != null)
        {
            Debug.Log("PINDAH RUANGAN WOY!!!");
            SceneManager.LoadScene("Mine 4");
        }
        else if (Physics2D.OverlapCircle(transform.position, 0.01f, mine5Layer) != null)
        {
            Debug.Log("PINDAH RUANGAN WOY!!!");
            SceneManager.LoadScene("Mine 5");
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
}
