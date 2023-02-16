using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }
}
