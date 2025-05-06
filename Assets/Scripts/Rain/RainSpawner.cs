using PlayerResources;
using System;
using UnityEngine;

/// <summary>
/// Spawns rain when the thirst resource is below certain thresholds
/// </summary>
public class RainSpawner : MonoBehaviour
{
    public static RainSpawner Instance { get; private set; }

    public event Action<bool> OnRainSpawned;

    [SerializeField] private ParticleSystem rainParticles;

    private const float RAINFALL_ACTIVE_DURATION = 20f;
    private float rainfallTimer;

    public bool isRaining { get; private set; }

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
        ResourceManager.Instance.OnThirstResourceLow += Instance_OnThirstResourceLow;

        randomSpawnFrequency = GetRandomInRange(1, 100);
        spawnDuration = randomSpawnFrequency;
    }

    private void OnDestroy()
    {
        ResourceManager.Instance.OnThirstResourceLow -= Instance_OnThirstResourceLow;
    }
    private void Update()
    {
        if (!isRaining) return;

        RainfallCountdown();
        Countdown();
    }
    private void RainfallCountdown()
    {
        rainfallTimer -= Time.deltaTime;
        if(rainfallTimer <= 0)
        {
            rainParticles.gameObject.SetActive(false);
            isRaining = false;
        }
    }
    private void Instance_OnThirstResourceLow(float thirstValue)
    {
        SpawnRain();
    }
    private void Countdown()
    {
        spawnDuration -= Time.deltaTime;

        if (spawnDuration <= 0)
        {
            randomSpawnFrequency = GetRandomInRange(1, 100);
            spawnDuration = randomSpawnFrequency;

            SpawnRain();
        }
    }
    private void SpawnRain()
    {
        rainParticles.gameObject.SetActive(true);

        OnRainSpawned?.Invoke(true);

        isRaining = true;
        rainfallTimer = RAINFALL_ACTIVE_DURATION;
    }
    private float GetRandomInRange(float min, float max) => UnityEngine.Random.Range(min, max);
}
