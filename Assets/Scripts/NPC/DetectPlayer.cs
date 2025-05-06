using NUnit.Framework.Internal.Filters;
using PlayerUI;
using System;
using UnityEngine;
using UnityEngine.UI;


namespace NPC
{
    /// <summary>
    /// The NPC reacts when the player enters the trigger zone
    /// </summary>
    public class DetectPlayer : MonoBehaviour
    {
        public bool isPlayerInRange { get; private set; } = false;
        [SerializeField] private GameObject playerCloseSprite;
        [SerializeField] private Button talkToThisNPCButton;
        [SerializeField] private Button talkToBothButton;

        public event Action<Button> OnPlayerExitedInteractRange;

        private bool isTalking = false;

        private CircleCollider2D detectPlayerCollider;
        private ContactFilter2D playerFilter;

        private static int detectedPlayerCounter = 0;

        private bool hasIncreasedCounter = false;
        private void Start()
        {
            talkToThisNPCButton.interactable = false;
            talkToBothButton.interactable = false;
            detectPlayerCollider = GetComponent<CircleCollider2D>();
            playerFilter = new ContactFilter2D().NoFilter();
        }

        private void Update()
        {
            if (isTalking) return;

            if (detectedPlayerCounter == 2)
            {
                talkToBothButton.interactable = true;
            }
            else
                talkToBothButton.interactable = false;

            Collider2D[] results = new Collider2D[5];
            int hits = detectPlayerCollider.Overlap(playerFilter, results);
     
            for (int i = 0; i < hits; i++)
            {
                if (results[i].CompareTag("Player"))
                {
                    isPlayerInRange = true;
                   
                    
                    if(playerCloseSprite != null)
                    {
                        playerCloseSprite.SetActive(true);
                    }
                   
                    if(talkToThisNPCButton != null)
                    {
                        talkToThisNPCButton.interactable = true;
                    }
                   
                    if(!hasIncreasedCounter)
                    {
                        hasIncreasedCounter = true;
                        detectedPlayerCounter++;
                    }

                    break;
                }
            }           
        }

        private void OnTriggerExit2D(Collider2D collision)
        {

            if (collision.CompareTag("Player"))
            {
                isPlayerInRange = false;

                if (playerCloseSprite != null)
                {
                    playerCloseSprite.SetActive(false);
                }

                if (talkToThisNPCButton != null)
                {
                    talkToThisNPCButton.interactable = false;
                }

                OnPlayerExitedInteractRange?.Invoke(talkToThisNPCButton);

                if (hasIncreasedCounter)
                {
                    hasIncreasedCounter = false;
                    detectedPlayerCounter--;
                }
            }
        }
        public void HidePlayerCloseSprite(bool isRespondingToWorldEvent)
        {
            if(!isRespondingToWorldEvent)
            {
                isTalking = true;
            }   
            playerCloseSprite.SetActive(false);
        }
        public void ShowPlayerCloseSprite() => isTalking = false;
    }
} 
    


