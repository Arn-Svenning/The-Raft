using Chat;
using LLMUnity;
using Player;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NPC
{

    /// <summary>
    /// This class handles the spoken interaction between the player and the NPC
    /// </summary>
    public class NPCInteraction : MonoBehaviour
    {
        [SerializeField] private NPCDataLoader npcDataLoader;
        [SerializeField] private TextMeshProUGUI AIText; // A UI element for displaying NPC responses
        [SerializeField] private GameObject AIChatBubble;
        [SerializeField] private Image NPCChatBubbleImage;
        [SerializeField] private GameObject currentlySpeakingBubble;

        public LLMRAGCharacter llmCharacter;
        public TMP_InputField playerText;
        public TMP_InputField playerTextBoth;
        private NPCBase npcBase;
        private DetermineNPCState determineNPCState;
        
        private DetectPlayer detectPlayer;
        public string playerMessage { get; set; }

        private bool isCharacterInitialized = false;

        bool isCurrentlyTalkingWithPlayer = false;

        private void Start()
        {
            playerText.onSubmit.AddListener(onInputFieldSubmit);
            playerTextBoth.onSubmit.AddListener(onInputFieldSubmit);

            detectPlayer = GetComponent<DetectPlayer>();
            npcBase = GetComponent<NPCBase>();
            determineNPCState = new DetermineNPCState(llmCharacter, npcDataLoader, npcBase, npcBase.npcStateMachine);
            DecreaseNPCAndChatBubbleVisibility();
            WarmupModel();
        }
        private void OnDestroy()
        {
            determineNPCState.Cleanup();
        }
        void WarmupCompleted()
        {
            isCharacterInitialized = true;
            LoadingLLMScene.Instance.LLMCharactersLoaded++;
        }
        private void  WarmupModel()
        {
            _ = llmCharacter.Warmup(WarmupCompleted);
        }

        void onInputFieldSubmit(string message)
        {
            if (!detectPlayer.isPlayerInRange || !isCharacterInitialized) return;

            CancelRequests();

            isCurrentlyTalkingWithPlayer = true;

            playerMessage = message;

            if (llmCharacter.playerName != PlayerCharacter.playerName)
            {
                llmCharacter.playerName = PlayerCharacter.playerName;
            }

            IncreaseNPCAndChatBubbleVisibility();
            AIText.text = "...";

            _ = llmCharacter.ChatWithRAG(npcDataLoader, determineNPCState ,message, SetAIText, AIReplyComplete, true);

        }

        public void RespondToWorldEvent(string context)
        {
            if(!isCharacterInitialized || isCurrentlyTalkingWithPlayer) return;

            string message = $"[World Event]: {context}";

            IncreaseNPCAndChatBubbleVisibility();

            AIText.text = "...";

            _ = llmCharacter.ChatWithRAG(npcDataLoader, determineNPCState, message, SetAIText, AIReplyComplete, true);
        }

        public void SetAIText(string text)
        {
            AIText.text = text;    
        }

        public void AIReplyComplete()
        {
            playerText.interactable = true;

            playerTextBoth.interactable = true;

            StartCoroutine(CloseChatBubble());
        }

        public void AIReplyCompleteWorldEvent()
        {
            AIText.text = "";
        }

        private IEnumerator CloseChatBubble()
        {
            yield return new WaitForSeconds(3);
            
            DecreaseNPCAndChatBubbleVisibility();

            isCurrentlyTalkingWithPlayer = false;

            if (ChatManager.Instance != null)
            {
                ChatManager.Instance.SendMessageToChat(llmCharacter.name, AIText.text);
            }

            AIText.text = "";
        }
        public void CancelRequests()
        {
            llmCharacter.CancelRequests();
            AIReplyCompleteWorldEvent();
        }

        public void ExitGame()
        {
            Debug.Log("Exit button clicked");
            Application.Quit();
        }

        /// <summary>
        /// decrease visibility of NPC chat bubble to indicate that they are not talking
        /// </summary>
        private void DecreaseNPCAndChatBubbleVisibility()
        {
            if(currentlySpeakingBubble != null)
            {
                currentlySpeakingBubble.SetActive(false);
            }
           
            detectPlayer.ShowPlayerCloseSprite();

            Color newColor = NPCChatBubbleImage.color;
            newColor.a = 0.25f;
            NPCChatBubbleImage.color = newColor;
        }
        /// <summary>
        /// Increase visibility of NPC chat bubble to indicate that they are talking
        /// </summary>
        private void IncreaseNPCAndChatBubbleVisibility()
        {
            if (currentlySpeakingBubble != null)
            {
                currentlySpeakingBubble.SetActive(true);
            }
            
            detectPlayer.HidePlayerCloseSprite();

            Color newColor = NPCChatBubbleImage.color;
            newColor.a = 1.0f;
            NPCChatBubbleImage.color = newColor;
        }
    }
}

