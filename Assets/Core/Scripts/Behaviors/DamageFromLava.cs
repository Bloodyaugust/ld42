using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFromLava : MonoBehaviour {

	public int DamageAmount;
	public float DamageInterval;

	float _timeToDamage;
	Health _health;

	// Use this for initialization
	void Start () {
		_health = GetComponent<Health>();
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "Lava") {
			_timeToDamage = DamageInterval;
		}
	}

	void OnTriggerStay2D (Collider2D collider) {
		if (collider.gameObject.tag == "Lava") {
			_timeToDamage -= Time.deltaTime;

			if (_timeToDamage <= 0) {
				_health.Damage(DamageAmount);
				_timeToDamage = DamageInterval;
			}
		}
	}
}
