using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System.IO;

public class DialogueVariables
{
    public Dictionary<string, Ink.Runtime.Object> variables;

    // Initialize dialogue variables from Ink JSON (using the Story class)
    public DialogueVariables(string globalsFilePath)
    {
       
        Story globalVariablesStory = new Story(globalsFilePath);

        // Initialize the dictionary to store global variables
        variables = new Dictionary<string, Ink.Runtime.Object>();

        // Populate the variables dictionary using the story's variablesState
        foreach (var variableName in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(variableName);
            variables.Add(variableName, value);
            Debug.Log("Loaded global variable: " + variableName + " with value: " + value);
        }
    }

    // Start listening for changes in the story variables
    public void StartListening(Story story)
    {
        // Sync the current variables to the story
        VariablesToStory(story);

        // Register for the variable changed event
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    // Stop listening for changes
    public void StopListening(Story story)
    {
        // Unsubscribe from the variable change event
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    // This function is triggered when a variable is changed in the story
    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        if (variables.ContainsKey(name))
        {
            // Update the variable in the dictionary
            variables[name] = value;
            Debug.Log($"Variable '{name}' changed to {value}");
        }
        else
        {
            // Add the new variable if it doesn't exist in the dictionary
            variables.Add(name, value);
            Debug.Log($"New variable '{name}' added with value {value}");
        }
    }

    // Sync the variables to the story's state
    private void VariablesToStory(Story story)
    {
        foreach (var variable in variables)
        {
            // Set the variable in the story's state
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}
