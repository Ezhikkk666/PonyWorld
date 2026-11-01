using TMPro;
using UnityEngine;

public class DinoGame : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _lobbyPanel;
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _gameOverPanel;

    [Header("Scene refs")]
    [SerializeField] private DinoPlayer _player;
    [SerializeField] private DinoSpawner _spawner;
    [SerializeField] private RectTransform _obstacleRoot;

    [Header("UI")]
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _finalScoreText;
    [SerializeField] private TMP_Text _rewardText;

    [Header("Balance")]
    [SerializeField] private int _rewardDivisor = 20;

    private bool _isPlaying;
    private int _score;
    private float _scoreAccumulator;

    private void Update()
    {
        if (!_isPlaying)
            return;

        _scoreAccumulator += Time.deltaTime;
        while (_scoreAccumulator >= 1f)
        {
            _scoreAccumulator -= 1f;
            _score++;
            _updateScoreText();
        }

        _checkCollision();
    }

    public void StartGame()
    {
        _lobbyPanel.SetActive(false);
        _gamePanel.SetActive(true);
        _gameOverPanel.SetActive(false);

        _score = 0;
        _scoreAccumulator = 0f;
        _updateScoreText();

        _player.ResetToStart();
        _player.enabled = true;
        _spawner.StartSpawning();

        _isPlaying = true;
    }

    public void GoToLobby()
    {
        _isPlaying = false;
        _spawner.StopSpawning();
        _gamePanel.SetActive(false);
        _gameOverPanel.SetActive(false);
        _lobbyPanel.SetActive(true);
    }

    private void OnPlayerDied()
    {
        _isPlaying = false;
        _spawner.StopSpawning();
        _player.enabled = false;

        int reward = _score / _rewardDivisor;
        if (reward > 0)
        {
            GameData.Instance.AddResource1(reward);
        }

        _finalScoreText.text = $"Score: {_score}";
        _rewardText.text = $"+{reward} Pony Coin";
        _gameOverPanel.SetActive(true);
    }

    private void _checkCollision()
    {
        Rect playerRect = _getWorldRect(_player.Rect);

        for (int i = 0; i < _obstacleRoot.childCount; i++)
        {
            var child = _obstacleRoot.GetChild(i) as RectTransform;
            if (child == null)
                continue;

            if (playerRect.Overlaps(_getWorldRect(child)))
            {
                OnPlayerDied();
                return;
            }
        }
    }

    private static Rect _getWorldRect(RectTransform rt)
    {
        var corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        return Rect.MinMaxRect(corners[0].x, corners[0].y, corners[2].x, corners[2].y);
    }

    private void _updateScoreText()
    {
        _scoreText.text = $"Score: {_score}";
    }
}
