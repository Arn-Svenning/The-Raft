using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;             

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Image raftBackground;
    [SerializeField] private Canvas tutorialCanvas;
    [SerializeField] private string sceneString;

    void Start()
    {
        raftBackground.gameObject.SetActive(true);
        tutorialCanvas.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(sceneString);
    }

    public void ShowTutorial()
    {
        tutorialCanvas.gameObject.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
