using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

	public float SpawnForce;
	public float SpawnInterval;
	public GameObject EnemyPrefab;

	float _timeToSpawn;
	GameObject _player;

	// Use this for initialization
	void Start () {
		_player = GameObject.FindWithTag("Player");
		_timeToSpawn = SpawnInterval;
	}

	// Update is called once per frame
	void Update () {
		_timeToSpawn -= Time.deltaTime;

		if (_timeToSpawn <= 0) {
			GameObject newEnemy = GameObject.Instantiate(EnemyPrefab, transform.position, Quaternion.identity);

			newEnemy.GetComponent<Rigidbody2D>().AddForce((_player.transform.position - transform.position).normalized * SpawnForce, ForceMode2D.Impulse);

			_timeToSpawn = SpawnInterval;
		}
	}
}
