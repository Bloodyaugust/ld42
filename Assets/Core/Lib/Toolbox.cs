using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Com.LuisPedroFonseca.ProCamera2D;

public class Toolbox : Singleton<Toolbox> {
	protected Toolbox () {}

	public AudioClip EnemyDiedAudioClip;
	public AudioClip[] PlayerDamageAudioClips;
	public GameObject PlayerPrefab;
	public GameObject WaveCompletedText;
	public int CurrentWave;

	public UnityEvent EnemyDied = new UnityEvent();
	public UnityEvent EnableWaves = new UnityEvent();
	public UnityEvent DisableWaves = new UnityEvent();
	public UnityEvent GameLoaded = new UnityEvent();
	public UnityEvent WaveCompleted = new UnityEvent();

	float _cameraScreenSize;
	AudioSource _audioSource;
	DamageFromContactWithTag _playerDamage;
	GameObject _player;
	Health _playerHealth;
	ProCamera2DSpeedBasedZoom _cameraZoom;
	WaveController _waveController;

	void Awake () {
		_audioSource = GetComponent<AudioSource>();
		_cameraScreenSize = Camera.main.orthographicSize;
		_cameraZoom = GetComponent<ProCamera2DSpeedBasedZoom>();
		_waveController = GetComponent<WaveController>();

		EnemyDied.AddListener(OnEnemyDeath);
		WaveCompleted.AddListener(OnWaveCompleted);

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnEnemyDeath () {
		ProCamera2DShake.Instance.Shake("EnemyDeathShake");
		_audioSource.pitch = 1 + UnityEngine.Random.Range(-0.1f, 0.5f);
		_audioSource.volume = 0.5f;
		_audioSource.PlayOneShot(EnemyDiedAudioClip);
	}

	void OnPlayerDamageHalfTaken () {
		_cameraZoom.enabled = false;
		ProCamera2D.Instance.UpdateScreenSize(3f, 0.5f);
	}

	void OnPlayerDamageReset () {
		_cameraZoom.enabled = true;
		ProCamera2D.Instance.UpdateScreenSize(_cameraScreenSize, 0.5f);
	}

	void OnPlayerDamageTaken () {
		_audioSource.pitch = 1 + UnityEngine.Random.Range(-0.1f, 0.1f);
		_audioSource.volume = 1f;
		_audioSource.PlayOneShot(PlayerDamageAudioClips[UnityEngine.Random.Range(0, PlayerDamageAudioClips.Length)]);
		ProCamera2DShake.Instance.Shake("PlayerDamageShake");
	}

	void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
		if (scene.name == "game") {
			GameLoaded.Invoke();

			CurrentWave = 0;

			_player = GameObject.FindWithTag("Player");
			_playerDamage = _player.GetComponent<DamageFromContactWithTag>();
			_playerHealth = _player.GetComponent<Health>();

			_playerDamage.DamageTaken.AddListener(OnPlayerDamageTaken);
			_playerDamage.DamageIntervalHalf.AddListener(OnPlayerDamageHalfTaken);
			_playerDamage.DamageReset.AddListener(OnPlayerDamageReset);
			_playerHealth.Died.AddListener(OnPlayerDied);

			ProCamera2D.Instance.AddCameraTarget(_player.transform);
		}
	}

	void OnPlayerDied () {
		StartCoroutine(LoadGame());
	}

	void OnWaveCompleted () {
		WaveCompletedText.GetComponent<TextMesh>().text = "Wave " + (CurrentWave + 1) + " Complete!";
		Instantiate(WaveCompletedText, new Vector3(0, 0, WaveCompletedText.transform.position.z), Quaternion.identity);

		CurrentWave++;

		StartCoroutine(StartNextWave());
	}

	static public T RegisterComponent<T> () where T: Component {
		return Instance.GetOrAddComponent<T>();
	}

	IEnumerator StartNextWave () {
		Debug.Log("Starting next wave in " + _waveController.TimeBetweenWaves);
		yield return new WaitForSeconds(_waveController.TimeBetweenWaves);
		_waveController.EnableWaves();
	}

	IEnumerator LoadGame () {
		yield return new WaitForSeconds(3);

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		for (int i = 0; i < enemies.Length; i++) {
			Destroy(enemies[i]);
		}

		Instantiate(PlayerPrefab, PlayerPrefab.transform.position, Quaternion.identity);

		GameLoaded.Invoke();

		CurrentWave = 0;

		_player = GameObject.FindWithTag("Player");
		_playerDamage = _player.GetComponent<DamageFromContactWithTag>();
		_playerHealth = _player.GetComponent<Health>();

		_playerDamage.DamageTaken.AddListener(OnPlayerDamageTaken);
		_playerDamage.DamageIntervalHalf.AddListener(OnPlayerDamageHalfTaken);
		_playerDamage.DamageReset.AddListener(OnPlayerDamageReset);
		_playerHealth.Died.AddListener(OnPlayerDied);

		ProCamera2D.Instance.AddCameraTarget(_player.transform);
	}
}
