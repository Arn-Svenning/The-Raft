using PlayerResources;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PlayerUI
{
    /// <summary>
    /// Handles all the UI elements 
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [Header("Pause Menu")]
        [SerializeField] private Canvas pauseMenuCanvas;
        [SerializeField] private Canvas TutorialCanvas;

        [Header("Resources")]
        [SerializeField] private Image[] hungerResource;
        [SerializeField] private Image[] thirstResource;
        [SerializeField] private Image[] moraleResource;
        [SerializeField] private Image[] raftHealthResource;

        #region Chat
        [Header("Chat Related")]
        [SerializeField] private GameObject buttonsGameObject;
        [SerializeField] private GameObject inputFieldGameObject;


        [SerializeField] private Button talkToRobertButton;
        [SerializeField] private Button talkToBobButton;
        [SerializeField] private Button talkToBothtButton;

        [SerializeField] private TMP_InputField RobertInputField;
        [SerializeField] private TMP_InputField BobInputField;
        [SerializeField] private TMP_InputField BothInputField;

        [SerializeField] private Button ExitCurrentDialogueButton;
        #endregion

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void UpdateUI()
        {
            UpdateHungerResourceUI();
            UpdateThirstResourceUI();
            UpdateMoraleResourceUI();
            UpdateRaftHealthResourceUI();
        }

        private void UpdateHungerResourceUI()
        {
            foreach(Image image in hungerResource)
            {
                Image currentActiveImage = ResourceManager.Instance.ManageResourceImage(hungerResource, ResourceManager.Instance.hunger);
                currentActiveImage.fillAmount = ResourceManager.Instance.hunger;
            }
        } 
        private void UpdateThirstResourceUI()
        {
            foreach (Image image in thirstResource)
            {
                Image currentActiveImage = ResourceManager.Instance.ManageResourceImage(thirstResource, ResourceManager.Instance.thirst);
                currentActiveImage.fillAmount = ResourceManager.Instance.thirst;
            }
        }
            
           
        private void UpdateMoraleResourceUI()
        {
            foreach (Image image in moraleResource)
            {
                Image currentActiveImage = ResourceManager.Instance.ManageResourceImage(moraleResource, ResourceManager.Instance.morale);
                currentActiveImage.fillAmount = ResourceManager.Instance.morale;
            }
        }

           
        private void UpdateRaftHealthResourceUI()
        {
            foreach (Image image in raftHealthResource)
            {
                Image currentActiveImage = ResourceManager.Instance.ManageResourceImage(raftHealthResource, ResourceManager.Instance.raftHealth);
                currentActiveImage.fillAmount = ResourceManager.Instance.raftHealth;
            }
        }

        public void TalkToRobertButtonPressed()
        {
            buttonsGameObject.SetActive(false);
            inputFieldGameObject.SetActive(true);
            BobInputField.gameObject.SetActive(false);
            BothInputField.gameObject.SetActive(false);
            RobertInputField.gameObject.SetActive(true);
        }
        
        public void TalkToBobButtonPressed()
        {
            buttonsGameObject.SetActive(false);
            inputFieldGameObject.SetActive(true);
            RobertInputField.gameObject.SetActive(false);
            BothInputField.gameObject.SetActive(false);
            BobInputField.gameObject.SetActive(true);
        }

        public void TalkToBothButtonPressed()
        {
            buttonsGameObject.SetActive(false);
            inputFieldGameObject.SetActive(true);
            RobertInputField.gameObject .SetActive(false);
            BobInputField.gameObject.SetActive(false);
            BothInputField.gameObject.SetActive(true);
        }

        public void InactivateTalkToNPCText()
        {
            buttonsGameObject.SetActive(true);
            inputFieldGameObject.SetActive(false);
            RobertInputField.gameObject.SetActive(false);
            BothInputField.gameObject.SetActive(false);
            BobInputField.gameObject.SetActive(false);
        }

        /// <summary>
        /// Go back to choose between who you will talk to
        /// </summary>
        public void ExitCurrentDialogueButtonPressed()
        {
            RobertInputField.gameObject.SetActive(false);
            BobInputField.gameObject.SetActive(false);
            BothInputField.gameObject.SetActive(false);
            inputFieldGameObject.SetActive(false);
            buttonsGameObject.SetActive(true);
        }

        #region Pause Menu

        public void PauseGame()
        {
            GameManager.Instance.ChangeGameState(GameManager.GameState.Paused);
            Time.timeScale = 0f;
            pauseMenuCanvas.gameObject.SetActive(true);
        }
        public void ContinueGame()
        {
            GameManager.Instance.ChangeGameState(GameManager.GameState.Playing);
            Time.timeScale = 1f;
            pauseMenuCanvas.gameObject.SetActive(false);
        }
        public void ExitToMainMenu()
        {
            Time.timeScale = 1f;
            if(GameManager.Instance.GetIsLLMVersion)
            {
                SceneManager.LoadScene("LLM_Main_Menu");
            }
            else
            {
                SceneManager.LoadScene("Scri_Main_Menu");
            }
        }
        public void OpenTutorialWindow()
        {
            TutorialCanvas.sortingOrder = 5;
            TutorialCanvas.gameObject.SetActive(true);
        }

        #endregion
    }
}

