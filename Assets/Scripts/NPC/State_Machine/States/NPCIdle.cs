using UnityEngine;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;

namespace NPC
{
    public class NPCIdle : NPCState
    {
        public NPCIdle(NPCBase NPCBase, NPCStateMachine NPCStateMachine) : base(NPCBase, NPCStateMachine)
        {

        }
        public override string DebugEnter()
        {
            base.DebugEnter();
            return "entered Idle State";
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
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}

