using UnityEngine;
using Input;
using Rewired;
using TaskAction;

namespace Player
{
    /// <summary>
    /// This class stores all the functionalites of the player such as movement
    /// </summary>

    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerChatBoxInteract))]
    [RequireComponent(typeof(UpdatePlayerInput))]
    [RequireComponent(typeof (PerformTaskAction))]
    public class PlayerCharacter : MonoBehaviour
    {
        public static string playerName;

        #region PlayerComponents

        [SerializeField] private Rewired.Player playerInput;

        private PlayerMovement playerMovement;
        private PlayerChatBoxInteract playerChatBoxInteract;
        private UpdatePlayerInput updatePlayerInput;
        private PerformTaskAction performTaskAction;
        private Rigidbody2D playerRigidBody;
        private CharacterAnimationController characterAnimationController;
        #endregion

        bool isFacingRight = false;

        private void Awake()
        {
            if(SetPlayerName.Instance != null)
            {
                playerName = SetPlayerName.Instance.playerName;
            }
           
        }
        void Start()
        {
            playerInput = ReInput.players.GetPlayer(0);

            #region GetComponents

            playerRigidBody = GetComponent<Rigidbody2D>();
            playerMovement = GetComponent<PlayerMovement>();
            playerChatBoxInteract = GetComponent<PlayerChatBoxInteract>();
            updatePlayerInput = GetComponent<UpdatePlayerInput>();
            performTaskAction = GetComponent<PerformTaskAction>();
            characterAnimationController = GetComponentInChildren<CharacterAnimationController>();
            #endregion
        }

        void Update()
        {
            updatePlayerInput.UpdateInput(playerChatBoxInteract, playerInput);
            
            MovePlayer();
            PerformTask();
            NPCInteract();
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
        private void MovePlayer()
        {
            if (DialogueTrigger.currentActiveTrigger != null && DialogueTrigger.currentActiveTrigger.manager.dialogueIsPlaying)
            {
                return;
            }

            if (updatePlayerInput.movementInput != Vector2.zero)
            {
                if (characterAnimationController != null)
                {
                    characterAnimationController.PlayAnimation(updatePlayerInput.movementInput);
                }
            }
            else
            {
                if (characterAnimationController != null)
                {
                    characterAnimationController.PlayAnimation(updatePlayerInput.movementInput);
                }
            }
            //CheckForFacingRightOrLeft(updatePlayerInput.movementInput);
            playerMovement.MovePlayer(updatePlayerInput.movementInput);
  
        }

        bool dummyBoolForExecuteTaskMethod;
        private void PerformTask()
        {
            if(updatePlayerInput.performTask)
            {
               
                performTaskAction.ExecuteTask(ref dummyBoolForExecuteTaskMethod);
            }
        }
        private void NPCInteract()
        {

            if (updatePlayerInput.performTask && DialogueTrigger.currentActiveTrigger != null)
            {

                DialogueManager dm = DialogueTrigger.currentActiveTrigger.manager;

                if (dm == null)
                {
                    Debug.LogError("Active DialogueTrigger has no DialogueManager assigned!");
                    return;
                }

                if (!dm.dialogueIsPlaying)
                {
                    dm.EnterDialogueMode(DialogueTrigger.currentActiveTrigger.inkJson);
                    Debug.Log("DialogueModeEntered");
                }
                else
                {
                    dm.ContinueStory();
                    Debug.Log("Story Continued");
                }
            }
        }
        
    }
}

