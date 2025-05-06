using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerName : MonoBehaviour
{
    public static SetPlayerName Instance { get; private set; }

    public string playerName { get; private set; }

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
        DontDestroyOnLoad(gameObject);
    }
    public void SetName(string chosenName)
    {
        playerName = chosenName;
    }
}
