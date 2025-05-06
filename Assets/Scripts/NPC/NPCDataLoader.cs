using CSV;
using LLMUnity;
using Player;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

namespace NPC
{
    /// <summary>
    /// This class loads all the game world data to the NPC LLM
    /// </summary>
    public class NPCDataLoader : MonoBehaviour
    {
        public RAG worldRAG;
        public RAG conversationRAG;

        [SerializeField] private LLMCharacter llmCharacter;

        private const string WORLD_DATA_RAG_PATH = "rag.zip";
        public CSVWriter worldDataCSV;


        private async void Start()
        {
            worldDataCSV = new CSVWriter(WORLD_DATA_RAG_PATH);
            await LoadGameData();
        }

        /// <summary>
        /// Loads the game world data this information is accessible to all NPCs
        /// </summary>
        /// <returns></returns>
        private async Task LoadGameData()
        {

            string playerName = PlayerCharacter.playerName;

            worldDataCSV.AddCSVData(1, "World", $"You were aboard a cruise ship which {playerName} was the captain of, crossing a vast ocean known as The Endless Blue when a violent storm struck and sank the vessel.");
            worldDataCSV.AddCSVData(2, "World", $"Only three people survived the wreck — you, {playerName}, and another person who washed ashore on a small deserted island.");
            worldDataCSV.AddCSVData(3, "World", $"With no hope of rescue, you built a makeshift raft from debris in a desperate attempt to find civilization across The Endless Blue.");
            worldDataCSV.AddCSVData(4, "World", "The Endless Blue is a vast, unpredictable ocean filled with violent storms, powerful currents, and hidden dangers beneath the waves.");
            worldDataCSV.AddCSVData(5, "World", $"{playerName} is the leader of the group — responsible for making decisions, assigning tasks, and guiding everyone toward survival.");
            worldDataCSV.AddCSVData(6, "World", $"{playerName} can instruct the group to collect fresh water, fish for food, steer the raft, repair damages, and maintain morale.");
            worldDataCSV.AddCSVData(7, "World", "As a companion, your role is to follow {playerName}'s instructions and do your part to keep the group alive.");
            worldDataCSV.AddCSVData(8, "World", "The raft is fragile — built from salvaged wood and scrap — and constantly at the mercy of waves, wind, and storms.");
            worldDataCSV.AddCSVData(9, "World", "Every day at sea is a battle against thirst, hunger, exhaustion, and fear. Resources are limited, and choices carry real consequences.");
            worldDataCSV.AddCSVData(10, "World", "Moments of quiet on the raft allow for personal dialogue — share memories, fears, or dreams of rescue with your fellow survivors.");
            worldDataCSV.AddCSVData(11, "World", "Though rumors say islands exist beyond the horizon, finding land in The Endless Blue is uncertain. The only hope is to keep paddling forward — together.");

           

            // Load the saved data file, if it's not found then create it and save it to the Rag path
            bool loaded = await worldRAG.Load(WORLD_DATA_RAG_PATH);
            if (!loaded)
            {
                foreach (string[] input in worldDataCSV.data)
                {
                    string category = input[1];
                    string description = input[2];

                    await worldRAG.Add(description, category);
                }
                // store the embeddings
                worldRAG.Save(WORLD_DATA_RAG_PATH);
            }
            
        }

        private Queue<string> recentMemory = new Queue<string>();
        public async void AddConversationData(string playerQuery, string NPCResponse)
        {
            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"); // Get current UTC time in readable format
            string conversationEntry = $"Player said: {playerQuery}, You said: {NPCResponse}, TIMESTAMP: {timestamp}, Current time is {timestamp}";

            recentMemory.Enqueue(conversationEntry);

            if(recentMemory.Count > 10)
            {
                recentMemory.Dequeue();
            }

            await conversationRAG.Add(conversationEntry);
        }

    }
}

