using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject _startPanel = null;
    [SerializeField] GameObject _endPanel = null;
    [SerializeField] float _endPanelWaitTime = 2f;
    [SerializeField] TextMeshProUGUI _scoreText = null;
    [SerializeField] TextMeshProUGUI _scoreTextShadow = null;
    [SerializeField, ReadOnly] int _scoreCount = 0;

    private void Start()
    {
        _startPanel.gameObject.SetActive(true);
        _endPanel.gameObject.SetActive(false);
        _scoreText.enabled = false;
        _scoreTextShadow.enabled = false;
        RestartScore();
    }

    private void OnEnable()
    {
        GameManager.onGameEnd += ShowEndPanel;
        EggBehaviour.onEggCollected += IncreaseScore;
    }

    private void OnDisable()
    {
        GameManager.onGameEnd -= ShowEndPanel;
        EggBehaviour.onEggCollected -= IncreaseScore;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (!_startPanel.activeSelf && !_endPanel.activeSelf) return;

            _startPanel.gameObject.SetActive(false);
            _endPanel.gameObject.SetActive(false);
            _scoreText.enabled = true;
            _scoreTextShadow.enabled = true;
            RestartScore();
            GameManager.Instance.StartGame();
        }
    }

    private void ShowEndPanel()
    {
        StartCoroutine(ShowEndPanel_routine());
    }

    private IEnumerator ShowEndPanel_routine()
    {
        yield return new WaitForSeconds(_endPanelWaitTime);
        _startPanel.gameObject.SetActive(false);
        _endPanel.gameObject.SetActive(true);
        _scoreText.enabled = true;
        _scoreTextShadow.enabled = true;
    }

    private void RestartScore()
    {
        _scoreCount = 0;
        _scoreText.text = _scoreCount.ToString("D2");
        _scoreTextShadow.text = _scoreText.text;
    }

    private void IncreaseScore()
    {
        _scoreCount++;
        _scoreText.text = _scoreCount.ToString("D2");
        _scoreTextShadow.text = _scoreText.text;
    }
}
