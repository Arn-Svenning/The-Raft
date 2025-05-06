using UnityEngine;
using UnityEngine.SceneManagement;

namespace RaftObject
{
    /// <summary>
    /// This script moves the raft in any given direction
    /// </summary>
    public class RaftMovement : MonoBehaviour
    {
        private enum RaftDirection
        {
            Up
        }

        [SerializeField] private float raftSpeed;
        [SerializeField] private bool isMainMenu = false;

        private RaftDirection currentRaftDirection = RaftDirection.Up;
        public Vector2 movement { get; private set; } = Vector2.zero;

        private void Update()
        {
            if(isMainMenu)
            {
                transform.position = transform.parent.position;
            }
        }
        /// <summary>
        /// Moves the raft in a given direction
        /// </summary>
        /// <param name="rigidBody"></param>
        public void MoveRaft()
        {
            Vector2 movementVector = determineDirection() * raftSpeed * Time.deltaTime;
            movement = movementVector;
            transform.position += (Vector3)movementVector;
        }


        /// <summary>
        /// Determine which direction the raft should move in
        /// </summary>
        /// <returns></returns>
        private Vector2 determineDirection()
        {
            Vector2 raftDirection = Vector2.zero;

            switch(currentRaftDirection)
            {
                case RaftDirection.Up:

                    return raftDirection = new Vector2(0, 1);

                    break;
            }

            return raftDirection;
        }
    }
}

