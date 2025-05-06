using NPC;
using RaftObject;
using System;
using UnityEngine;

namespace TaskAction
{
    public class PerformTaskAction : MonoBehaviour
    {
        private ITaskAction currentTask;

        #region Tasks

        private PaddleRaftTaskAction paddleTaskAction;
        private FishingTaskAction fishingTaskAction;
        private CollectRainTaskAction collectRainTaskAction;
        private RepairRaftTaskAction repairRaftTaskAction;
        private CasualChatterTaskAction casualChatterTaskAction;

        private ITaskAction[] taskActions;

        #endregion

        private Collider2D mainTriggerCollider;

        private bool isInRange = false;
        public bool isDoingTask { get; set; } = false;

        private void Start()
        {
            Raft raft = FindAnyObjectByType<Raft>();

            paddleTaskAction = new PaddleRaftTaskAction(raft);
            fishingTaskAction = new FishingTaskAction();
            collectRainTaskAction = new CollectRainTaskAction();
            repairRaftTaskAction = new RepairRaftTaskAction();
            casualChatterTaskAction = new CasualChatterTaskAction();

            taskActions = new ITaskAction[] { paddleTaskAction, fishingTaskAction, collectRainTaskAction, repairRaftTaskAction, casualChatterTaskAction };

            mainTriggerCollider = GetComponent<Collider2D>();
        }
        private void OnDestroy()
        {
            paddleTaskAction.Cleanup();
        }
        private void Update()
        {
            if (!isDoingTask) return;

            currentTask?.ExecuteTask();

        }
        public void ExecuteTask()
        {
            if (isInRange)
            {
                if(!isDoingTask)
                {
                    isDoingTask = true;
                }
                else
                {
                    isDoingTask = false;
                    StopCurrentTask();
                }
                
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (mainTriggerCollider != null && !mainTriggerCollider.IsTouching(collision)) return;

            if (collision.CompareTag(NPCStateNames.PADDLING_STATE))
            {
                isInRange = true;
                ChangeTaskAction(paddleTaskAction);
            }
            else if(collision.CompareTag(NPCStateNames.FISHING_STATE))
            {
                isInRange = true;
                ChangeTaskAction(fishingTaskAction);
            }
            else if (collision.CompareTag(NPCStateNames.COLLECT_RAIN_STATE))
            {
                isInRange = true;
                ChangeTaskAction(collectRainTaskAction);
            }
            else if (collision.CompareTag(NPCStateNames.REPAIR_RAFT_STATE))
            {
                isInRange = true;
                ChangeTaskAction(repairRaftTaskAction);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(NPCStateNames.PADDLING_STATE))
            {
                isInRange = false;
                isDoingTask = false;
                StopCurrentTask();
            }
            else if (collision.CompareTag(NPCStateNames.FISHING_STATE))
            {
                isInRange = false;
                isDoingTask = false;
                StopCurrentTask();
            }
            else if (collision.CompareTag(NPCStateNames.COLLECT_RAIN_STATE))
            {
                isInRange = false;
                isDoingTask = false;
                StopCurrentTask();
            }
            else if (collision.CompareTag(NPCStateNames.REPAIR_RAFT_STATE))
            {
                isInRange = false;
                isDoingTask = false;
                StopCurrentTask();
            }
        }
        private void ChangeTaskAction(ITaskAction task)
        {
            if (currentTask != task)
            {
                StopCurrentTask();
                currentTask = task;
            }
        }
        private void StopCurrentTask()
        {
            foreach (ITaskAction taskAction in taskActions)
            {
                if (currentTask == taskAction)
                {
                    taskAction.StopTask();
                }

            }

        }
    }
}

