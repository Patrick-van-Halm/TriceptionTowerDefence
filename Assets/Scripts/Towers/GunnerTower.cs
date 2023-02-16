using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunnerTower : Tower
{
    public override GameObject FindEnemy()
    {
        var overlaps = Physics.OverlapCapsule(transform.position + Vector3.down, transform.position + Vector3.up, _data.Range / 2);
        var detected = overlaps.Where(c => c.CompareTag("Enemy")).FirstOrDefault();
        return detected?.gameObject;
    }

    public override void ShootEnemy(GameObject enemy)
    {
        transform.rotation = Quaternion.LookRotation(enemy.transform.position - transform.position, transform.up);
        enemy.GetComponent<Enemy>().TakeDamage(_data.Damage);
    }
}
