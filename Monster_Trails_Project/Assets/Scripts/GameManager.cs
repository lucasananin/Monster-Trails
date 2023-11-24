using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] bool _isPlaying = true;

    public static System.Action onGameStart = null;
    public static System.Action onGameEnd = null;

    public bool IsPlaying { get => _isPlaying; private set => _isPlaying = value; }

    private void Awake()
    {
        _isPlaying = false;
    }

    //private void Start()
    //{
    //    StartGame();
    //}

    private void OnEnable()
    {
        BodyDeathCollision.onSnakeDestroyed += EndGame;
    }

    private void OnDisable()
    {
        BodyDeathCollision.onSnakeDestroyed -= EndGame;
    }

    [Button]
    public void StartGame()
    {
        _isPlaying = true;
        onGameStart?.Invoke();
    }

    [Button]
    private void EndGame()
    {
        _isPlaying = false;
        onGameEnd?.Invoke();
    }
}
