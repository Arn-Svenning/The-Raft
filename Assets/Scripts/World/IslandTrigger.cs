using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandTrigger : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Raft"))
        {
            Debug.Log("YOU WIN!");
            GameManager.Instance.ChangeGameState(GameManager.GameState.Won);
        }
    }
}
