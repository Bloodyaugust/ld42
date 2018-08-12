using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

	public float TimeBetweenWaves;
	public Wave[] Waves;

	bool _isActive;
	bool _isWaiting;
	float _timeToNextSpawn;
	GameObject[] _spawnPoints;
	int _currentSpawnIndex;
	int _currentWaveIndex;
	int _spawnsLeft;
	Toolbox _toolbox;
	Wave _currentWave;

	public void DisableWaves () {
		_isActive = false;
	}

	public void EnableWaves () {
		_isActive = true;
	}

	void Awake () {
		_toolbox = Toolbox.Instance;

		_toolbox.GameLoaded.AddListener(OnGameSceneLoad);
		_toolbox.DisableWaves.AddListener(DisableWaves);
		_toolbox.EnableWaves.AddListener(EnableWaves);
	}

	// Update is called once per frame
	void Update () {
		if (_isActive) {
			_timeToNextSpawn -= Time.deltaTime;

			if (_timeToNextSpawn <= 0) {
				if (_spawnsLeft == 0) {
					_currentSpawnIndex++;

					if (_currentSpawnIndex == _currentWave.NumberSpawns.Length) {
						_currentSpawnIndex = 0;
						_isActive = false;
						_isWaiting = true;

						_currentWaveIndex++;
						if (_currentWaveIndex == Waves.Length) {
							_currentWaveIndex = 0;
						}

						_currentWave = Waves[_currentWaveIndex];
						_spawnsLeft = _currentWave.NumberSpawns[_currentSpawnIndex];
						_timeToNextSpawn = _currentWave.TimeBetweenSpawns;
					} else {
						_spawnsLeft = _currentWave.NumberSpawns[_currentSpawnIndex];
						_timeToNextSpawn = _currentWave.TimeBetweenSpawns;
					}
				} else {
					Debug.Log("Spawning on wave " + _currentWaveIndex);
					Debug.Log("Spawns left " + _spawnsLeft);
					Debug.Log("Spawn index " + _currentSpawnIndex);
					Instantiate(_currentWave.EnemyPrefabs[_currentSpawnIndex], _spawnPoints[Random.Range(0, _spawnPoints.Length)].transform.position, Quaternion.identity);

					_spawnsLeft--;
					_timeToNextSpawn = _currentWave.TimeBetweenSpawns;
				}
			}
		}

		if (_isWaiting) {
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

			if (enemies.Length == 0) {
				_isWaiting = false;
				_toolbox.WaveCompleted.Invoke();
				Debug.Log("Wave completed: " + _currentWaveIndex);
			}
		}
	}

	void OnGameSceneLoad () {
		_isActive = true;
		_isWaiting = false;
		_currentSpawnIndex = 0;
		_currentWaveIndex = 0;
		_currentWave = Waves[_currentWaveIndex];
		_spawnsLeft = _currentWave.NumberSpawns[_currentSpawnIndex];
		_timeToNextSpawn = _currentWave.TimeBetweenSpawns;

		_spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
	}
}
