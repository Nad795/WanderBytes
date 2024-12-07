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

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void HandleUpdate()
    {
        if(!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // Debug.Log("This is input.x" + input.x);
            // Debug.Log("This is input.y" + input.y);

            if(input.x != 0) input.y = 0;

            if(input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if(IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }

        animator.SetBool("isMoving", isMoving);

        if(Input.GetKeyDown(KeyCode.Space))
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
        if(collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
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
        if(Physics2D.OverlapCircle(targetPos, 0.2f, solidBGLayer | interactableLayer) != null)
        {
            return false;
        }
        return true;
    }

    private void ChangeScene()
    {
        if(Physics2D.OverlapCircle(transform.position, 0.01f, mine1Layer) != null)
        {
            Debug.Log("PINDAH RUANGAN WOY!!!");
            SceneManager.LoadScene("Mine Vampire");
        }
        else if(Physics2D.OverlapCircle(transform.position, 0.01f, mine2Layer) != null)
        {
            Debug.Log("PINDAH RUANGAN WOY!!!");
            SceneManager.LoadScene("Mine 2");
        }
        else if(Physics2D.OverlapCircle(transform.position, 0.01f, mine3Layer) != null)
        {
            Debug.Log("PINDAH RUANGAN WOY!!!");
            SceneManager.LoadScene("Mine 3");
        }
        else if(Physics2D.OverlapCircle(transform.position, 0.01f, mine4Layer) != null)
        {
            Debug.Log("PINDAH RUANGAN WOY!!!");
            SceneManager.LoadScene("Mine 4");
        }
        else if(Physics2D.OverlapCircle(transform.position, 0.01f, mine5Layer) != null)
        {
            Debug.Log("PINDAH RUANGAN WOY!!!");
            SceneManager.LoadScene("Mine 5");
        }
        else if(Physics2D.OverlapCircle(transform.position, 0.01f, houseLayer) != null)
        {
            Debug.Log("PINDAH RUANGAN WOY!!!");
            SceneManager.LoadScene("Rumah");
        }
    }
}