using TMPro;

namespace Chat
{
    [System.Serializable]
    public class Message 
    {
        public string text { get; set; }

        public TextMeshProUGUI textObject { get; set; } 
    }
}

