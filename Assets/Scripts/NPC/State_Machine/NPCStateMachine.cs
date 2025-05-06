
namespace NPC
{
    public class NPCStateMachine
    {
        public NPCState currentNPCState { get; set; }
        public NPCStates currentNPCStateName { get; private set; }

        public bool hasChangedState = false;
        public void Init(NPCState startingState)
        {
            currentNPCState = startingState;
            currentNPCState.EnterState();
        }
        public void ChangeState(NPCState newState, NPCStates newStateName)
        {
            currentNPCState.ExitState();
            currentNPCState = newState;
            currentNPCState.EnterState();
            hasChangedState = true;

            currentNPCStateName = newStateName; // Format for llm npc to interpret what it's currently doing
        }
    }
}

