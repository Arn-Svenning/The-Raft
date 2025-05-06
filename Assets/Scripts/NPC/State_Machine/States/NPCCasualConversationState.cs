using System;
using UnityEngine;

namespace NPC
{
    public class NPCCasualConversationState : NPCState
    {
        public static event Action<bool> OnIsChatting;

        public event Action<bool> OnConversationStopped;
        private float CONVERSATION_IDLE_DURATION = 10;
        private float conversationIdleTimer;

        public NPCCasualConversationState(NPCBase NPCBase, NPCStateMachine NPCStateMachine) : base(NPCBase, NPCStateMachine)
        {
        }
        public override string DebugEnter()
        {
            base.DebugEnter();
            return "entered Casual chatter State";
        }
        public override void EnterState()
        {
            base.EnterState();
            conversationIdleTimer = CONVERSATION_IDLE_DURATION;
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        public override void FrameUpdate(string playerMessage)
        {
            base.FrameUpdate(playerMessage);

            OnIsChatting?.Invoke(true);

            conversationIdleTimer -= Time.deltaTime;

            if(conversationIdleTimer <= 0)
            {
                OnConversationStopped?.Invoke(true);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}

