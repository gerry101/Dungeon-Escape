using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	private static UIManager _instance;
	public static UIManager Instance {
		get{
			if(_instance == null) {
				Debug.LogError("UIManager is NULL");
			}

			return _instance;
		}
	}

	public Text playerGemCount;

	public void OpenShop(int gemCount) {
		string updatedGemCount = gemCount.ToString() + "G";
		playerGemCount.text = updatedGemCount;
	}

	void Awake() {
		_instance = this;
	}
}
