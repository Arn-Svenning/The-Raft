using Player;
using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public static DialogueTrigger currentActiveTrigger;

    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink File")]
    public TextAsset inkJson;

    public bool playerInRange;

    public DialogueManager manager;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }
    void Update()
    {
        if (playerInRange && !manager.dialogueIsPlaying)
        {
            visualCue.SetActive(true);

        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
            currentActiveTrigger = this;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
            if (currentActiveTrigger == this)
                currentActiveTrigger = null;
        }
    }
}
