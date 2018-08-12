using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {

	public int StartingHealth;
	public int HealthScalar;

	public UnityEvent Died = new UnityEvent();

	bool _isDying;
	int _health;
	Toolbox _toolbox;

	public void Damage (int amount) {
		_health -= amount;
	}

	void LateUpdate () {
		if (_isDying) {
			Died.Invoke();
			SendMessage("Die", null, SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		_toolbox = Toolbox.Instance;

		_health = StartingHealth + (HealthScalar * _toolbox.CurrentWave);
	}

	// Update is called once per frame
	void Update () {
		if (_health <= 0) {
			_isDying = true;
		}
	}
}
