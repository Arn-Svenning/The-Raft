using UnityEngine;
using UnityEngine.UI;
using Fish;              // for FishSpawner
using PlayerResources;   // for ResourceManager
using TMPro;

public enum WorldEventBlockerType
{
    None,
    FishSpawned,
    RainStarted,
    RaftDamaged
}

[RequireComponent(typeof(Button))]
public class ChoiceEventBlocker : MonoBehaviour
{
    [Tooltip("Which world event makes this button usable?")]
    public WorldEventBlockerType blockOnEvent = WorldEventBlockerType.None;

    [Tooltip("Suffix to append when the button is inactive")]
    public string disabledSuffix = " (Unavailable)";

    private Button _btn;
    private TextMeshProUGUI _label;

    void Awake()
    {
        _btn = GetComponent<Button>();
        _label = GetComponentInChildren<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        Subscribe();
        // immediate sync
        UpdateInteractable();
    }

    void OnDisable()
    {
        Unsubscribe();
    }

    void Update()
    {
        // continuous sync: catches the moment DialogueManager changes the label
        UpdateInteractable();
    }

    private void Subscribe()
    {
        switch (blockOnEvent)
        {
            case WorldEventBlockerType.FishSpawned:
                FishSpawner.Instance.OnFishSpawned += OnWorldEvent;
                break;
            case WorldEventBlockerType.RainStarted:
                RainSpawner.Instance.OnRainSpawned += _ => OnWorldEvent();
                break;
            case WorldEventBlockerType.RaftDamaged:
                RaftDamageSpawner.Instance.OnRaftDamaged += _ => OnWorldEvent();
                ResourceManager.Instance.OnRaftRepaired += _ => OnWorldEvent();
                break;
        }
    }

    private void Unsubscribe()
    {
        switch (blockOnEvent)
        {
            case WorldEventBlockerType.FishSpawned:
                if (FishSpawner.Instance != null)
                    FishSpawner.Instance.OnFishSpawned -= OnWorldEvent;
                break;
            case WorldEventBlockerType.RainStarted:
                if (RainSpawner.Instance != null)
                    RainSpawner.Instance.OnRainSpawned -= _ => OnWorldEvent();
                break;
            case WorldEventBlockerType.RaftDamaged:
                if (RaftDamageSpawner.Instance != null)
                    RaftDamageSpawner.Instance.OnRaftDamaged -= _ => OnWorldEvent();
                if (ResourceManager.Instance != null)
                    ResourceManager.Instance.OnRaftRepaired -= _ => OnWorldEvent();
                break;
        }
    }

    // adapter for the Fish event signature
    private void OnWorldEvent(int count, Transform t) => UpdateInteractable();
    private void OnWorldEvent() => UpdateInteractable();

    private void UpdateInteractable()
    {
        // determine whether this choice should be active
        bool isActive = blockOnEvent switch
        {
            WorldEventBlockerType.FishSpawned => FishSpawner.Instance.ActiveFishes.Count > 0,
            WorldEventBlockerType.RainStarted => RainSpawner.Instance.isRaining,
            WorldEventBlockerType.RaftDamaged => RaftDamageSpawner.Instance.isRaftDamaged,
            _ => true
        };

        // update button state
        _btn.interactable = isActive;

        // recalculate the base label (strip any old suffix)
        string text = _label.text;
        if (text.EndsWith(disabledSuffix))
            text = text.Substring(0, text.Length - disabledSuffix.Length);

        // reapply either bare text or with suffix
        _label.text = isActive ? text : text + disabledSuffix;
    }
}
