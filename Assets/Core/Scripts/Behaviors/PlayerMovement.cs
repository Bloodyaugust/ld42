using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerMovement : MonoBehaviour {

	public float MoveForce;
	public float MoveForceScalar;

	Player _player;
	Rigidbody2D _rigidbody;
	Toolbox _toolbox;

	void Awake () {
		_player = ReInput.players.GetPlayer(0);
		_rigidbody = GetComponent<Rigidbody2D>();
		_toolbox = Toolbox.Instance;
	}

	// Use this for initialization
	void Start () {

	}

	void FixedUpdate () {
		Vector3 movement = new Vector3(_player.GetAxis("Horizontal"), _player.GetAxis("Vertical"), 0);

		float moveForce = MoveForce + (MoveForceScalar * _toolbox.CurrentWave);

		_rigidbody.AddForce(movement.normalized * moveForce);
	}
}
