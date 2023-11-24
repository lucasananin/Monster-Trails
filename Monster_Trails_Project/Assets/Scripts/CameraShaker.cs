using Cinemachine;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] CinemachineImpulseSource _impulseSource = null;
    [SerializeField] float _maxVelocity = 0.5f;

    private void OnEnable()
    {
        BodyDeathCollision.onSnakeDestroyed += ShakeCam;
    }

    private void OnDisable()
    {
        BodyDeathCollision.onSnakeDestroyed -= ShakeCam;
    }

    [Button]
    private void ShakeCam()
    {
        float _xVelocity = Random.Range(-_maxVelocity, _maxVelocity);
        float _yVelocity = Random.Range(-_maxVelocity, _maxVelocity);
        _impulseSource.m_DefaultVelocity = new Vector3(_xVelocity, _yVelocity, 0);
        _impulseSource.GenerateImpulse();
    }
}
