using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _fwd;

    private void Awake()
    {
        _fwd = transform.forward;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    void Update()
    {
        transform.position += _fwd * _speed * Time.deltaTime;
    }
}
