using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxBehaviour : MonoBehaviour
{
    [SerializeField] float _timeUntilDestroy = 2f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_timeUntilDestroy);

        Destroy(gameObject);
    }
}
