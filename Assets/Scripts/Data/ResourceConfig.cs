using UnityEngine;

[CreateAssetMenu(fileName = "ResourceConfig", menuName = "Game/Resource Config")]
public class ResourceConfig : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private string _displayName;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _startingAmount;

    public string Id => _id;
    public string DisplayName => _displayName;
    public Sprite Icon => _icon;
    public int StartingAmount => _startingAmount;
}