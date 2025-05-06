using Input;
using NUnit.Framework;
using System;
using TaskAction;
using UnityEngine;

namespace NPC
{
    public class NPCBase : MonoBehaviour, INPCMoveable
    {
        private NPCInteraction NPCInteraction;
        protected PerformTaskAction performTaskAction;

        #region NPC State machine
        public NPCStateMachine npcStateMachine { get; private set; }
        public NPCState currentState { get; private set; }

        #endregion
        public Rigidbody2D NPCRigidBody { get; set; }

        [SerializeField] private Transform[] taskPositions;
        public Transform currentTaskPosition;
        public Transform[] TasPositions { get { return taskPositions; } }
        public bool isFacingRight { get; set; } = true;

        private event Action<NPCState> OnChangedState;

        private CharacterAnimationController characterAnimationController;

        private void Awake()
        {
            npcStateMachine = new NPCStateMachine();
        }
        protected void Start()
        {
            NPCRigidBody = GetComponent<Rigidbody2D>();
            if(GameManager.Instance.GetIsLLMVersion)
            {
                NPCInteraction = GetComponent<NPCInteraction>();

                if (NPCInteraction == null)
                    Debug.LogError("NPCBase: Missing NPCInteraction!", this);
            }
           
            performTaskAction = GetComponentInChildren<PerformTaskAction>();
            characterAnimationController = GetComponentInChildren<CharacterAnimationController>();


            if (performTaskAction == null)
                Debug.LogError($"[NPCBase] performTaskAction is NULL on {name}", this);

            OnChangedState += NPCBase_OnChangedState;

            if (NPCRigidBody == null)
                Debug.LogError("NPCBase: Missing Rigidbody2D!", this);
           
            if (performTaskAction == null)
                Debug.LogError("NPCBase: Missing PerformTaskAction child!", this);

        }
        private void OnDestroy()
        {
            OnChangedState -= NPCBase_OnChangedState;
        }


        private void Update()
        {
            // 1) Guard against missing state machine or state
            if (npcStateMachine == null || npcStateMachine.currentNPCState == null)
                return;

            // 2) Drive movement for any non‑idle states, if you centralized movement here:
            var s = npcStateMachine.currentNPCState;
            if (!(s is NPCIdle) && !(s is NPCCasualConversationState))
                MoveNPC(Vector2.zero);

            // 3) Instead of NPCInteraction.playerMessage, just pass null (or "")
            s.FrameUpdate(null);
        }

        private void FixedUpdate()
        {
            if (npcStateMachine == null || npcStateMachine.currentNPCState == null)
                return;

            npcStateMachine.currentNPCState.PhysicsUpdate();
        }

        public void CheckForFacingRightOrLeft(Vector2 velocity)
        {
            if (velocity.x < 0f && isFacingRight)
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                isFacingRight = false;
            }
            else if (velocity.x > 0f && !isFacingRight)
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                isFacingRight = true;
            }
        }

        public void MoveNPC(Vector2 velocity)
        {
            if (!IsNPCAtTaskPosition())
            {
                Vector2 moveDirection = ((Vector2)(currentTaskPosition.position - transform.position)).normalized;

                moveDirection = new Vector2(
                    Mathf.Round(moveDirection.x),
                    Mathf.Round(moveDirection.y)
                );

                transform.position = Vector2.MoveTowards(transform.position, currentTaskPosition.position, 5 * Time.deltaTime);

                if (characterAnimationController != null)
                {
                    characterAnimationController.PlayAnimation(moveDirection);
                }
            }
            else
            {
                PerformTask();

                if (characterAnimationController != null)
                {
                    characterAnimationController.PlayAnimation(Vector2.zero); // Not moving
                }
            }
        }


        private bool IsNPCAtTaskPosition()
        {
            return Vector2.Distance(transform.position, currentTaskPosition.position) < 0.1f;
        }

        /// <summary>
        /// Used for LLM version of game
        /// </summary>
        /// <param name="stateName"></param>
        /// <param name="newState"></param>
        public void SwitchToState(string stateName, NPCState newState, NPCStates newStateName)
        {
            npcStateMachine.ChangeState(newState, newStateName);

            if(newState is not NPCIdle && newState is not NPCCasualConversationState)
            {
                SetCurrentTaskPosition(stateName);
            }
               
            OnChangedState?.Invoke(newState);
        }

        public bool GetSethasExecutedTask { get { return hasExecutedTask; } set { hasExecutedTask = value; } }
        private bool hasExecutedTask = false;
        private void PerformTask()
        {
            if (hasExecutedTask) return;

            performTaskAction.ExecuteTask(ref hasExecutedTask);
            
            Debug.Log($"PerformTask() invoked. isInRange={currentTaskPosition}, currentTaskAction={(performTaskAction == null ? "null" : performTaskAction.ToString())}");
        }

        private void SetCurrentTaskPosition(string stateName)
        {
           
            stateName = stateName.Trim();
            stateName = stateName.Replace(".", "");

            foreach (Transform position in taskPositions)
            {
                Debug.Log($"Checking {position.gameObject.name} - Expected Tag: {stateName}, Actual Tag: {position.gameObject.tag}");

                if (position.gameObject.CompareTag(stateName))
                {
                    Debug.Log($"Match found: {position.gameObject.name}");
                    currentTaskPosition = position;
                    return;
                }
            }

            Debug.LogError($"No match found for tag: {stateName}");
        }

        private void NPCBase_OnChangedState(NPCState newsState)
        {
            performTaskAction.isDoingTask = false;
            hasExecutedTask = false;
        }
    }
}

