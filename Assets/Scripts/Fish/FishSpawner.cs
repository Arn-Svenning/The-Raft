using System;
using System.Collections.Generic;
using UnityEngine;


namespace Fish
{
    /// <summary>
    /// This script spawns all the fish and keeps track of them
    /// </summary>
    public class FishSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject fishPrefab;
        public static FishSpawner Instance { get; private set; }

        public event Action<int, Transform> OnFishSpawned;

        private List<GameObject> activeFishes = new List<GameObject>();
        public List<GameObject> ActiveFishes { get { return activeFishes; } }

        private float randomSpawnFrequency = 0f;
        private float spawnDuration = 0;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            randomSpawnFrequency = GetRandomInRange(1, 100);
            spawnDuration = randomSpawnFrequency;
        }

        private void Update()
        {
            Countdown();
        }

        private void Countdown()
        {
            spawnDuration -= Time.deltaTime;

            if(spawnDuration <= 0)
            {
                randomSpawnFrequency = GetRandomInRange(1, 100);
                spawnDuration = randomSpawnFrequency;

                SpawnFish();
            }
        }

        /// <summary>
        /// Spawn the fish and add it to the activeFishes list
        /// </summary>
        private void SpawnFish()
        {
            Vector2 spawnPosition = GetRandomSpawnPosition(); 
            GameObject newFish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
            activeFishes.Add(newFish);

            OnFishSpawned?.Invoke(activeFishes.Count, newFish.transform);
        }

        /// <summary>
        /// Get a spawn position just outside of the camera's view
        /// </summary>
        /// <returns></returns>
        private Vector2 GetRandomSpawnPosition()
        {
            Camera mainCamera = Camera.main;
    
            // Get screen boundaries in world coordinates (camera's view area)
            float screenX = mainCamera.orthographicSize * mainCamera.aspect;
            float screenY = mainCamera.orthographicSize;

            // Randomly select one of the 4 screen edges
            int edge = UnityEngine.Random.Range(0, 4);
            Vector2 spawnPos = Vector2.zero;

            // Convert viewport coordinates to world coordinates
            Vector3 worldPos = Vector3.zero;

            switch (edge)
            {
                case 0: // Left
                    worldPos = mainCamera.ViewportToWorldPoint(new Vector3(0, GetRandomInRange(0f, 1f), mainCamera.nearClipPlane));
                    spawnPos = new Vector2(worldPos.x - 1, worldPos.y); // Shift off-screen to the left
                    break;
                case 1: // Right
                    worldPos = mainCamera.ViewportToWorldPoint(new Vector3(1, GetRandomInRange(0f, 1f), mainCamera.nearClipPlane));
                    spawnPos = new Vector2(worldPos.x + 1, worldPos.y); // Shift off-screen to the right
                    break;
                case 2: // Bottom
                    worldPos = mainCamera.ViewportToWorldPoint(new Vector3(GetRandomInRange(0f, 1f), 0, mainCamera.nearClipPlane));
                    spawnPos = new Vector2(worldPos.x, worldPos.y - 1); // Shift off-screen to the bottom
                    break;
                case 3: // Top
                    worldPos = mainCamera.ViewportToWorldPoint(new Vector3(GetRandomInRange(0f, 1f), 1, mainCamera.nearClipPlane));
                    spawnPos = new Vector2(worldPos.x, worldPos.y + 1); // Shift off-screen to the top
                    break;
            }

            return spawnPos;
        }



        /// <summary>
        /// Destroy fish and remove from list 
        /// </summary>
        /// <param name="fish"></param>
        public void DestroyFish(GameObject fish)
        {
            if(activeFishes.Contains(fish))
            {
                activeFishes.Remove(fish);
                Destroy(fish);
            }
        }

        /// <summary>
        /// Prevent Amiguity between System.Random and UnityEngine.Random
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private float GetRandomInRange(float min, float max) => UnityEngine.Random.Range(min, max);
    }
}

