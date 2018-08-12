using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMeshRendererSortingOrder : MonoBehaviour {

	public int SortingOrder;

	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer>().sortingOrder = SortingOrder;
	}
}
