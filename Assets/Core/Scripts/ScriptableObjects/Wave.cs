using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class Wave : ScriptableObject {
  public float TimeBetweenSpawns;
  public GameObject[] EnemyPrefabs;
  public int[] NumberSpawns;
}
