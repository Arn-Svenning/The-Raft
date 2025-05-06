using System.Collections;
using TMPro;
using UnityEngine;

public class LoadingLLMScene : MonoBehaviour
{
    public static LoadingLLMScene Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI dotText;
    public int LLMCharactersLoaded { get; set; }

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
    private void Start()
    {
        StartCoroutine(LoadingDotsEffect());
    }
    private void Update()
    {
        if(LLMCharactersLoaded == 2)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
    private IEnumerator LoadingDotsEffect()
    {
        string baseText = "";
        int dotCount = 0;

        while (true)
        {
            dotText.text = baseText + new string('.', dotCount);
            dotCount = (dotCount + 1) % 4; 
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }
}
