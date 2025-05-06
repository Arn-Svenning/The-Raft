using UnityEngine;

[CreateAssetMenu(fileName = "LLM Prompt", menuName = "LLM/Prompt")]
public class ConstructLLMPrompt : ScriptableObject
{
    // This is individual prompt
    [SerializeField] private TextAsset personality;
    [SerializeField] private TextAsset criticalRules;
    [SerializeField] private TextAsset validStates;
    [SerializeField] private TextAsset formatExamples;
    [SerializeField] private TextAsset theSurvivors;

    public string GetPrompt(string playerName)
    {
        string finalPrompt = "";

        finalPrompt += SetupPersonalityPrompt(playerName);
        finalPrompt += SetupGeneralPrompt(theSurvivors, playerName);
        finalPrompt += SetupGeneralPrompt(criticalRules, playerName);
        finalPrompt += SetupGeneralPrompt(validStates, playerName);
        finalPrompt += SetupGeneralPrompt(formatExamples, playerName);
        
        return finalPrompt;
    }
    private string SetupPersonalityPrompt(string playerName)
    {
        if (personality == null)
        {
            Debug.LogWarning("Prompt file is not assigned.");
            return "";
        }

        string rawText = personality.text;
        string replaced = rawText.Replace("{playerName}", playerName);
        return replaced;
    }

    private string SetupGeneralPrompt(TextAsset prompt, string playerName)
    {
        if (prompt == null)
        {
            Debug.LogWarning("Prompt file is not assigned.");
            return "";
        }

        string rawText = prompt.text;
        string replaced = rawText.Replace("{playerName}", playerName);
        return replaced;
    }
}

