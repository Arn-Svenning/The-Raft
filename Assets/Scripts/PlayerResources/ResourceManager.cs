using NPC;
using System;
using System.Collections;
using TaskAction;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerResources
{
    /// <summary>
    /// Handles all the player resources
    /// </summary>
    public class ResourceManager : MonoBehaviour
    {
        public static ResourceManager Instance { get; private set; }

        public event Action<float> OnThirstResourceLow;
        public event Action<bool> OnRaftRepaired;
        public event Action<bool> OnRaftCantMove;

        /// <summary>
        /// Show how much of the raft that has been fixed by adjusting transperency
        /// </summary>
        [SerializeField] private SpriteRenderer raftDamageSprite; 

        #region Resources

        public float hunger { get; private set; } = 1f;
        public float thirst { get; private set; } = 1f;
        public float morale { get; private set; } = 1f;
        public float raftHealth { get; private set; } = 1f;

        #endregion

        private const float HUNGER_DECREASE_AMOUNT = 0.005f;
        private const float THIRST_DECREASE_AMOUNT = 0.008f;
        private const float MORAL_DECREASE_AMOUNT = 0.02f;
        private const float RAFT_HEALTH_DECREASE_AMOUNT = 0.05f;

        private const float RESOURCE_MEDIUM_THRESHOLD = 0.5f;
        private const float RESOURCE_LOW_THRESHOLD = 0.2f;

        private const float ORIGINAL_HUNGER_GAIN = 0.3f;
        private const float ORIGINAL_THIRST_GAIN = 0.07f;
        private const float ORIGINAL_RAFT_HEALTH_GAIN = 0.15f;
        private const float ORIGINAL_MORALE_GAIN = 0.07f;

        private float hungerGain = 0.5f;
        private float thirstGain = 0.4f;
        private float raftHealthGain = 0.3f;
        private float moraleGain = 0.07f;

        private bool isThirstResourceMedium = false;
        private bool isThirstResourceLow = false;

        [SerializeField] private bool canDie = true;

        private bool hasRaftLostMovement = false;

        #region bools for UIManager
        public bool hungerMedium { get; private set; } = false;
        public bool hungerLow { get; private set; } = false;

        public bool thirstMedium { get; private set; } = false;
        public bool thirstLow { get; private set; } = false;

        public bool raftHealthMedium { get; private set; } = false;
        public bool raftHealthLow { get; private set; } = false;

        public bool moraleMedium { get; private set; } = false;
        public bool moraleLow { get; private set; } = false;

        #endregion
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            FishingTaskAction.OnFishCaught += FishingTaskAction_OnFishCaught;
            CollectRainTaskAction.OnCollectingRain += CollectRainTaskAction_OnCollectingRain;
            RepairRaftTaskAction.OnRepairingRaft += RepairRaftTaskAction_OnRepairingRaft;
            NPCCasualConversationState.OnIsChatting += CasualChatterTaskAction_OnIsChatting;

            hungerGain = ORIGINAL_HUNGER_GAIN;
            thirstGain = ORIGINAL_THIRST_GAIN;
            raftHealthGain = ORIGINAL_RAFT_HEALTH_GAIN;
            moraleGain = ORIGINAL_MORALE_GAIN;
        }
        private void OnDestroy()
        {
            FishingTaskAction.OnFishCaught -= FishingTaskAction_OnFishCaught;
            CollectRainTaskAction.OnCollectingRain -= CollectRainTaskAction_OnCollectingRain;
            RepairRaftTaskAction.OnRepairingRaft -= RepairRaftTaskAction_OnRepairingRaft;
            NPCCasualConversationState.OnIsChatting -= CasualChatterTaskAction_OnIsChatting;
        }
        private void CheckMorale()
        {
            if(morale <= RESOURCE_MEDIUM_THRESHOLD)
            {
                raftHealthGain = ORIGINAL_RAFT_HEALTH_GAIN / 3;
                thirstGain = ORIGINAL_THIRST_GAIN / 3;
                hungerGain = ORIGINAL_HUNGER_GAIN / 3;
            }
            else
            {
                raftHealthGain = ORIGINAL_RAFT_HEALTH_GAIN;
                thirstGain = ORIGINAL_THIRST_GAIN;
                hungerGain = ORIGINAL_HUNGER_GAIN;
            }
        }
        private void CheckRaftHealth()
        {
            if(raftHealth <= RESOURCE_MEDIUM_THRESHOLD && !hasRaftLostMovement)
            {
                hasRaftLostMovement = true;
                OnRaftCantMove?.Invoke(true);
            }
            else if(raftHealth <= 0)
            {
                if (!canDie) return;

                GameManager.Instance.ChangeGameState(GameManager.GameState.Died);
                Debug.Log("Raft destroyed");
            }
        }

        private void CheckIfPlayerDied()
        {
            if (!canDie) return;

            if(hunger <= 0 || thirst <= 0 || raftHealth <= 0)
            {
                GameManager.Instance.ChangeGameState(GameManager.GameState.Died);
                Debug.Log("YOU DIED");
            }        
        }

        private void CasualChatterTaskAction_OnIsChatting(bool obj)
        {
            morale += moraleGain * Time.deltaTime;
            morale = Mathf.Clamp01(morale);
        }
    
        private void RepairRaftTaskAction_OnRepairingRaft(bool isRepairingRaft)
        {
            if(!isRepairingRaft)
            {
                if(raftHealth < 1)
                {
                    Color color = raftDamageSprite.color;
                    color.a = 0;
                    raftDamageSprite.color = color;
                }
                
            }
            else
            {
                raftHealth += raftHealthGain * Time.deltaTime;
                raftHealth = Mathf.Clamp01(raftHealth);

                Color color = raftDamageSprite.color;
                color = new Color(1f, 0f, 0f, raftHealth);
                raftDamageSprite.color = color;

                if (raftHealth >= 1)
                {
                    OnRaftRepaired?.Invoke(true);
                    hasRaftLostMovement = false;

                    StartCoroutine(SetDamagedRaftToGreen());
                }
            }
        }
        public void MakeRaftDamageSpriteRed()
        {
            Color color = raftDamageSprite.color;
            color = new Color(1f, 0f, 0f, 0);
            raftDamageSprite.color = color;
        }
        public IEnumerator SetDamagedRaftToGreen()
        {
            Color color = raftDamageSprite.color;
            color = new Color(0f, 1f, 0f, color.a);
            raftDamageSprite.color = color;

            yield return new WaitForSeconds(2);

            // return to original color
            color = raftDamageSprite.color;
            color = new Color(1f, 1f, 1f, raftHealth);
            raftDamageSprite.color = color;
        }

        private void CollectRainTaskAction_OnCollectingRain(bool obj)
        {
            thirst += thirstGain * Time.deltaTime;
            thirst = Mathf.Clamp01(thirst);
        }

        private void FishingTaskAction_OnFishCaught(bool obj)
        {
            hunger += hungerGain;
            hunger = Mathf.Clamp01(hunger);
        }

        /// <summary>
        /// Continously decrease resources, called in GameManager
        /// </summary>
        public void DecreaseResources()
        {
            hunger -= HUNGER_DECREASE_AMOUNT * Time.deltaTime;
            thirst -= THIRST_DECREASE_AMOUNT * Time.deltaTime;
            morale -= MORAL_DECREASE_AMOUNT * Time.deltaTime;

            CheckRaftHealth();
            CheckMorale();
            CheckIfThirstIsLow();
            CheckIfPlayerDied();
        }

        /// <summary>
        /// Called when the raft has taken damage
        /// </summary>
        public void DecreaseRaftHealth()
        {
            raftHealth -= RAFT_HEALTH_DECREASE_AMOUNT * Time.deltaTime;
        }

        #region Thirst resource
        /// <summary>
        /// Can trigger the rainfall again when flags are reset 
        /// </summary>
        private void ResetThirstLowTriggers()
        {
            if(thirst > RESOURCE_MEDIUM_THRESHOLD)
            {
                isThirstResourceMedium = false;
            }
            if(thirst > RESOURCE_LOW_THRESHOLD)
            {
                isThirstResourceLow = false;
            }
        }

        /// <summary>
        /// Trigger OnThirstResourceLow events
        /// </summary>
        private void CheckIfThirstIsLow()
        {
            ResetThirstLowTriggers();

            if(thirst <= RESOURCE_MEDIUM_THRESHOLD && !isThirstResourceMedium)
            {
                isThirstResourceMedium = true;
                OnThirstResourceLow?.Invoke(thirst);
            }
            if(thirst <= RESOURCE_LOW_THRESHOLD && !isThirstResourceLow)
            {
                isThirstResourceLow = true;
                OnThirstResourceLow?.Invoke(thirst);
            }
        }
        #endregion

        /// <summary>
        /// Used for activating either green, yellow or red indicator on resource for the UI
        /// </summary>

        public Image ManageResourceImage(Image[] resourceImages, float resourceValue)
        {
            if(IsResourceLow(resourceValue))
            {
                resourceImages[0].gameObject.SetActive(false);
                resourceImages[1].gameObject.SetActive(false);
                resourceImages[2].gameObject.SetActive(true);
                return resourceImages[2];
            }
            else if(IsResourceMedium(resourceValue))
            {
                resourceImages[0].gameObject.SetActive(false);
                resourceImages[1].gameObject.SetActive(true);
                resourceImages[2].gameObject.SetActive(false);
                return resourceImages[1];
            }
            else
            {
                resourceImages[0].gameObject.SetActive(true);
                resourceImages[1].gameObject.SetActive(false);
                resourceImages[2].gameObject.SetActive(false);
                return resourceImages[0];
            }
                
        }
        
        private bool IsResourceLow(float resourceValue)
        {
            if(resourceValue <= RESOURCE_LOW_THRESHOLD)
            {
                return true;
            }

            return false;
        }
        private bool IsResourceMedium(float resourceValue)
        {
            if (resourceValue <= RESOURCE_MEDIUM_THRESHOLD && resourceValue > RESOURCE_LOW_THRESHOLD)
            {
                return true;
            }

            return false;
        }
    }
}

