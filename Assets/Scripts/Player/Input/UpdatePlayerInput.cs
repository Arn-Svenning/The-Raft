using Chat;
using Player;
using UnityEngine;

namespace Input
{
    /// <summary>
    /// This script stores the methods that update player input which are called in the player class
    /// </summary>
    public class UpdatePlayerInput : MonoBehaviour
    {
        public Vector2 movementInput { get; private set; } = Vector2.zero;
        public bool performTask { get; private set; } = false;

        private bool useChat = false;

        public bool dialogueInput { get; private set; } = false;

        public void UpdateInput(PlayerChatBoxInteract playeChatBox, Rewired.Player playerInput)
        {
            MovementInput(playeChatBox, playerInput);
            UseChatInput(playeChatBox, playerInput);
            PerformTaskInput(playeChatBox, playerInput);
            DialogueInput(playeChatBox, playerInput);

        }

        /// <summary>
        /// Movement input 
        /// </summary>
        /// <param name="playeChatBox"></param>
        /// <param name="playerMovement"></param>
        /// <param name="playerInput"></param>
        private void MovementInput(PlayerChatBoxInteract playeChatBox, Rewired.Player playerInput)
        {
            if (playeChatBox != null && playeChatBox.BIsUsingChat())
            {
                movementInput = Vector2.zero;
                return;
            }

            movementInput = PlayerInput.Instance.GetMovementInput(playerInput);
            
        }

        /// <summary>
        /// Chatbox input
        /// </summary>
        /// <param name="playeChatBox"></param>
        /// <param name="playerInput"></param>
        private void UseChatInput(PlayerChatBoxInteract playeChatBox, Rewired.Player playerInput)
        {
            useChat = PlayerInput.Instance.GetSendToChatInput(playerInput);

            if (useChat && playeChatBox != null && ChatManager.Instance != null)
            {
                playeChatBox.SendMessageToChat();
            }
        }

        /// <summary>
        /// Input for interaction with the tasks on the raft 
        /// </summary>
        /// <param name="playeChatBox"></param>
        /// <param name="playerInput"></param>
        private void PerformTaskInput(PlayerChatBoxInteract playeChatBox, Rewired.Player playerInput)
        {
            if (playeChatBox != null && playeChatBox.BIsUsingChat())
            {
                performTask = false;
                return;
            }

            performTask = PlayerInput.Instance.GetPerformTaskInput(playerInput);
        }
        private void DialogueInput(PlayerChatBoxInteract playeChatBox, Rewired.Player playerInput)
        {
            // Don’t trigger dialogue while typing in chat:
            if (playeChatBox != null && playeChatBox.BIsUsingChat())
            {
                dialogueInput = false;
                return;
            }
            dialogueInput = PlayerInput.Instance.GetDialogueInput(playerInput);
        }
    }
}

