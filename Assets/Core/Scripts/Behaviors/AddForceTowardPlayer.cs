using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceTowardPlayer : MonoBehaviour {

	public float MoveForce;
	public float MoveForceScalar;

	GameObject _player;
	Rigidbody2D _rigidbody;
	Toolbox _toolbox;

	// Use this for initialization
	void Start () {
		_player = GameObject.FindWithTag("Player");
		_rigidbody = GetComponent<Rigidbody2D>();
		_toolbox = Toolbox.Instance;

		MoveForce = MoveForce + (MoveForceScalar * _toolbox.CurrentWave);
	}

	// Update is called once per frame
	void Update () {
		if (_player != null) {
			_rigidbody.AddForce((_player.transform.position - transform.position).normalized * MoveForce);
		}
	}
}
