using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PonyState
{
    public string Id;
    public int Level;
    public float Timer;
}

public class PonyManager : MonoBehaviour
{
    public static PonyManager Instance { get; private set; }

    [SerializeField] private List<PonyDefinition> _allPonies = new();
    
    private readonly Dictionary<string, PonyDefinition> _poniesById = new();
    private readonly Dictionary<string, PonyState> _states = new();

    public event Action<string> OnPonyStateChanged;
    public event Action<PonyState> OnPonyIncomeTick;

    public IReadOnlyList<PonyDefinition> AllPonies => _allPonies;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(gameObject); 
            return; 
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (var _pony in _allPonies)
        {
            if (_pony == null || string.IsNullOrEmpty(_pony.Id))
                continue;
            if (_poniesById.ContainsKey(_pony.Id))
            {
                Debug.LogError($"Дублирующая id пони: {_pony.Id}");
                continue;
            }
            _poniesById.Add(_pony.Id, _pony);
            _states.Add(_pony.Id, new PonyState { Id = _pony.Id, Level = 0, Timer = 0f });
        }
    }

    private void Update()
    {
        float dt = Time.deltaTime;

        foreach (var state in _states.Values)
        {
            if (state.Level <= 0)
                continue;

            var _def = _poniesById[state.Id];
            float interval = Mathf.Max(0.01f, _def.IncomeIntervalSeconds);

            state.Timer += dt;

            while (state.Timer >= interval)
            {
                state.Timer -= interval;

                int income = _def.BaseIncomeAmount * state.Level;
                GameData.Instance.AddResource1(income);

                OnPonyIncomeTick?.Invoke(state);
            }
        }
    }

    public PonyDefinition GetDefinition(string id)
        => _poniesById.TryGetValue(id, out var def) ? def : null;

    public PonyState GetState(string id)
        => _states.TryGetValue(id, out var st) ? st : null;

    public int GetCurrentPrice(PonyDefinition def)
    {
        var state = GetState(def.Id);
        if (state == null)
            return int.MaxValue;

        if (state.Level == 0)
            return def.BaseBuyPrice;

        float price = def.BaseUpgradePrice * Mathf.Pow(def.UpgradePriceMultiplier, state.Level - 1);
        return Mathf.RoundToInt(price);
    }

    public bool TryBuyOrUpgrade(string id)
    {
        var def = GetDefinition(id);
        var state = GetState(id);
        if (def == null || state == null)
            return false;

        int price = GetCurrentPrice(def);
        if (!GameData.Instance.TrySpendResource1(price))
            return false;

        state.Level += 1;
        if (state.Level == 1)
            state.Timer = 0f;

        _recalculateClickBonus();
        OnPonyStateChanged?.Invoke(id);
        return true;
    }

    public int GetTotalClickBonus()
    {
        int total = 0;
        foreach (var state in _states.Values)
        {
            if (state.Level <= 0)
                continue;
            total += _poniesById[state.Id].BaseClickBonus * state.Level;
        }
        return total;
    }

    private void _recalculateClickBonus()
    {
        GameData.Instance.SetPonyClickBonus(GetTotalClickBonus());
    }
}
