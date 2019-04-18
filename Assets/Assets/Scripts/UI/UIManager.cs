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
	public Text gemCountText;
	public Image[] healthUnits;

	void Awake() {
		_instance = this;
	}

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

	// Update gem count text
	public void UpdateGemCountText(int updatedPlayerGemCount) {
		gemCountText.text = updatedPlayerGemCount.ToString();
	}

	public void UpdatePlayerHealthUnits(int maxHealth, int currentHealth) {
		if(currentHealth <= maxHealth * 0.25) {
			healthUnits[1].enabled = false;
		} else {
			healthUnits[1].enabled = true;
		}

		if(currentHealth <= maxHealth * 0.5) {
			healthUnits[2].enabled = false;
		} else {
			healthUnits[2].enabled = true;
		}

		if(currentHealth <= maxHealth * 0.75) {
			healthUnits[3].enabled = false;
		} else {
			healthUnits[3].enabled = true;
		}

		if(currentHealth <= 0) {
			healthUnits[0].enabled = false;
		} else {
			healthUnits[0].enabled = true;
		}
	}
}
