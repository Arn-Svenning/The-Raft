using Chat;
using Ink.Parsed;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// This class handles the player typing to the chat box
    /// </summary>
    public class PlayerChatBoxInteract : MonoBehaviour
    {
        [SerializeField] private GameObject playerSpeechBubble;
        [SerializeField] private TextMeshPro playerSpeechBubbleText;

        [SerializeField] private TMP_InputField playerInputFieldRobert;
        [SerializeField] private TMP_InputField playerInputFieldBob;
        [SerializeField] private TMP_InputField playerInputFieldBoth;

        private const float SPEECH_BUBBLE_DURATION = 2;

        [Header("Scene Settings")]
        [Tooltip("If unchecked, all chat checks are disabled in this scene")]
        public bool enabledInThisScene = true;

        private void Start()
        {
            
        }

        private void Update()
        {
            //if(playerInputFieldRobert == null)
            //{
            //    playerInputField = FindAnyObjectByType<TMP_InputField>();
            //}
        }

        public void SendMessageToChat()
        {
            if (!enabledInThisScene)
                return;

            if (playerInputFieldRobert == null || playerInputFieldBob == null || playerInputFieldBoth == null)
            {
                Debug.LogError("Player Input Field is null");
                return;
            }

            if (!BIsUsingChat()) return;


            TMP_InputField[] inputFields = {playerInputFieldRobert, playerInputFieldBob, playerInputFieldBoth};
            foreach(var inputField in inputFields)
            {
                if(inputField.gameObject.activeSelf)
                {
                    ChatManager.Instance.SendMessageToChat(PlayerCharacter.playerName, inputField.text);
                    inputField.text = "";
                    return;
                }
            }
        }
        public bool BIsUsingChat()
        {

            if (!enabledInThisScene)
                return false;

            TMP_InputField[] inputFields = { playerInputFieldRobert, playerInputFieldBob, playerInputFieldBoth };
            foreach (var inputField in inputFields)
            {
                if(inputField.gameObject.activeSelf && inputField.text != "")
                {
                    return true;
                }
            }
            return false;

        }
        //public void ShowPlayerSpeechBubble()
        //{       
        //    playerSpeechBubble.SetActive(true);
        //    StartCoroutine(TypeSentence(playerInputField.text, playerSpeechBubbleText));
        //}
        //private void StopTyping()
        //{
        //    StartCoroutine(DisableSpeechBubble());
        //}
        //private IEnumerator DisableSpeechBubble()
        //{
        //    yield return new WaitForSeconds(SPEECH_BUBBLE_DURATION);

        //    playerSpeechBubble.gameObject.SetActive(false);
        //}

        //private IEnumerator TypeSentence(string sentence, TextMeshPro dialogueText)
        //{
        //    dialogueText.text = "";

        //    foreach (char letter in sentence.ToCharArray())
        //    {
        //        dialogueText.text += letter;
        //        yield return null;
        //    }

        //    StopTyping();
        //}

    }
}

