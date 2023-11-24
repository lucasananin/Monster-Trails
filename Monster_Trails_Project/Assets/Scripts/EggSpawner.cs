using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpawner : MonoBehaviour
{
    [SerializeField] EggBehaviour _eggPrefab = null;
    [SerializeField] float _xExtents = 13f;
    [SerializeField] float _yExtents = 6f;
    [SerializeField, ReadOnly] bool _isSpawning = false;
    [SerializeField, ReadOnly] EggBehaviour _currentEgg = null;

    //private void Start()
    //{
    //    SpawnEgg();
    //}

    private void OnEnable()
    {
        EggBehaviour.onEggCollected += SpawnEgg;
        GameManager.onGameStart += RespawnEgg;
    }

    private void OnDisable()
    {
        EggBehaviour.onEggCollected -= SpawnEgg;
        GameManager.onGameStart -= RespawnEgg;
    }

    private void RespawnEgg()
    {
        if (_currentEgg != null)
        {
            Destroy(_currentEgg.gameObject);
        }

        SpawnEgg();
    }

    private void SpawnEgg()
    {
        if (_isSpawning) return;

        StartCoroutine(SpawnEgg_routine());
    }

    private IEnumerator SpawnEgg_routine()
    {
        yield return new WaitForSeconds(0.2f);

        if (!GameManager.Instance.IsPlaying) yield break;

        _isSpawning = true;

        do
        {
            float _randomX = Random.Range(-_xExtents, _xExtents);
            float _randomY = Random.Range(-_yExtents, _yExtents);
            Vector3 _randomPosition = new Vector3(_randomX, _randomY, 0);

            if (SnakeManager.Instance.IsInsideSnakePosition(_randomPosition))
            {
                continue;
            }

            _currentEgg = Instantiate(_eggPrefab, _randomPosition, Quaternion.identity);
            _isSpawning = false;
        } while (_isSpawning);
    }
}
