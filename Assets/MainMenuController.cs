using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;             

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Image raftBackground;
    [SerializeField] private Canvas tutorialCanvas; 
    [SerializeField] private string sceneString;
    [SerializeField] private Canvas characterCreatorCanvas;

    [SerializeField] private bool isLLMVersion = false;

    [Header("This is only for LLM version")]
    [SerializeField] private GameObject normalMenu;
    [SerializeField] private GameObject choosePlayerNameMenu;
    [SerializeField] private Button startGameFromNameSelectionButton;
    [SerializeField] private TMP_InputField playerName;

    void Start()
    {
        raftBackground.gameObject.SetActive(true);
        tutorialCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isLLMVersion) return;

        if (string.IsNullOrWhiteSpace(playerName.text))
        {
            startGameFromNameSelectionButton.interactable = false;
        }
        else
        {
            startGameFromNameSelectionButton.interactable = true;
        }
    }
    public void ChoosePlayerName()
    {
        raftBackground.gameObject.SetActive(true);
        characterCreatorCanvas.gameObject.SetActive(false);
        normalMenu.SetActive(false);
        choosePlayerNameMenu.SetActive(true);
    }
    public void GoBackToNormalMenu()
    {
        raftBackground.gameObject.SetActive(true);
        characterCreatorCanvas.gameObject.SetActive(false);
        if(choosePlayerNameMenu.gameObject != null)
        {
            choosePlayerNameMenu.SetActive(false);
        }
        
        normalMenu.SetActive(true); 
    }
    
    public void StartGame()
    {
        if(isLLMVersion)
        {
            SetPlayerName.Instance.SetName(playerName.text);
        }
        


        SceneManager.LoadScene(sceneString);
    }

    public void ShowTutorial()
    {
        tutorialCanvas.gameObject.SetActive(true);
    }
    public void OpenCharacterCreator()
    {
        raftBackground.gameObject.SetActive(false);
        characterCreatorCanvas.gameObject.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
