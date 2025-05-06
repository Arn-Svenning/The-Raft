using System.Collections;
using System.Collections.Generic;
using TaskAction;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeColorOnTrigger : MonoBehaviour
{
    [SerializeField] private TextMeshPro interactText;

    [SerializeField] private bool isFishingTask;
    [SerializeField] private bool isPaddlingTask;
    [SerializeField] private bool isCollectingRainTask;
    [SerializeField] private bool isRepairRaftTask;
    
    private SpriteRenderer[] spriteRenderers;

    private GameObject taskIndicator;

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
        RepairRaftTaskAction.OnRepairingRaft += RepairRaftTaskAction_OnRepairingRaft;

        foreach (Transform child in transform)
        {
            if (child.CompareTag("Task Indicator"))
            {
                taskIndicator = child.gameObject;
                break; 
            }
        }
    }

    

    private void OnDestroy()
    {
        PaddleRaftTaskAction.OnPaddlingRaft -= PaddleRaftTaskAction_OnPaddlingRaft;
        CollectRainTaskAction.OnCollectingRainNoRain -= CollectRainTaskAction_OnCollectingRainNoRain;
        FishingTaskAction.OnFishing -= FishingTaskAction_OnFishing;
        RepairRaftTaskAction.OnRepairingRaft -= RepairRaftTaskAction_OnRepairingRaft;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("NPC"))
        {
            isPlayerInRange = true;
            UpdateColor();

            if(collision.CompareTag("Player"))
            {
                SetInteractTextVisibility(true);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("NPC"))
        {
            isPlayerInRange = false;
            UpdateColor();

            if (collision.CompareTag("Player"))
            {
                SetInteractTextVisibility(false);
            }
        }
    }
    private void Update()
    {
        taskIndicator.SetActive(!isTaskActive);
    }

    private void RepairRaftTaskAction_OnRepairingRaft(bool isInteracting)
    {
        if (!isRepairRaftTask) return;
        isTaskActive = isInteracting;
        UpdateColor();
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
        if (isRepairRaftTask) return;

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
            if (spriteRenderer.CompareTag("Task Indicator")) continue;

            Color finalColor = targetColor;

            if(spriteRenderer.gameObject.CompareTag("PaddleFin"))
            {
                finalColor = new Color(targetColor.r, targetColor.g, targetColor.b, 0.35f);
            }
            spriteRenderer.color = finalColor;
        }
    }
    private void SetInteractTextVisibility(bool showInteractText)
    {
        if (interactText == null || !interactText.transform.parent.gameObject.CompareTag("Player")) return;

        interactText.gameObject.SetActive(showInteractText);

        if(isTaskActive)
        {
            interactText.text = "";
            return;
        }

        if(isPaddlingTask)
        {
            interactText.text = "Press E to paddle";
        }
        else if(isRepairRaftTask)
        {
            interactText.text = "Press E to Repair Raft";
        }
        else if(isCollectingRainTask)
        {
            interactText.text = "Press E to Collect Water";
        }
        else if(isFishingTask)
        {
            interactText.text = "Press E to Fish";
        }
    }
}

