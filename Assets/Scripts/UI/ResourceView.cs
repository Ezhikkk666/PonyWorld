using TMPro;
using UnityEngine;

public class ResourceView : MonoBehaviour
{
    [SerializeField] private ResourceConfig _config;
    [SerializeField] private TMP_Text _amountText;

    private void Start()
    {
        if (GameData.Instance == null)
            return;
        GameData.Instance.OnResourceChanged += HandleResourceChanged;
        Refresh();
    }

    private void OnDestroy()
    {
        if (GameData.Instance != null)
            GameData.Instance.OnResourceChanged -= HandleResourceChanged;
    }

    private void HandleResourceChanged(ResourceConfig changedConfig, int amount)
    {
        if (changedConfig != _config)
            return;
        _amountText.text = amount.ToString();
    }

    private void Refresh()
    {
        int current = _config == GameData.Instance.Resource1
            ? GameData.Instance.Resource1Amount
            : GameData.Instance.Resource2Amount;
        _amountText.text = current.ToString();
    }
}
