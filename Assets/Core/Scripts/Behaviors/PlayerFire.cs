using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerFire : MonoBehaviour {

	public float ProjectileFireInterval;
	public float ProjectileForce;
	public GameObject ProjectilePrefab;

	float _timeToFire;
	Player _player;

	// Use this for initialization
	void Start () {
		_timeToFire = ProjectileFireInterval;
		_player = ReInput.players.GetPlayer(0);
	}

	// Update is called once per frame
	void Update () {
		_timeToFire -= Time.deltaTime;

		if (_timeToFire <= 0) {
			GameObject newProjectile = GameObject.Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);

			Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			worldMousePosition = new Vector3(worldMousePosition.x, worldMousePosition.y, 0);

			newProjectile.GetComponent<Rigidbody2D>().AddForce((worldMousePosition - transform.position).normalized * ProjectileForce, ForceMode2D.Impulse);

			_timeToFire = ProjectileFireInterval;
		}
	}
}
