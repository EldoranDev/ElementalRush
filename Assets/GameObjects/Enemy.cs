﻿using UnityEngine;
using System.Collections;

class Enemy : MonoBehaviour {

    public float Speed;
    public float RotationSpeed;
    public int Reward;
    public float Life;
    public int Damage;

	public DamageType ArmorType;

    int _nextNode = 0;

    Transform _target;
    Transform _path;

	// Use this for initialization
	void Start () {
        _path = GameObject.Find("__PATH__").transform;

        // TODO Multipath tracks
        // if their is not path the complete map has no sense
        _target = _path.GetChild(_nextNode);
        _nextNode++;
    }
	
	// Update is called once per frame
	void Update () {
	    if((_target.position - transform.position).magnitude <= 0.1)
        {
            if(_nextNode >= _path.childCount)
            {
                WorldManager.Instance.Lifes -= Damage;

                Destroy(gameObject);
                return; // early cancel this because we do not longer exist
            }

            _target = _path.GetChild(_nextNode);
            _nextNode++;
        }

        var dir = (_target.position - transform.position).normalized;
        var targetRot = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, RotationSpeed);

        transform.Translate(dir * Speed * Time.deltaTime, Space.World);
	}

    public void DealDamage(float dmg)
    {
        Life -= dmg;

        if (Life <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        WorldManager.Instance.Money += Reward;
        WorldManager.Instance.Points += Reward;
        
        Destroy(gameObject);
    }
}
