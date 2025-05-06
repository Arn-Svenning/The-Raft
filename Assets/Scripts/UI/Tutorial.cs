using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] Canvas TutorialCanvas;
    [SerializeField] GameObject[] tutorials;

    private int currentTutorial;
    private GameObject currentActiveTutorial;

    private void Start()
    {
        Time.timeScale = 0;
    }
    public void GoToNextTutorial()
    {
        currentTutorial++;
        currentActiveTutorial = tutorials[currentTutorial].gameObject;

        foreach(var tutorial in tutorials)
        {
            if(tutorial != currentActiveTutorial)
            {
                tutorial.SetActive(false);
            }
        }
        currentActiveTutorial.SetActive(true); 
    }
    public void GoBackToPreviousTutorial()
    {
        currentTutorial--;
        currentActiveTutorial = tutorials[currentTutorial].gameObject;

        foreach (var tutorial in tutorials)
        {
            if (tutorial != currentActiveTutorial)
            {
                tutorial.SetActive(false);
            }
        }
        currentActiveTutorial.SetActive(true); 
    }
    public void ExitTutorialWindow()
    {
        foreach(var tutorial in tutorials)
        {
            tutorial.gameObject.SetActive(false);
        }


        currentTutorial = 0;
        currentActiveTutorial = tutorials[currentTutorial].gameObject;
        currentActiveTutorial.gameObject.SetActive(true);
        
        TutorialCanvas.gameObject.SetActive(false);

        if(GameManager.Instance != null)
        {
            GameManager.Instance.ChangeGameState(GameManager.GameState.Playing);
        }
        
        Time.timeScale = 1;
    }
}
