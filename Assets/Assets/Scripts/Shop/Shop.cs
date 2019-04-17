using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

	[SerializeField]
	private GameObject _shopPanel;
	private int _playerGemCount;
	private int _selectedItemIndex;

	void Start() {
		_playerGemCount = 0;
		_selectedItemIndex = 0;
	}

	// Open shop if player collides with shopkeeper
	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player") {
			Player player = other.GetComponent<Player>();
			if(player != null) {
				UIManager.Instance.UpdateShop(player.gems);
				_playerGemCount = player.gems;
			}
			_shopPanel.SetActive(true);	
		}
	}

	// Close shop if player leaves shopkeeper
	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == "Player") {
			_shopPanel.SetActive(false);
			Player player = other.GetComponent<Player>();
			if(player != null) {
				player.gems = _playerGemCount;
			}
		}
	} 

	// Check for what selection button was clicked
	public void SelectItem(int selectionIndex) {
		// 0 = Flame Sword
		// 1 = Boots of Flight
		// 2 = Key to Castle

		_selectedItemIndex = selectionIndex;

		switch(selectionIndex) {
			case 0:
				UIManager.Instance.UpdateSelectionImage(291.3f, -174f, 74.9f);
				break;
			case 1:
				UIManager.Instance.UpdateSelectionImage(354.8f, -142f, -25f);
				break;
			case 2:
				UIManager.Instance.UpdateSelectionImage(354.8f, -142f, -129f);
				break;
			default:
				break;
		}
	}

	// Allow player to buy item
	public void BuyItem() {
		int itemCost;

		// 0 = Flame Sword
		// 1 = Boots of Flight
		// 2 = Key to Castle

		switch(_selectedItemIndex) {
			case 0:
				itemCost = 200;
				CheckItemValidity(itemCost);
				break;
			case 1:
				itemCost = 400;
				CheckItemValidity(itemCost);
				break;
			case 2:
				itemCost = 100;
				CheckItemValidity(itemCost);
				break;
			default:
				break;
		}
	}

	// Check if item can be bought or not
	void CheckItemValidity(int itemCost) {
		if(itemCost <= _playerGemCount) {
			_playerGemCount -= itemCost;

			// Check if item is the Key to Castle
			if(_selectedItemIndex == 2) {
				GameManager.Instance.HasKeyToCastle = true;
			}

			UIManager.Instance.UpdateShop(_playerGemCount);
		} 
	}
}
