using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Chat
{
    /// <summary>
    /// This class handles the chatbox
    /// </summary>
    public class ChatManager : MonoBehaviour
    {
        public static ChatManager Instance { get; private set; } 

        [SerializeField] private GameObject chatText, chatContent;

        private Queue<Message> messageQueue = new Queue<Message>();

        private const int MAX_MESSAGE_COUNT = 25;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(Instance);
            }
        }

        public void SendMessageToChat(string sender, string text)
        {
            if(messageQueue.Count >= MAX_MESSAGE_COUNT)
            {
                Message messageObject = messageQueue.Dequeue();
                Destroy(messageObject.textObject.gameObject);
            }

            Message newMessage = new Message();
            newMessage.text = text;

            GameObject newText = Instantiate(chatText, chatContent.transform);
            newMessage.textObject = newText.GetComponent<TextMeshProUGUI>();
            newMessage.textObject.text = $"<color=yellow>{sender}</color>: {newMessage.text}";

            messageQueue.Enqueue(newMessage);
        }
    }
}

