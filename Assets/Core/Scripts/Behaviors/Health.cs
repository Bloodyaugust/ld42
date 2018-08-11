using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public int StartingHealth;

	bool _isDying;
	int _health;

	public void Damage (int amount) {
		_health -= amount;
	}

	void LateUpdate () {
		if (_isDying) {
			SendMessage("Die", null, SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		_health = StartingHealth;
	}

	// Update is called once per frame
	void Update () {
		if (_health <= 0) {
			_isDying = true;
		}
	}
}
