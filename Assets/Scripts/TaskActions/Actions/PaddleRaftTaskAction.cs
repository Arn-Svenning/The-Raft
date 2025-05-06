using RaftObject;
using System;
using PlayerResources;

namespace TaskAction
{
    public class PaddleRaftTaskAction : ITaskAction
    {
        private Raft raft;

        public static event Action<bool> OnPaddlingRaft; // This is only used in 

        private bool raftCantMove = false;

        public PaddleRaftTaskAction(Raft raft)
        {
            this.raft = raft;
            ResourceManager.Instance.OnRaftCantMove += Instance_OnRaftCantMove;
            ResourceManager.Instance.OnRaftRepaired += Instance_OnRaftRepaired;
        }
        public void Cleanup()
        {
            ResourceManager.Instance.OnRaftCantMove -= Instance_OnRaftCantMove;
            ResourceManager.Instance.OnRaftRepaired -= Instance_OnRaftRepaired;
        }
        private void Instance_OnRaftRepaired(bool obj)
        {
            raftCantMove = false;
        }

        private void Instance_OnRaftCantMove(bool raftDamaged)
        {
            raftCantMove = true;
        }

        public void ExecuteTask()
        {
            if (raftCantMove) return;

            Paddle(raft);

            OnPaddlingRaft?.Invoke(true);
        }
        private void Paddle(Raft raft)
        {
            raft.MoveRaft();
        }

        public void StopTask()
        {
            OnPaddlingRaft?.Invoke(false);
        }
    }
}

