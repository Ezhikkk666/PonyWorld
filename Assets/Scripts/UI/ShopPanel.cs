using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private PonyShopCard _cardPrefab;

    private List<PonyShopCard> _cards = new();

    private bool _built;

    private void OnEnable()
    {
        if (_built)
            return;
        _buildCards();
        _built = true;
    }

    private void _buildCards()
    {
        foreach (var pony in PonyManager.Instance.AllPonies)
        {
            var card = Instantiate(_cardPrefab, _content);
            _cards.Add(card);
            card.Setup(pony);
        }
    }
}
