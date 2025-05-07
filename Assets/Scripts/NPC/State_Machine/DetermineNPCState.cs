using LLMUnity;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace NPC
{
    public class DetermineNPCState
    {
        private Timer timer;

        private NPCBase npcBase;

        public NPCStateMachine npcStateMachine { get; private set; }
        private NPCFishingState NPCFishingState;
        private NPCPaddlingState NPCPaddlingState;
        private NPCCollectingRainState NPCCollectingRainState;
        private NPCRepairRaftState NPCRepairRaftState;
        private NPCCasualConversationState NPCCasualConversationState;
        private NPCIdle NPCIdleState;

        /// <summary>
        /// Used for LLM version of the game
        /// </summary>
        /// <param name="llmCharacter"></param>
        /// <param name="npcDataLoader"></param>
        /// <param name="npcBase"></param>
        /// <param name="npcStateMachine"></param>
        public DetermineNPCState(LLMCharacter llmCharacter, NPCBase npcBase, NPCStateMachine npcStateMachine)
        {
            this.npcBase = npcBase;
            this.npcStateMachine = npcStateMachine;

            NPCFishingState = new NPCFishingState(npcBase, npcStateMachine);
            NPCIdleState = new NPCIdle(npcBase, npcStateMachine);
            NPCPaddlingState = new NPCPaddlingState(npcBase, npcStateMachine);
            NPCCollectingRainState = new NPCCollectingRainState(npcBase, npcStateMachine);
            NPCRepairRaftState = new NPCRepairRaftState(npcBase, npcStateMachine);
            NPCCasualConversationState = new NPCCasualConversationState(npcBase, npcStateMachine);

            npcStateMachine.Init(NPCIdleState);

            NPCCasualConversationState.OnConversationStopped += NPCCasualConversationState_OnConversationStopped;
        }
        
        /// <summary>
        /// Used for scripted version of the game
        /// </summary>
        /// <param name="npcBase"></param>
        /// <param name="npcStateMachine"></param>
        public DetermineNPCState(NPCBase npcBase, NPCStateMachine npcStateMachine)
        {
            this.npcBase = npcBase;
            this.npcStateMachine = npcStateMachine;

            NPCFishingState = new NPCFishingState(npcBase, npcStateMachine);
            NPCIdleState = new NPCIdle(npcBase, npcStateMachine);
            NPCPaddlingState = new NPCPaddlingState(npcBase, npcStateMachine);
            NPCCollectingRainState = new NPCCollectingRainState(npcBase, npcStateMachine);
            NPCRepairRaftState = new NPCRepairRaftState(npcBase, npcStateMachine);
            NPCCasualConversationState = new NPCCasualConversationState(npcBase, npcStateMachine);

            npcStateMachine.Init(NPCIdleState);

            NPCCasualConversationState.OnConversationStopped += NPCCasualConversationState_OnConversationStopped;
        }

        public void Cleanup()
        {
            NPCCasualConversationState.OnConversationStopped -= NPCCasualConversationState_OnConversationStopped;
        }


        private void NPCCasualConversationState_OnConversationStopped(bool obj)
        {
            npcBase.SwitchToState("Idle", NPCIdleState, NPCStates.Idle);
        }

        public void StartStateDetermination(string detectedState, NPCState newState, NPCStates newStateName)
        {
            if (!string.IsNullOrEmpty(detectedState))
            {
                npcBase.SwitchToState(detectedState, newState, newStateName);
            }
        }

        /// <summary>
        /// Determine the state that the NPC should be in. Used for LLM version of the game
        /// </summary>
        /// <param name="response"></param>
        /// <param name="state"></param>
        public void DetermineState(string response)
        {
            // Extract any text between square brackets: [Fishing], [Paddling], etc.
            Match match = Regex.Match(response, @"\[(.*?)\]");

            if (match.Success)
            {
                string extractedState = match.Groups[1].Value.Trim();

                switch (extractedState)
                {
                    case "Fishing":
                        StartStateDetermination(NPCStateNames.FISHING_STATE, NPCFishingState, NPCStates.Fishing);
                        break;
                    case "Paddling":
                        StartStateDetermination(NPCStateNames.PADDLING_STATE, NPCPaddlingState, NPCStates.Paddle);
                        break;
                    case "Idle":
                        StartStateDetermination(NPCStateNames.IDLE_STATE, NPCIdleState, NPCStates.Idle);
                        break;
                    case "Collect Rain":
                        StartStateDetermination(NPCStateNames.COLLECT_RAIN_STATE, NPCCollectingRainState, NPCStates.CollectRain);
                        break;
                    case "Repair Raft":
                        StartStateDetermination(NPCStateNames.REPAIR_RAFT_STATE, NPCRepairRaftState, NPCStates.RepairRaft);
                        break;
                    case "Casual Chatter":
                        StartStateDetermination(NPCStateNames.CASUAL_CHATTER_STATE, NPCCasualConversationState, NPCStates.CasualChatter);
                        break;
                    default:
                        Debug.LogWarning($"Unknown NPC state: {extractedState}");
                        StartStateDetermination(NPCStateNames.IDLE_STATE, NPCIdleState, NPCStates.Idle);
                        break;
                }
            }
            else
            {
                Debug.LogError("Failed to extract state from response.");
                StartStateDetermination(NPCStateNames.IDLE_STATE, NPCIdleState, NPCStates.Idle);
            }
        }


        /// <summary>
        /// Used for scripted version of game
        /// </summary>
        /// <param name="stateName"></param>
        public void DetermineStateScripted(string stateName)
        {
            switch (stateName)
            {
                case "Fishing":
                    StartStateDetermination(NPCStateNames.FISHING_STATE, NPCFishingState, NPCStates.Fishing);
                    break;
                case "Paddling":
                    StartStateDetermination(NPCStateNames.PADDLING_STATE, NPCPaddlingState, NPCStates.Paddle);
                    break;
                case "Idle":
                    StartStateDetermination(NPCStateNames.IDLE_STATE, NPCIdleState, NPCStates.Idle);
                    break;
                case "Collect Rain":
                    StartStateDetermination(NPCStateNames.COLLECT_RAIN_STATE, NPCCollectingRainState, NPCStates.CollectRain);
                    break;
                case "Repair Raft":
                    StartStateDetermination(NPCStateNames.REPAIR_RAFT_STATE, NPCRepairRaftState, NPCStates.RepairRaft);
                    break;
                case "Casual Chatter":
                    StartStateDetermination(NPCStateNames.CASUAL_CHATTER_STATE, NPCCasualConversationState, NPCStates.CasualChatter);
                    break;
                default:
                    Debug.LogWarning($"Unknown NPC state: {stateName}");
                    StartStateDetermination(NPCStateNames.IDLE_STATE, NPCIdleState, NPCStates.Idle);
                    break;
            }
        }
    }
}

