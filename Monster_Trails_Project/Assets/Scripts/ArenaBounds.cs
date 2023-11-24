using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBounds : MonoBehaviour
{
    [SerializeField] BoxCollider2D _rightCollider = null;
    [SerializeField] BoxCollider2D _leftCollider = null;
    [SerializeField] BoxCollider2D _topCollider = null;
    [SerializeField] BoxCollider2D _bottomCollider = null;

    private void Start()
    {
        AdjustColliders();
    }

    private void AdjustColliders()
    {
        var _cam = Camera.main;

        Vector3 _rightEdge = _cam.ViewportToWorldPoint(new Vector2(1, 0.5f));
        _rightEdge.z = 0;
        _rightCollider.size = new Vector2(1, _cam.orthographicSize * 2);
        _rightCollider.transform.position = _rightEdge + Vector3.right * (_rightCollider.size.x / 1);

        Vector3 _leftEdge = _cam.ViewportToWorldPoint(new Vector2(0, 0.5f));
        _leftEdge.z = 0;
        _leftCollider.size = new Vector2(1, _cam.orthographicSize * 2);
        _leftCollider.transform.position = _leftEdge + Vector3.left * (_leftCollider.size.x / 1);

        float _xSize = _leftEdge.x - _rightEdge.x;
        _xSize = Mathf.Abs(_xSize);

        var _topEdge = _cam.ViewportToWorldPoint(new Vector2(0.5f, 1));
        _topEdge.z = 0;
        _topCollider.size = new Vector2(_xSize, 1);
        _topCollider.transform.position = _topEdge + Vector3.up * (_topCollider.size.y / 1);

        var _bottomEdge = _cam.ViewportToWorldPoint(new Vector2(0.5f, 0));
        _bottomEdge.z = 0;
        _bottomCollider.size = new Vector2(_xSize, 1);
        _bottomCollider.transform.position = _bottomEdge + Vector3.down * (_bottomCollider.size.y / 1);
    }
}
