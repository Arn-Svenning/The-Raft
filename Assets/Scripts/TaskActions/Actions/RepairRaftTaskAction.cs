using RaftObject;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaskAction
{
    public class RepairRaftTaskAction : ITaskAction
    {
        public static event Action<bool> OnRepairingRaft;
        public RepairRaftTaskAction()
        {
            
        }
        public void ExecuteTask()
        {
            if (!RaftDamageSpawner.Instance.isRaftDamaged) return;

            OnRepairingRaft?.Invoke(true);
        }
        private void RepairRaft()
        {
          
        }

        public void StopTask()
        {
            OnRepairingRaft?.Invoke(false);
        }
    }
}

