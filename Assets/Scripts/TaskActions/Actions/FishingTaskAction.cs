using Fish;
using System;
using UnityEngine;

namespace TaskAction
{
    public class FishingTaskAction : ITaskAction
    {
        private const float CATCH_CHECK_INTERVAL = 1f; 
        private float timer = 0f;

        public static event Action<bool> OnFishCaught;
        public static event Action<bool> OnFishing;

        public void ExecuteTask()
        {
            timer += Time.deltaTime;
   
            OnFishing?.Invoke(true);

            if (timer >= CATCH_CHECK_INTERVAL)
            {
                timer = 0f; 
                Fishing();
            }
        }

        private void Fishing()
        {
           
            if (FishSpawner.Instance.ActiveFishes.Count == 0) return;

            for (int i = 0; i < FishSpawner.Instance.ActiveFishes.Count; i++)
            {
                if (CatchFish())
                {
                    FishSpawner.Instance.DestroyFish(FishSpawner.Instance.ActiveFishes[i]);
                    OnFishCaught?.Invoke(true);
                    return;
                }
            }
        }

        private bool CatchFish()
        {
            int randomNr = UnityEngine.Random.Range(0, 10); // 1 in 10 chance to catch a fish
            return randomNr == 0;
        }

        public void StopTask()
        {
            OnFishing?.Invoke(false);
        }
        
    }

}

