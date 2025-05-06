using UnityEngine;
using Ink.Runtime;
using NPC;


public class NPCDeadBehavior : NPCBase
{
    [SerializeField] private DialogueManager dialogueManager;
    private DetermineNPCState determineNPCState;

    private const string npcTaskVariableName = "dead_npc_task";
    private string lastTask = null;

    private void Start()
    {
        base.Start();
        determineNPCState = new DetermineNPCState(this, npcStateMachine);
    }

    private void LateUpdate()
    {
        if (dialogueManager == null || dialogueManager.dialogueIsPlaying)
            return;

        var taskObj = dialogueManager.GetVariableState(npcTaskVariableName);
        if (taskObj != null)
        {
            var task = taskObj.ToString().Trim('\"');
            if (task != lastTask)
            {
                lastTask = task;
                determineNPCState.DetermineStateScripted(task);
            }
        }
    }
}
