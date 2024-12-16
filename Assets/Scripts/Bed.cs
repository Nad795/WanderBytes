using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour, IInteractable
{
    AudioManager audioManager;

    public void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void Interact()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            audioManager.PlayMusic(audioManager.rest);
            player.ResetHP(); 
        }
    }
}
