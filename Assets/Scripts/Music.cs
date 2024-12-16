using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioClip music;

    private void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ChangeMusic(music);
        }
    }
}
