using NPC;
using TaskAction;
using UnityEngine;


public class NormalNPCBehavior : NPCBase
{
    [SerializeField] private DialogueManager dialogueManager;
    private DetermineNPCState determineNPCState;
    private const string npcTaskVariableName = "normal_npc_task";

    private string lastTask = null;

    private void Start()
    {
        base.Start();
        determineNPCState = new DetermineNPCState(this, npcStateMachine);
    }

    private void OnDestroy()
    {
        determineNPCState.Cleanup();
    }
    private void LateUpdate()
    {
        if (dialogueManager == null || dialogueManager.dialogueIsPlaying)
            return;

        var taskObj = dialogueManager.GetVariableState(npcTaskVariableName);
        if (taskObj != null)
        {
            var task = taskObj.ToString().Trim('"');

            if (task != lastTask)
            {
                lastTask = task;
                determineNPCState.DetermineStateScripted(task);
            }
        }
    }
}

