using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.ResetHP(); 
        }
    }
}
