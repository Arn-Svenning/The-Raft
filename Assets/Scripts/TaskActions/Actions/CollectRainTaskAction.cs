using RaftObject;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace TaskAction
{
    public class CollectRainTaskAction : ITaskAction
{
        public static event Action<bool> OnCollectingRain;
        public static event Action<bool> OnCollectingRainNoRain;
 
        public CollectRainTaskAction()
        {
          
        }
        public void ExecuteTask()
        {
            OnCollectingRainNoRain?.Invoke(true);

            if (!RainSpawner.Instance.isRaining) return;

            CollectRain();
            OnCollectingRain?.Invoke(true);
        }
        private void CollectRain()
        {
        }

        public void StopTask()
        {
            OnCollectingRainNoRain?.Invoke(false);
        }
    }
}

