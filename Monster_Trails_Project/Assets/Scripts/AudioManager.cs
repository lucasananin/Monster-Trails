using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Space]
    [SerializeField] AudioSource _musicAudio = null;
    [SerializeField] AudioSource _startGameAudio = null;
    [SerializeField] AudioSource _gameOverAudio = null;
    [SerializeField] AudioSource _scoreAudio = null;
    [Space]
    [SerializeField] AudioClip _scoreClip = null;

    private void Start()
    {
        PlayMusic();
    }

    private void OnEnable()
    {
        GameManager.onGameStart += PlayMusic;
        GameManager.onGameStart += PlayStartGameSfx;
        GameManager.onGameEnd += StopMusic;
        GameManager.onGameEnd += PlayGameOverSfx;
        EggBehaviour.onEggCollected += PlayScoreSfx;
    }

    private void OnDisable()
    {
        GameManager.onGameStart -= PlayMusic;
        GameManager.onGameStart -= PlayStartGameSfx;
        GameManager.onGameEnd -= StopMusic;
        GameManager.onGameEnd -= PlayGameOverSfx;
        EggBehaviour.onEggCollected -= PlayScoreSfx;
    }

    public void PlayMusic()
    {
        if (_musicAudio.isPlaying) return;
        _musicAudio.Play();
    }

    public void StopMusic()
    {
        _musicAudio.Stop();
    }

    private void PlayStartGameSfx()
    {
        _startGameAudio.Play();
    }

    private void StopStartGameSfx()
    {
        _startGameAudio.Stop();
    }

    public void PlayScoreSfx()
    {
        _scoreAudio.PlayOneShot(_scoreClip);
    }

    public void PlayGameOverSfx()
    {
        _gameOverAudio.Play();
    }

    public void StopGameOverSfx()
    {
        _gameOverAudio.Stop();
    }
}
