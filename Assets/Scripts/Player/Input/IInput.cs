using UnityEngine;

namespace Input
{
    /// <summary>
    /// This is an input interface, all classes that gets input should inherit from this
    /// </summary>

    public interface IInput
    {
        public interface IInputInterface
        {
            public Vector2 GetMovementInput(Rewired.Player player);
            public Vector2 GetMouseInput(Rewired.Player player);
            public bool GetToggleChatInput(Rewired.Player player);
            public bool GetSendToChatInput(Rewired.Player player);
            public bool GetPerformTaskInput(Rewired.Player player);
        }
    }
}

