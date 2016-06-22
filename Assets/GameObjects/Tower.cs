﻿using UnityEngine;
using System.Collections;
using System.Linq;

public class Tower : MonoBehaviour
{

    public Projectile ProjectilePrototype;
    public int Cost;
    public int Damage;
    public float Range;
    public float ProjectileSpeed;

    public float Cooldown;

    private float _counter;
    private Transform _out;

	// Use this for initialization
	void Start ()
	{
	    _out = transform.FindChild("__OUT__");
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _counter += Time.deltaTime;

	    if (_counter < Cooldown) return;
        
	    var enemys = Physics.OverlapSphere(transform.position, Range, 1 << LayerMask.NameToLayer("EnemyLayer"));

	    if (enemys.Length == 0) return;

	    var target = enemys.First();
	    var startRot = Quaternion.LookRotation(target.transform.position - transform.position);

	    var projectile = ((GameObject)Instantiate(ProjectilePrototype.gameObject, _out.position, startRot)).GetComponent<Projectile>();
	    projectile.SetTarget(target.transform);

	    projectile.Speed = ProjectileSpeed;
	    projectile.Damage = Damage;

	    _counter = 0;
	}
}