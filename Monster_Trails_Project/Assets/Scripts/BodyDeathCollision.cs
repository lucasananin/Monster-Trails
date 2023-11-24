using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyDeathCollision : MonoBehaviour
{
    public static System.Action onSnakeDestroyed = null;

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (!GameManager.Instance.IsPlaying) return;

        bool _isSnakeHead = SnakeManager.Instance.IsSnakeHead(_other.gameObject);
        //bool _isBodyPart = _other.gameObject.CompareTag("Player");
        bool _isEdgeCollider = _other.gameObject.CompareTag("Edge");

        if (_isSnakeHead /*&& _isBodyPart*/ || _isEdgeCollider)
        {
            onSnakeDestroyed?.Invoke();
        }
    }
}
