using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyAnimator : MonoBehaviour
{
    [SerializeField] Animator _animator = null;

    private int _dieIndexHash = Animator.StringToHash("DieIndex");

    private void OnEnable()
    {
        BodyDeathCollision.onSnakeDestroyed += Die;
    }

    private void OnDisable()
    {
        BodyDeathCollision.onSnakeDestroyed -= Die;
    }

    [Button]
    public void Die()
    {
        int _randomIndex = Random.Range(0, 2);
        _animator.SetInteger(_dieIndexHash, _randomIndex);
    }
}
