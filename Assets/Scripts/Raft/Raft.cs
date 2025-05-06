using TaskAction;
using UnityEditor;
using UnityEngine;

/// <summary>
/// This script houses all of the raft functionalites
/// </summary>
namespace RaftObject
{
    [RequireComponent(typeof(RaftMovement))]
    public class Raft : MonoBehaviour
    {
        #region Raft Components

        RaftMovement raftMovement;
        Rigidbody2D raftRigidBody;
        Animator animator;

        #endregion
        private void Start()
        {
            raftMovement = GetComponent<RaftMovement>();
            raftRigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
          
        }
        public void MoveRaft()
        {
            animator.Play("Raft_Move_Up");
            raftMovement.MoveRaft();
        }
    }
}


