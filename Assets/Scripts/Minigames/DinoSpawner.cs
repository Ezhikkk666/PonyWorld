using UnityEngine;

public class DinoSpawner : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private DinoObstacle _obstaclePrefab;

    [Header("Parent")]
    [SerializeField] private RectTransform _spawnParent;

    [Header("Balance")]
    [SerializeField] private float _minInterval = 1.2f;
    [SerializeField] private float _maxInterval = 2.5f;
    [SerializeField] private float _obstacleSpeed = 500f;
    [SerializeField] private float _spawnMargin = 100f;
    [SerializeField] private float _groundY = 40f;

    private float _timer;
    private bool _isSpawning;

    public  void StartSpawning()
    {
        _clearAll();
        _timer = Random.Range(_minInterval, _maxInterval);
        _isSpawning = true;
    }

    public void StopSpawning()
    {
        _isSpawning = false;
    }

    private void Update()
    {
        if (!_isSpawning)
            return;

        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            _spawnOne();
            _timer = Random.Range(_minInterval, _maxInterval);
        }
    }

    private void _spawnOne()
    {
        var obstacle = Instantiate(_obstaclePrefab, _spawnParent);
        obstacle.Setup(_obstacleSpeed);

        float spawnX = _spawnParent.rect.width * 0.5f + _spawnMargin;
        obstacle.Rect.anchoredPosition = new Vector2(spawnX, _groundY);
    }

    private void _clearAll()
    {
        for (int i = _spawnParent.childCount - 1; i >= 0; i--)
        {
            Destroy(_spawnParent.GetChild(i).gameObject);
        }
    }
}
