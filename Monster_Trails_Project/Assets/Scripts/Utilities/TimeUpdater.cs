using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimeUpdater : MonoBehaviour
{
    [SerializeField] float _minUpdateRate = 8f;
    [SerializeField] float _maxUpdateRate = 16f;
    [Space]
    [SerializeField] float _updateRate = 0f;
    [SerializeField] float _nextUpdate = 0f;

    private void Start()
    {
        UpdateValues();
        _updateRate = GetRandomValue();
    }

    private void LateUpdate()
    {
        _nextUpdate += Time.deltaTime;

        if (_nextUpdate > _updateRate)
        {
            _nextUpdate -= _updateRate;
            _updateRate = GetRandomValue();

            UpdateValues();
        }

        //OnLateUpdate();
    }

    private float GetRandomValue()
    {
        return Random.Range(_minUpdateRate, _maxUpdateRate);
    }

    //protected virtual void OnLateUpdate()
    //{
    //    //
    //}

    public abstract void UpdateValues();
}
