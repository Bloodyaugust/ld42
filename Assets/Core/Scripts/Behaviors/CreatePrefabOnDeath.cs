using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePrefabOnDeath : MonoBehaviour {

	public GameObject Prefab;

	void Die () {
		Instantiate(Prefab, transform.position, Quaternion.identity);
	}
}
