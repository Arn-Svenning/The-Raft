using System;
using PlayerResources;
using PlayerUI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(UIManager))]
[RequireComponent(typeof(ResourceManager))]
public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        MainMenu,
        Paused,

        Playing,
        Won,
        Died
    }

    public static GameManager Instance { get; private set; }

    [SerializeField] private Canvas playerWinLooseCanvas;

    public event Action<GameState> OnGameStateChanged;
    public event Action<bool> OnPlayerWon;

    private GameState currentGameState = GameState.Playing;
    private void Awake()
    {
        Time.timeScale = 1f;
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
        
    }
    private void Update()
    {
        switch (currentGameState)
        {
            case GameState.MainMenu:

                break;

            case GameState.Playing:

                HandleResources();
                UIManager.Instance.UpdateUI();

                break;

            case GameState.Won:

                PlayerHasWonGame();

                break;

            case GameState.Died:

                PlayerHasDied();
                break;

            case GameState.Paused:

                break;
        }

    }
    /// <summary>
    /// Called from other classes, handles switching of the gamestate
    /// </summary>
    /// <param name="newGameState"></param>
    public void ChangeGameState(GameState newGameState)
    {
        currentGameState = newGameState;
        OnGameStateChanged?.Invoke(currentGameState);
    }

    private void PlayerHasWonGame()
    {
        Time.timeScale = 0;
        playerWinLooseCanvas.gameObject.SetActive(true);
    }
    private void PlayerHasDied()
    {
        Time.timeScale = 0;
        playerWinLooseCanvas.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void HandleResources()
    {
        if(RaftDamageSpawner.Instance.isRaftDamaged)
        {
            ResourceManager.Instance.DecreaseRaftHealth();
        }
        ResourceManager.Instance.DecreaseResources();
        
    }
}
