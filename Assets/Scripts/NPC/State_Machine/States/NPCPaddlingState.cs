using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPCPaddlingState : NPCState
    {
        public NPCPaddlingState(NPCBase NPCBase, NPCStateMachine NPCStateMachine) : base(NPCBase, NPCStateMachine)
        {

        }

        public override string DebugEnter()
        {
            base.DebugEnter();
            return "entered Paddling State";
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

