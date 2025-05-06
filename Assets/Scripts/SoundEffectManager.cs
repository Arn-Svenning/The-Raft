using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance { get; private set; }

    [SerializeField] private AudioClip raftBreakingSoundEffect;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRaftBreakingSound()
    {
        if (audioSource != null && raftBreakingSoundEffect != null)
        {
            audioSource.PlayOneShot(raftBreakingSoundEffect);
        }
    }

}
