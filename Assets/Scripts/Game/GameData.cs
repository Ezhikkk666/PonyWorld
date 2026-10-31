using System;
using UnityEngine;

public class GameData : MonoBehaviour
{
   public static GameData Instance { get; private set; }

    [Header("Resources")]
    [SerializeField] private ResourceConfig _resource1;
    [SerializeField] private ResourceConfig _resource2;
    [SerializeField] private int _baseClickPower = 1;

    public event Action<ResourceConfig, int> OnResourceChanged;
    public event Action<int> OnClickPowerChanged;

    private int _resource1Amount;
    private int _resource2Amount;
    private int _ponyClickBonus = 0;

    public int Resource1Amount => _resource1Amount;
    public int Resource2Amount => _resource2Amount;
    public int ClickPower => _baseClickPower + _ponyClickBonus;
    public ResourceConfig Resource1 => _resource1;
    public ResourceConfig Resource2 => _resource2;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _resource1Amount = _resource1 != null ? _resource1.StartingAmount : 0;
        _resource2Amount = _resource2 != null ? _resource2.StartingAmount : 0;
    }

    public void Start()
    {
        OnResourceChanged?.Invoke(_resource1, _resource1Amount);
        OnResourceChanged?.Invoke(_resource2, _resource2Amount);
        OnClickPowerChanged?.Invoke(ClickPower);
    }

    public void AddResource1(int amount)
    {
        if (amount <= 0)
            return;
        _resource1Amount += amount;
        OnResourceChanged?.Invoke(_resource1, _resource1Amount);
    }

    public void AddResource2(int amount)
    {
        if (amount <= 0)
            return;
        _resource1Amount += amount;
        OnResourceChanged?.Invoke(_resource2, _resource2Amount);
    }

    public bool TrySpendResource1(int amount)
    {
        if (amount < 0 || _resource1Amount < amount)
            return false;
        _resource1Amount -= amount;
        OnResourceChanged?.Invoke(_resource1, _resource1Amount);
        return true;
    }

    public void SetPonyClickBonus(int total)
    {
        if (_ponyClickBonus == total)
            return;
        _ponyClickBonus = total;
        OnClickPowerChanged?.Invoke(ClickPower);
    }
}
