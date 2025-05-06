using UnityEngine;

namespace NPC
{
    public class NPCCollectingRainState : NPCState
    {
        public NPCCollectingRainState(NPCBase NPCBase, NPCStateMachine NPCStateMachine) : base(NPCBase, NPCStateMachine)
        {
        }

        public override string DebugEnter()
        {
            base.DebugEnter();
            return "entered Collecting Rain state";
        }
        public override void EnterState()
        {
            base.EnterState();
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        public override void FrameUpdate(string playerMessage)
        {
            base.FrameUpdate(playerMessage);
            NPCBase.MoveNPC(Vector2.zero);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}

