using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : Singleton<SnakeManager>
{
    [Title("// General")]
    [SerializeField] float _distanceBetween = 0.2f;
    [SerializeField] float _moveSpeed = 280f;
    [SerializeField] float _turnSpeed = 180f;
    [SerializeField] List<BodyPart> _bodyParts = null;

    [Title("// Debug")]
    [SerializeField] int _maxSnakeBody = 5;
    [SerializeField] bool _rotateInput = false;
    [SerializeField, ReadOnly] float _timer = 0;
    [SerializeField, ReadOnly] List<BodyPart> _snakeBody = null;

    private void Start()
    {
        CreateBodyParts();
    }

    private void OnEnable()
    {
        EggBehaviour.onEggCollected += IncreaseSnake;
        BodyDeathCollision.onSnakeDestroyed += StopSnakeHead;
        GameManager.onGameStart += RestartSnake;
    }

    private void OnDisable()
    {
        EggBehaviour.onEggCollected -= IncreaseSnake;
        BodyDeathCollision.onSnakeDestroyed -= StopSnakeHead;
        GameManager.onGameStart -= RestartSnake;
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        IncreaseSnake();
    //    }
    //}

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsPlaying) return;

        ManageSnakeBody();
        SnakeMovement();
    }

    private void CreateBodyParts()
    {
        if (!GameManager.Instance.IsPlaying) return;

        if (_snakeBody.Count == 0)
        {
            int _randomIndex = Random.Range(0, _bodyParts.Count);
            var _instance = Instantiate(_bodyParts[_randomIndex], transform.position, transform.rotation);
            //var _instance = Instantiate(_bodyParts[_maxSnakeBody - 1], transform.position, transform.rotation);
            _snakeBody.Add(_instance);
        }

        var _lastBodyMarker = _snakeBody[_snakeBody.Count - 1].BodyMarker;

        if (_timer == 0)
        {
            _lastBodyMarker.ClearMarkerList();
        }

        if (_snakeBody.Count >= _maxSnakeBody) return;

        _timer += Time.fixedDeltaTime;

        if (_timer >= _distanceBetween)
        {
            int _randomIndex = Random.Range(0, _bodyParts.Count);
            var _instance = Instantiate(_bodyParts[_randomIndex], _lastBodyMarker.Markers[0].position, _lastBodyMarker.Markers[0].rotation);
            //var _instance = Instantiate(_bodyParts[_maxSnakeBody - 1], _lastBodyMarker.Markers[0].position, _lastBodyMarker.Markers[0].rotation);
            _snakeBody.Add(_instance);
            _instance.BodyMarker.ClearMarkerList();
            _timer = 0;
        }
    }

    private void ManageSnakeBody()
    {
        if (_bodyParts.Count > 0)
        {
            CreateBodyParts();
        }

        int _count = _snakeBody.Count;

        for (int i = 0; i < _count; i++)
        {
            if (_snakeBody[i] == null)
            {
                _snakeBody.RemoveAt(i);
                i = i - 1;
            }
        }

        if (_snakeBody.Count == 0)
        {
            Destroy(gameObject);
        }
    }

    [Button]
    private void IncreaseSnake()
    {
        _maxSnakeBody++;
    }

    private void SnakeMovement()
    {
        MoveSnakeHead();
        RotateSnakeHead();
        MoveSnakeBodyParts();
        FlipSprites();
    }

    private void MoveSnakeHead()
    {
        var _snakeHead = _snakeBody[0];
        _snakeHead.Rb.velocity = _snakeHead.transform.right * _moveSpeed * Time.fixedDeltaTime;
    }

    private void StopSnakeHead()
    {
        var _snakeHead = _snakeBody[0];
        _snakeHead.Rb.velocity = Vector2.zero;
    }

    private void RotateSnakeHead()
    {
        float _xInput = Input.GetAxis("Horizontal");
        var _snakeHead = _snakeBody[0];

        if (_rotateInput)
        {
            _xInput = 1;
        }

        if (_xInput != 0)
        {
            _snakeHead.transform.Rotate(new Vector3(0, 0, _xInput * -_turnSpeed * Time.fixedDeltaTime));
        }
    }

    private void MoveSnakeBodyParts()
    {
        if (_snakeBody.Count > 1)
        {
            int _count = _snakeBody.Count;

            for (int i = 1; i < _count; i++)
            {
                BodyMarker _previousBodyMarker = _snakeBody[i - 1].BodyMarker;
                BodyPart _bodyPart = _snakeBody[i];
                _bodyPart.transform.position = _previousBodyMarker.Markers[0].position;
                _bodyPart.transform.rotation = _previousBodyMarker.Markers[0].rotation;
                _previousBodyMarker.Markers.RemoveAt(0);
            }
        }
    }

    private void FlipSprites()
    {
        int _count = _snakeBody.Count;

        for (int i = 0; i < _count; i++)
        {
            var _bodyPart = _snakeBody[i];

            if (i == 0)
            {
                _bodyPart.FlipX();
            }
            else
            {
                BodyMarker _previousBodyMarker = _snakeBody[i - 1].BodyMarker;
                Vector3 _dir = _previousBodyMarker.Markers[0].position - _bodyPart.transform.position;
                Vector3 _velocity = _dir.normalized * _moveSpeed * Time.fixedDeltaTime;
                _bodyPart.FlipX(_velocity.x);
            }
        }
    }

    public bool IsSnakeHead(GameObject _gameObject)
    {
        return _gameObject == _snakeBody[0].gameObject && _gameObject.CompareTag("Player");
    }

    public bool IsInsideSnakePosition(Vector3 _position)
    {
        int _count = _snakeBody.Count;

        for (int i = 0; i < _count; i++)
        {
            if (_snakeBody[i].AreaCollider.bounds.Contains(_position))
            {
                return true;
            }
        }

        return false;
    }

    [Button]
    private void RestartSnake()
    {
        for (int i = _snakeBody.Count - 1; i >= 0; i--)
        {
            Destroy(_snakeBody[i].gameObject);
        }

        _snakeBody.Clear();
        _maxSnakeBody = 1;
        _timer = 0;
    }
}
