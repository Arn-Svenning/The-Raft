using TaskAction;
using UnityEngine;

namespace Visual
{
    /// <summary>
    /// This script is only for visually paddling the oard on the raft
    /// </summary>
    public class PaddleMotion : MonoBehaviour
    {
        [SerializeField] private float wiggleSpeed = 2f;   
        [SerializeField] private float wiggleAmount = 10f; 

        private float timeCounter = 0f;
        private float initialZRotation; 

        [SerializeField] private bool isLeftPaddle = false;
        private void Start()
        {
            initialZRotation = transform.eulerAngles.z;

            PaddleRaftTaskAction.OnPaddlingRaft += PaddleRaftAction_OnPaddlingRaft;
        }
        private void OnDestroy()
        {
            PaddleRaftTaskAction.OnPaddlingRaft -= PaddleRaftAction_OnPaddlingRaft;
        }
        public void TriggerPaddleMovement()
        {
            timeCounter += Time.deltaTime * wiggleSpeed;

            float angleZ = Mathf.Sin(timeCounter) * wiggleAmount;

            
            if (isLeftPaddle)
            {
                transform.rotation = Quaternion.Euler(0, 0, (initialZRotation - angleZ));
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, initialZRotation + angleZ);
            }
        }

        private void PaddleRaftAction_OnPaddlingRaft(bool isPaddling)
        {
            TriggerPaddleMovement();
        }
    }


}


