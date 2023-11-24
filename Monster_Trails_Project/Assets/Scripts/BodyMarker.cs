using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMarker : MonoBehaviour
{
    [SerializeField, ReadOnly] List<Marker> _markers = new List<Marker>();

    public List<Marker> Markers { get => _markers; private set => _markers = value; }

    [System.Serializable]
    public class Marker
    {
        public Vector3 position = default;
        public Quaternion rotation = default;

        public Marker(Vector3 _pos, Quaternion _rot)
        {
            position = _pos;
            rotation = _rot;
        }
    }

    private void FixedUpdate()
    {
        UpdateMarkerList();
    }

    public void UpdateMarkerList()
    {
        if (_markers.Count > 99) return;

        _markers.Add(new Marker(transform.position, transform.rotation));
    }

    public void ClearMarkerList()
    {
        _markers.Clear();
        UpdateMarkerList();
    }
}
