using Fish;
using NPC;
using System.Collections.Generic;
using PlayerResources;
using UnityEngine;

/// <summary>
/// This class is used subscribes to all events that happen in the world
/// </summary>

[RequireComponent(typeof(FishSpawner))]
[RequireComponent(typeof(RainSpawner))]
[RequireComponent(typeof(RaftDamageSpawner))]
public class WorldEventsManager : MonoBehaviour
{
    public static WorldEventsManager Instance { get; private set; }

    private List<NPCInteraction> NPCInteractionInstances = new List<NPCInteraction>();

    [SerializeField] private bool isLLMVersion = true;

    private void Awake()
    {
        if(Instance == null)
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
        SubscribeToWorldEvents();

        NPCInteractionInstances.Add(GameObject.FindAnyObjectByType<NPCInteraction>());
    }
    private void OnDestroy()
    {
        UnsubscribeToWorldEvents();
    }
    private void SubscribeToWorldEvents()
    {
        FishSpawner.Instance.OnFishSpawned += FishSpawner_OnFishSpawned;
        RainSpawner.Instance.OnRainSpawned += RainSpawner_OnRainSpawned;
        RaftDamageSpawner.Instance.OnRaftDamaged += RaftDamageSpawner_OnRaftDamaged;
        ResourceManager.Instance.OnRaftRepaired += ResourceManager_OnRaftRepaired;
        ResourceManager.Instance.OnRaftCantMove += ResourceManager_OnRaftCantMove;
    }

    /// <summary>
    /// When raft is damaged and cannot move
    /// </summary>
    /// <param name="obj"></param>
    private void ResourceManager_OnRaftCantMove(bool obj)
    {
        if(isLLMVersion)
        {
            string worldEventContext = "The raft was damaged too badly and can not be paddled until the damage is fixed. React to this event";
            worldEventContext += "React to this event with a short, in-character remark, **but do NOT change your current state under any circumstances** unless " +
               "the PLAYER tells you to. Always include your current state in this exact format at the beginning of your response: [State: ...].";
            worldEventContext += "The raft cannot be paddled until the damage is fixed";

            RespondToWorldEventLLM(worldEventContext, 1);
        }
        else
        {

        }
        
    }

    private void ResourceManager_OnRaftRepaired(bool obj)
    {
        if(isLLMVersion)
        {
            string worldEventContext = "The raft was successfully repaired. React to this event";
            worldEventContext += "React to this event with a short, in-character remark, **but do NOT change your current state under any circumstances** unless " +
               "the PLAYER tells you to. Always include your current state in this exact format at the beginning of your response: [State: ...].";
            worldEventContext += "Do not act on this event";

            RespondToWorldEventLLM(worldEventContext, 1);
        }
        else
        {

        }
    }

    private void UnsubscribeToWorldEvents()
    {
        FishSpawner.Instance.OnFishSpawned -= FishSpawner_OnFishSpawned;
        RainSpawner.Instance.OnRainSpawned -= RainSpawner_OnRainSpawned;
        RaftDamageSpawner.Instance.OnRaftDamaged -= RaftDamageSpawner_OnRaftDamaged;
        ResourceManager.Instance.OnRaftRepaired -= ResourceManager_OnRaftRepaired;
        ResourceManager.Instance.OnRaftCantMove -= ResourceManager_OnRaftCantMove;
    }

    #region Fish
    private void FishSpawner_OnFishSpawned(int numberOfActiveFishes, Transform transformOfSpawnedFish)
    {
        if(isLLMVersion)
        {
         
            string worldEventContext = "A fish has just appeared near the raft. React to this event";
            worldEventContext += "React to this event with a short, in-character remark, **but do NOT change your current state under any circumstances** unless " +
               "the PLAYER tells you to. Always include your current state in this exact format at the beginning of your response: [State: ...].";
            worldEventContext += "Do not act on this event";

            RespondToWorldEventLLM(worldEventContext, 10);
        }
        else
        {
            /// ADD CODE FOR SCRIPTED VERSION
        }
    }

    private void RainSpawner_OnRainSpawned(bool obj)
    {
        if (isLLMVersion)
        {
            string worldEventContext = "It just started to rain. React to this event";
            worldEventContext += "React to this event with a short, in-character remark, **but do NOT change your current state under any circumstances** unless " +
               "the PLAYER tells you to. Always include your current state in this exact format at the beginning of your response: [State: ...].";
            worldEventContext += "Do not act on this event";

            RespondToWorldEventLLM(worldEventContext, 10);
        }
        else
        {
            /// ADD CODE FOR SCRIPTED VERSION
        }
    }


    private void RaftDamageSpawner_OnRaftDamaged(bool obj)
    {
        if (isLLMVersion)
        {
            string worldEventContext = "The raft is damaged. React to this event";
            worldEventContext += "React to this event with a short, in-character remark, **but do NOT change your current state under any circumstances** unless " +
                "the PLAYER tells you to. Always include your current state in this exact format at the beginning of your response: [State: ...].";
            worldEventContext += "Do not act on this event";

            RespondToWorldEventLLM(worldEventContext, 8);
        }
        else
        {
            /// ADD CODE FOR SCRIPTED VERSION
        }
    }
    #endregion

    private void RespondToWorldEventLLM(string worldEventContext, int oneInXRespondChance)
    {
        int randomChanceToRespond = Random.Range(0, oneInXRespondChance);

        if(randomChanceToRespond == 1)
        {
            foreach (NPCInteraction interaction in NPCInteractionInstances)
            {
                interaction.RespondToWorldEvent(worldEventContext);
            }
        }     
        else if(oneInXRespondChance == 1)
        {
            foreach (NPCInteraction interaction in NPCInteractionInstances)
            {
                interaction.RespondToWorldEvent(worldEventContext);
            }
        }
    }
}
