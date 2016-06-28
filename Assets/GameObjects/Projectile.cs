using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour { 

    public float Speed;
    public int Damage;
    public float ExplosionRadius;

	public DamageType Type;

    private Transform _target;
    private Vector3 _targetPosition;

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            _targetPosition = _target.position;
        }

        var direction = (_targetPosition - transform.position);

		transform.rotation = Quaternion.LookRotation (direction);
        transform.Translate(direction.normalized*Speed*Time.deltaTime, Space.World);

        if (direction.magnitude <= 0.1)
        {
            if (_target != null && ExplosionRadius == 0)
            {
                var enemy = _target.gameObject.GetComponent<Enemy>();

				enemy.DealDamage(Damage * Type.GetDamageModifier(enemy.ArmorType));
            }
            else
            {
                var enemys = Physics.OverlapSphere(transform.position, ExplosionRadius, LayerMask.GetMask("EnemyLayer"));

                if (enemys.Length > 0)
                {
                    foreach (var enemy in enemys)
                    {
						var e = enemy.GetComponent<Enemy> ();

						e.DealDamage(Damage * Type.GetDamageModifier(e.ArmorType));
                    }
                }
            }

            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

}
