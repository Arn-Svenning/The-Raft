using NUnit.Framework.Internal.Filters;
using UnityEngine;


namespace NPC
{
    /// <summary>
    /// The NPC reacts when the player enters the trigger zone
    /// </summary>
    public class DetectPlayer : MonoBehaviour
    {
        public bool isPlayerInRange { get; private set; } = false;
        [SerializeField] private GameObject playerCloseSprite;

        private bool isTalking = false;

        private CircleCollider2D detectPlayerCollider;
        private ContactFilter2D playerFilter;
        private void Start()
        {
            detectPlayerCollider = GetComponent<CircleCollider2D>();
            playerFilter = new ContactFilter2D().NoFilter();
        }

        private void Update()
        {
            if (isTalking) return;

            Collider2D[] results = new Collider2D[5];
            int hits = detectPlayerCollider.Overlap(playerFilter, results);

            for (int i = 0; i < hits; i++)
            {
                if (results[i].CompareTag("Player"))
                {
                    isPlayerInRange = true;
                    playerCloseSprite.SetActive(true);
                    break;
                }
            }
        }
        
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (isTalking) return;

            if (collision.CompareTag("Player"))
            {
                isPlayerInRange = false;
                playerCloseSprite.SetActive(false);
            }
        }
        public void HidePlayerCloseSprite()
        {
            isTalking = true;
            playerCloseSprite.SetActive(false);
        }
        public void ShowPlayerCloseSprite() => isTalking = false;
    }
} 
    


