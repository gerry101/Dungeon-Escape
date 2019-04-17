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
	public Image selectionImage;

	// Update gem count text
	public void UpdateShop(int gemCount) {
		string updatedGemCount = gemCount.ToString() + "G";
		playerGemCount.text = updatedGemCount;
	}

	// Update y position of selection image
	public void UpdateSelectionImage(float width, float xPos, float yPos) {
		selectionImage.rectTransform.sizeDelta = new Vector2(width, selectionImage.rectTransform.sizeDelta.y);
		selectionImage.rectTransform.anchoredPosition = new Vector2(xPos, yPos);
	}

	void Awake() {
		_instance = this;
	}
}
