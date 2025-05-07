using Chat;
using LLMUnity;
using Player;
using PlayerUI;
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
        [SerializeField] private TextMeshProUGUI AIText; // A UI element for displaying NPC responses
        [SerializeField] private GameObject AIChatBubble;
        [SerializeField] private GameObject characterImage;
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
            determineNPCState = new DetermineNPCState(llmCharacter, npcBase, npcBase.npcStateMachine);
            DecreaseNPCAndChatBubbleVisibility();
            WarmupModel();

            detectPlayer.OnPlayerExitedInteractRange += DetectPlayer_OnPlayerExitedInteractRange;
        }

        private void DetectPlayer_OnPlayerExitedInteractRange(Button currentActiveButton)
        {
            UIManager.Instance.InactivateTalkToNPCText();
        }

        private void OnDestroy()
        {
            determineNPCState.Cleanup();
            detectPlayer.OnPlayerExitedInteractRange -= DetectPlayer_OnPlayerExitedInteractRange;
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

        async void onInputFieldSubmit(string message)
        {
            if (!detectPlayer.isPlayerInRange || !isCharacterInitialized) return;

            CancelRequests();

            isCurrentlyTalkingWithPlayer = true;

            playerMessage = message;

            if (llmCharacter.playerName != PlayerCharacter.playerName)
            {
                llmCharacter.playerName = PlayerCharacter.playerName;
            }

            IncreaseNPCAndChatBubbleVisibility(false);
            AIText.text = "...";

            var npcResponse = await llmCharacter.ChatWithRAG(determineNPCState, message, SetAIText, AIReplyComplete, true);


            SessionLogger.Instance.Log(message, npcResponse, llmCharacter.AIName);
        }

        public void RespondToWorldEvent(string context)
        {
            if(!isCharacterInitialized || isCurrentlyTalkingWithPlayer) return;

            string message = $"[World Event]: {context}";

            IncreaseNPCAndChatBubbleVisibility(true);

            AIText.text = "...";

            _ = llmCharacter.ChatWithRAG(determineNPCState, message, SetAIText, AIReplyComplete, true);
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

            Color newColor = characterImage.transform.GetChild(0).GetComponent<Image>().color;
            newColor.a = 0.25f;
            ChangeChatBubbleImageVisibility(newColor);
        }
        /// <summary>
        /// Increase visibility of NPC chat bubble to indicate that they are talking
        /// </summary>
        private void IncreaseNPCAndChatBubbleVisibility(bool isRespondingToWorldEvent)
        {
            if (currentlySpeakingBubble != null)
            {
                currentlySpeakingBubble.SetActive(true);
            }

            detectPlayer.HidePlayerCloseSprite(isRespondingToWorldEvent);

            Color newColor = characterImage.transform.GetChild(0).GetComponent<Image>().color;
            newColor.a = 1.0f;
            ChangeChatBubbleImageVisibility(newColor);
        }
        private void ChangeChatBubbleImageVisibility(Color newColor)
        {
            for (int i = 0; i < characterImage.transform.childCount; i++)
            {
                Transform child = characterImage.transform.GetChild(i);
                Image image = child.GetComponent<Image>();
                if (image != null)
                {
                    image.color = newColor;
                }
            }
        }
    }
}

