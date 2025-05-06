using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenuScene : MonoBehaviour
{
    [SerializeField] private string mainMenuVersion;

    private void Start()
    {
        SceneManager.LoadScene(mainMenuVersion);
    }
}
