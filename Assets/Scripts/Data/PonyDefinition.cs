using UnityEngine;

[CreateAssetMenu(fileName = "PonyDefinition", menuName = "Game/Pony Definition")]
public class PonyDefinition : ScriptableObject
{
    [Header("Identity")]
    [SerializeField] private string _id;
    [SerializeField] private string _displayName;
    [SerializeField, TextArea] private string _description;
    [SerializeField] private Sprite _icon;

    [Header("Prices")]
    [SerializeField] private int _baseBuyPrice = 100;
    [SerializeField] private int _baseUpgradePrice = 50;
    [SerializeField] private float _upgradePriceMultiplier = 1.5f;

    [Header("Income per tick")]
    [Tooltip("Сколько Pony Coin даёт за один тик на 1 уровне")]
    [SerializeField] private int _baseIncomeAmount = 1;
    [Tooltip("Интервал между тиками в секундах")]
    [SerializeField] private float _incomeIntervalSeconds = 2f;

    [Header("Click bonus")]
    [Tooltip("Насколько усиливает клик на 1 уровне")]
    [SerializeField] private int _baseClickBonus = 1;

    public string Id => _id;
    public string DisplayName => _displayName;
    public string Description => _description;
    public Sprite Icon => _icon;
    public int BaseBuyPrice => _baseBuyPrice;
    public int BaseUpgradePrice => _baseUpgradePrice;
    public float UpgradePriceMultiplier => _upgradePriceMultiplier;
    public int BaseIncomeAmount => _baseIncomeAmount;
    public float IncomeIntervalSeconds => _incomeIntervalSeconds;
    public int BaseClickBonus => _baseClickBonus;
}