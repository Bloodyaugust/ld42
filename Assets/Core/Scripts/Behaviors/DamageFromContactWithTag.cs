using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DamageFromContactWithTag : MonoBehaviour {

	public int DamageAmount;
	public float DamageInterval;
	public string[] DamageTags;

	public UnityEvent DamageIntervalHalf = new UnityEvent();
	public UnityEvent DamageReset = new UnityEvent();
	public UnityEvent DamageTaken = new UnityEvent();

	bool _halfIntervalFired;
	Dictionary<int, GameObject> _contactingGameObjects;
	float _timeToDamage;
	Health _health;

	// Use this for initialization
	void Start () {
		_contactingGameObjects = new Dictionary<int, GameObject>();
		_health = GetComponent<Health>();
	}

	void Update () {
		List<int> keys = new List<int>(_contactingGameObjects.Keys);
		foreach (int key in keys) {
			if (_contactingGameObjects[key] == null) {
				_contactingGameObjects.Remove(key);
			}
		}
		if (_contactingGameObjects.Count == 0) {
			if (_timeToDamage < DamageInterval) {
				DamageReset.Invoke();
			}
			_timeToDamage = DamageInterval;
			_halfIntervalFired = false;
		} else {
			_timeToDamage -= Time.deltaTime;

			if (_timeToDamage <= DamageInterval / 2 && !_halfIntervalFired) {
				DamageIntervalHalf.Invoke();
				_halfIntervalFired = true;
			}

			if (_timeToDamage <= 0) {
				_health.Damage(DamageAmount);
				DamageTaken.Invoke();
				_timeToDamage = DamageInterval;
				_halfIntervalFired = false;
			}
		}
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if (DamageTags.Contains(collision.gameObject.tag)) {
			_contactingGameObjects.Add(collision.gameObject.GetInstanceID(), collision.gameObject);
		}
	}

	void OnCollisionExit2D (Collision2D collision) {
		if (DamageTags.Contains(collision.gameObject.tag)) {
			_contactingGameObjects.Remove(collision.gameObject.GetInstanceID());
		}
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (DamageTags.Contains(collider.gameObject.tag)) {
			_contactingGameObjects.Add(collider.gameObject.GetInstanceID(), collider.gameObject);
		}
	}

	void OnTriggerExit2D (Collider2D collider) {
		if (DamageTags.Contains(collider.gameObject.tag)) {
			_contactingGameObjects.Remove(collider.gameObject.GetInstanceID());
		}
	}
}
