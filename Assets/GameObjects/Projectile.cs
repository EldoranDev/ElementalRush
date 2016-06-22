using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour { 

    public float Speed;
    public int Damage;
    public float Radius;

    private Transform _target;

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            var direction = (_target.position - transform.position);

            transform.Translate(direction.normalized*Speed*Time.deltaTime, Space.World);

            if (direction.magnitude <= 0.1)
            {
                var enemy = _target.gameObject.GetComponent<Enemy>();
                enemy.DealDamage(Damage);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

}
