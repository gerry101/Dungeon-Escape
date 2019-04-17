using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;
	public static GameManager Instance {
		get {
			if(_instance == null) {
				Debug.LogError("GameManager is NULL");
			}

			return _instance;
		}
	}

	public bool HasKeyToCastle{get; set;}

	void Awake() {
		_instance = this;
	}
}
