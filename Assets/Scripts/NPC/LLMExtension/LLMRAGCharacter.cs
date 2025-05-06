using LLMUnity;
using NPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

namespace NPC
{
    public class LLMRAGCharacter : LLMCharacter
    {
        [Header("USE SCRIPTABLE OBJECT FOR PROMPT INSTEAD")]
        [Chat] public ConstructLLMPrompt llmPrompt;
 
        private void Start()
        {
            if(SetPlayerName.Instance != null)
            {
                playerName = SetPlayerName.Instance.playerName;
            }
            
            SetPrompt(llmPrompt.GetPrompt(playerName));
            grammarString = ConstructGrammar();
        }

        /// <summary>
        /// This method extends the functionality of the Chat method to also use RAG when answering the query
        /// </summary>
        /// <param name="worldRAG"></param>
        /// <param name="state"></param>
        /// <param name="query"></param>
        /// <param name="callback"></param>
        /// <param name="completionCallback"></param>
        /// <param name="addToHistory"></param>
        /// <returns></returns>
        public async Task<string> ChatWithRAG(NPCDataLoader npcData, DetermineNPCState state, string query, Callback<string> callback = null, EmptyCallback completionCallback = null, bool addToHistory = true)
        {

            string result = await Chat(query, callback, completionCallback, addToHistory);

            state.DetermineState(result);

            return result;
        }

        /// <summary>
        /// Search data in RAG and construct prompt based on that data.
        /// </summary>
        /// <param name="rag"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        private async Task<string> Retrieval(NPCDataLoader rag, string query, DetermineNPCState state)
        {
            
            (string[] similarPhrases, float[] distances) = await rag.worldRAG.Search(query, 2, "World");

            string prompt = "Answer the user query based on the provided world data.\n\n";
            prompt += $"User query: {query}\n\n";
            prompt += $"World Data:\n";

            foreach (string similarPhrase in similarPhrases) prompt += $"\n- {similarPhrase}";

            prompt += $"Your current state is [{state.npcStateMachine.currentNPCStateName}]";
            prompt += $"Do not change state if the player doesn't explicitly tell you to do so";

            prompt += $"\nNow answer this user query:\n\"{query}\"\n";

            return prompt;
        }
        
        public async Task<string> ConstructPromptRAG(NPCDataLoader rag, string query, DetermineNPCState state)
        {            
            string prompt = await Retrieval(rag, query, state);
            return prompt;
        }
        
        private string ConstructGrammar()
        {
            return @"root ::= state message
                state ::= ""[Idle]"" | ""[Paddling]"" | ""[Fishing]"" | ""[Collect Rain]"" | ""[Repair Raft]"" | ""[Casual Chatter]""
                message ::= .+";
        }

        //private int maxChatHistory = 3;
        //public override void AddMessage(string role, string content)
        //{
        //    base.AddMessage(role, content);

        //    // Trim old messages if exceeding max allowed
        //    if (chat.Count > maxChatHistory)
        //    {
        //        chat.RemoveAt(0); // remove the oldest
        //    }
        //}

    }
}

