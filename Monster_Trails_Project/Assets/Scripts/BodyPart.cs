using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField] BodyMarker _bodyMarker = null;
    [SerializeField] Rigidbody2D _rb = null;
    [SerializeField] SpriteRenderer _spriteRenderer = null;
    [SerializeField] Collider2D _areaCollider = null;

    public BodyMarker BodyMarker { get => _bodyMarker; private set => _bodyMarker = value; }
    public Rigidbody2D Rb { get => _rb; private set => _rb = value; }
    public Collider2D AreaCollider { get => _areaCollider; private set => _areaCollider = value; }

    private void LateUpdate()
    {
        _spriteRenderer.transform.rotation = Quaternion.identity;
    }

    public void FlipX()
    {
        FlipX(_rb.velocity.x);
    }

    public void FlipX(float _xVelocity)
    {
        if (_xVelocity > 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_xVelocity < 0)
        {
            _spriteRenderer.flipX = false;
        }
    }
}
