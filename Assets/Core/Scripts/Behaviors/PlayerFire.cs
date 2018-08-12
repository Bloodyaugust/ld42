using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerFire : MonoBehaviour {

	public AudioClip ProjectileNoise;
	public float ProjectileFireInterval;
	public float ProjectileFireIntervalScalar;
	public float ProjectileFireIntervalMax;
	public float ProjectileForce;
	public GameObject ProjectilePrefab;

	AudioSource _audioSource;
	float _timeToFire;
	Player _player;
	Toolbox _toolbox;

	// Use this for initialization
	void Start () {
		_audioSource = GetComponent<AudioSource>();
		_timeToFire = ProjectileFireInterval;
		_player = ReInput.players.GetPlayer(0);
		_toolbox = Toolbox.Instance;
	}

	// Update is called once per frame
	void Update () {
		_timeToFire -= Time.deltaTime;

		if (_timeToFire <= 0) {
			GameObject newProjectile = GameObject.Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);

			Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			worldMousePosition = new Vector3(worldMousePosition.x, worldMousePosition.y, 0);

			newProjectile.GetComponent<Rigidbody2D>().AddForce((worldMousePosition - transform.position).normalized * ProjectileForce, ForceMode2D.Impulse);

			_audioSource.pitch = 1 + UnityEngine.Random.Range(-0.1f, 0.2f);
			_audioSource.PlayOneShot(ProjectileNoise);

			_timeToFire = Mathf.Clamp(ProjectileFireInterval - (ProjectileFireIntervalScalar * _toolbox.CurrentWave), ProjectileFireIntervalMax, ProjectileFireInterval);
		}
	}
}
