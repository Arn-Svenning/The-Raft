using System.Collections;
using System.Collections.Generic;
using TaskAction;
using UnityEngine;

public class ChangeColorOnTrigger : MonoBehaviour
{
    [SerializeField] private bool isFishingTask;
    [SerializeField] private bool isPaddlingTask;
    [SerializeField] private bool isCollectingRainTask;

    private SpriteRenderer[] spriteRenderers;

    private bool isTaskActive = false;
    private bool isPlayerInRange = false;

    private void Start()
    {
        spriteRenderers = GetComponents<SpriteRenderer>();
        if (spriteRenderers.Length == 0)
        {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }

        PaddleRaftTaskAction.OnPaddlingRaft += PaddleRaftTaskAction_OnPaddlingRaft;
        CollectRainTaskAction.OnCollectingRainNoRain += CollectRainTaskAction_OnCollectingRainNoRain;
        FishingTaskAction.OnFishing += FishingTaskAction_OnFishing;
    }

    private void OnDestroy()
    {
        PaddleRaftTaskAction.OnPaddlingRaft -= PaddleRaftTaskAction_OnPaddlingRaft;
        CollectRainTaskAction.OnCollectingRainNoRain -= CollectRainTaskAction_OnCollectingRainNoRain;
        FishingTaskAction.OnFishing -= FishingTaskAction_OnFishing;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("NPC"))
        {
            isPlayerInRange = true;
            UpdateColor();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("NPC"))
        {
            isPlayerInRange = false;
            UpdateColor();
        }
    }

    private void FishingTaskAction_OnFishing(bool isInteracting)
    {
        if (!isFishingTask) return;
        isTaskActive = isInteracting;
        UpdateColor();
    }

    private void CollectRainTaskAction_OnCollectingRainNoRain(bool isInteracting)
    {
        if (!isCollectingRainTask) return;
        isTaskActive = isInteracting;
        UpdateColor();
    }

    private void PaddleRaftTaskAction_OnPaddlingRaft(bool isInteracting)
    {
        if (!isPaddlingTask) return;
        isTaskActive = isInteracting;
        UpdateColor();
    }

    private void UpdateColor()
    {
        Color targetColor = Color.white;

        if (isTaskActive)
        {
            targetColor = Color.green;
        }
        else if (isPlayerInRange)
        {
            targetColor = new Color(1f, 0.5f, 0f); // Orange
        }

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = targetColor;
        }
    }
}

