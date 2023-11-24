using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBehaviour : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer = null;
    [SerializeField] VfxBehaviour _smokeVfx = null;

    public static Action onEggCollected = null;

    private void Start()
    {
        FlipX();
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (!GameManager.Instance.IsPlaying) return;

        bool _isSnakeHead = SnakeManager.Instance.IsSnakeHead(_other.gameObject);
        //bool _hasPlayerTag = _other.gameObject.CompareTag("Player");

        if (_isSnakeHead /*&& _hasPlayerTag*/)
        {
            Instantiate(_smokeVfx, transform.position, transform.rotation);
            onEggCollected?.Invoke();
            Destroy(gameObject);
        }
    }

    private void FlipX()
    {
        int _randomValue = UnityEngine.Random.Range(0, 2);
        _spriteRenderer.flipX = _randomValue == 1;
    }
}
