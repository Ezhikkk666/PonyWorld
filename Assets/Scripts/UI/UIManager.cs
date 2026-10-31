using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private GameObject _minigamePanel;
    [SerializeField] private GameObject _achievementsPanel;

    public void OpenShop() => _openPanel(_shopPanel);
    public void OpenMinigames() => _openPanel(_minigamePanel);
    public void OpenAchievements() => _openPanel(_achievementsPanel);

    public void CloseAll()
    {
        _shopPanel.SetActive(false);
        _minigamePanel.SetActive(false);
        _achievementsPanel.SetActive(false);
    }

    private void _openPanel(GameObject panel)
    {
        CloseAll();
        panel.SetActive(true);
    }
}
