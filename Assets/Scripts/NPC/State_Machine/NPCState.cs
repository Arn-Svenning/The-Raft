

using JetBrains.Annotations;
using System.Threading.Tasks;

namespace NPC
{
    public class NPCState
    {
        protected NPCBase NPCBase;
        protected NPCStateMachine NPCStateMachine;
        public NPCState(NPCBase NPCBase, NPCStateMachine NPCStateMachine)
        {
            this.NPCBase = NPCBase;
            this.NPCStateMachine = NPCStateMachine;
        }
        public virtual string DebugEnter() 
        {
            NPCStateMachine.hasChangedState = false;
            return ""; 
        }
        public virtual void EnterState() 
        {
            NPCStateMachine.hasChangedState = true;
            NPCBase.GetSethasExecutedTask = false;
        }
        public virtual void ExitState() { }
        public virtual void FrameUpdate(string playerMessage)
        {
           
            
        }
        public virtual void PhysicsUpdate() { }
    }
}

