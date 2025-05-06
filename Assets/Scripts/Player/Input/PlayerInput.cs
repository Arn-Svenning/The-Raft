using UnityEngine;

namespace Input
{
    /// <summary>
    /// This class stores all of the inputs that the player can do
    /// </summary>
    public class PlayerInput : MonoBehaviour, IInput
    {
        public static PlayerInput Instance { get; private set; }

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

        /// <summary>
        /// Get movement input
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public Vector2 GetMovementInput(Rewired.Player player)
        {
            float moveHorizontal = player.GetAxisRaw("MoveHorizontal");
            float moveVertical = player.GetAxisRaw("MoveVertical");

            return new Vector2(moveHorizontal, moveVertical);
        }

        /// <summary>
        /// Get mouse axis input
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public Vector2 GetMouseInput(Rewired.Player player)
        {
            float mouseHorizontal = player.GetAxisRaw("MouseHorizontal");
            float mouseVertical = player.GetAxisRaw("MouseVertical");

            return new Vector2(mouseHorizontal, mouseVertical);
        }

        /// <summary>
        /// Input for opening chat
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool GetToggleChatInput(Rewired.Player player)
        {
            bool isPressed = false;

            isPressed = player.GetButtonDown("ToggleChat");

            return isPressed;
        }

        public bool GetSendToChatInput(Rewired.Player player)
        {
            bool isPressed = false;

            isPressed = player.GetButtonDown("SendMessage");

            return isPressed;
        }
        public bool GetPerformTaskInput(Rewired.Player player)
        {
            bool isPressed = false;

            isPressed = player.GetButtonDown("PerformTask");

            return isPressed;
        }
        public bool GetDialogueInput(Rewired.Player player)
        {
            bool isPressed = false;

            isPressed = player.GetButtonDown("Dialogue");

            return isPressed;
        }
    }
}

