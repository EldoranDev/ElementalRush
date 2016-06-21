using UnityEngine;
using System.Collections;
using System.Collections.Generic;


class WaveManager : MonoBehaviour {

    public Wave[] Waves;
    public GameObject Spawn;

    int _wave = 0;
    WorldManager _world;

    Transform _enemys;

    float _countDown = 5f;
    float _counter = 0f;

	// Use this for initialization
	void Start () {
        _enemys = GameObject.Find("__ENEMYS__").transform;
	}
	
	// Update is called once per frame
	void Update () {
        _counter += Time.deltaTime;

        if(_counter >= _countDown)
        {
            if(_wave < Waves.Length)
            {
                _counter = 0;
                _countDown = Waves[_wave].WaveLength;
                StartCoroutine(SpawnWave(_wave));
                _wave++;
            }
        }

        if(_enemys.childCount == 0)
        {
            if(_wave == Waves.Length)
            {
                //TODO GameOver we won
            }
        }
	}

    IEnumerator SpawnWave(int wave)
    {
        for(var i = 0; i < Waves[wave].EnemyCount; i++)
        {
            var g = (GameObject)Instantiate(Waves[wave].EnemyType, Spawn.transform.position, Quaternion.identity);

            g.transform.SetParent(_enemys);

            yield return new WaitForSeconds(Waves[wave].SpawnTimeOut);
        }
    }
}
