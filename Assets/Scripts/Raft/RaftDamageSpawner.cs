using System;
using UnityEngine;
using PlayerResources;
using System.Collections;

public class RaftDamageSpawner : MonoBehaviour
{
    public static RaftDamageSpawner Instance { get; private set; }

    public event Action<bool> OnRaftDamaged;

    [SerializeField] private SpriteMask raftDamageSpriteMask;

    private float randomSpawnFrequency = 0f;
    private float spawnDuration = 0;

    public bool isRaftDamaged { get; private set; } = false;

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
        ResourceManager.Instance.OnRaftRepaired += Instance_OnRaftRepaired;

        randomSpawnFrequency = GetRandomInRange(1, 100);
        spawnDuration = randomSpawnFrequency;
    }
    private void OnDestroy()
    {
        ResourceManager.Instance.OnRaftRepaired -= Instance_OnRaftRepaired;
    }
    private void Instance_OnRaftRepaired(bool obj)
    {
        isRaftDamaged = false;

        StartCoroutine(HideRaftDamageSpriteMask());
    }
    private IEnumerator HideRaftDamageSpriteMask()
    {
        yield return new WaitForSeconds(2);

        raftDamageSpriteMask.gameObject.SetActive(false);

        randomSpawnFrequency = GetRandomInRange(1, 100);
        spawnDuration = randomSpawnFrequency;
    }
    private void Update()
    {
        Countdown();
    }

    private void Countdown()
    {
        if (isRaftDamaged) return;

        spawnDuration -= Time.deltaTime;

        if (spawnDuration <= 0)
        {
            randomSpawnFrequency = GetRandomInRange(1, 100);
            spawnDuration = randomSpawnFrequency;

            SpawnRaftDamage();
        }
    }

    /// <summary>
    /// make the hole visible on the raft
    /// </summary>
    private void SpawnRaftDamage()
    {
        SoundEffectManager.Instance.PlayRaftBreakingSound();
        isRaftDamaged = true;
        raftDamageSpriteMask.gameObject.SetActive(true);

        OnRaftDamaged?.Invoke(true);
        ResourceManager.Instance.MakeRaftDamageSpriteRed();
    }


    /// <summary>
    /// Prevent Amiguity between System.Random and UnityEngine.Random
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    private float GetRandomInRange(float min, float max) => UnityEngine.Random.Range(min, max);
}
