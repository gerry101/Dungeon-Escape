using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

	[SerializeField]
	private GameObject _shopPanel;

	// Open shop if player collides with shopkeeper
	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player") {
			Player player = other.GetComponent<Player>();
			if(player != null) {
				UIManager.Instance.OpenShop(player.gems);
			}
			_shopPanel.SetActive(true);
		}
	}

	// Close shop if player leaves shopkeeper
	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == "Player") {
			_shopPanel.SetActive(false);
		}
	} 
}
