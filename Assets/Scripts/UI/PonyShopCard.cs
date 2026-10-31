using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PonyShopCard : MonoBehaviour
{
    [Header("UI refs")]
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _incomeText;
    [SerializeField] private Button _actionButton;
    [SerializeField] private TMP_Text _actionButtonText;

    private PonyDefinition _definition;
    private string _ponyId;

    public void Setup(PonyDefinition definition)
    {
        _definition = definition;
        _ponyId = definition.Id;

        _iconImage.sprite = definition.Icon;
        _nameText.text = definition.DisplayName;

        _actionButton.onClick.AddListener(_onActionClicked);

        PonyManager.Instance.OnPonyStateChanged += _handlePonyStateChanged;
        GameData.Instance.OnResourceChanged += _handleResourceChanged;
    }

    private void _onActionClicked()
    {
        PonyManager.Instance.TryBuyOrUpgrade(_ponyId);
    }

    private void _handlePonyStateChanged(string id)
    {
        if (id != _ponyId)
            return;
        _refresh();
    }

    private void _handleResourceChanged(ResourceConfig config, int amount)
    {
        _refreshButtonState();
    }

    private void _refresh()
    {
        var state = PonyManager.Instance.GetState(_ponyId);
        if (state == null || _definition == null)
            return;

        int income = _definition.BaseIncomeAmount * state.Level;
        int clickBonus = _definition.BaseClickBonus * state.Level;

        if (state.Level == 0)
        {
            _levelText.text = "Not owned";
            _incomeText.text = $"Income: +{_definition.BaseIncomeAmount}/{_definition.IncomeIntervalSeconds:0.#}s\nClick: +{_definition.BaseClickBonus}";
            _actionButtonText.text = $"Buy\n{PonyManager.Instance.GetCurrentPrice(_definition)}";
        }
        else
        {
            _levelText.text = $"Lv. {state.Level}";
            _incomeText.text = $"Income: +{income}/{_definition.IncomeIntervalSeconds:0.#}s\nClick: +{clickBonus}";
            _actionButtonText.text = $"Upgrade\n{PonyManager.Instance.GetCurrentPrice(_definition)}";
        }

        _refreshButtonState();
    }

    private void _refreshButtonState()
    {
        if (_definition == null)
            return;

        int price = PonyManager.Instance.GetCurrentPrice(_definition);
        _actionButton.interactable = GameData.Instance.Resource1Amount >= price;
    }
}
