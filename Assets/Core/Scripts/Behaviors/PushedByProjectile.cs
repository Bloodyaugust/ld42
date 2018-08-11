using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushedByProjectile : MonoBehaviour {

	public float PushForce;

	Rigidbody2D _rigidbody;

	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == "PlayerProjectile") {
			_rigidbody.AddForce((collision.transform.position - transform.position).normalized * -PushForce, ForceMode2D.Impulse);
		}
	}
}
