using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {

	public int enemyGemCount;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player") {
			Player player = other.GetComponent<Player>();
			if(player != null) {
				player.gems += enemyGemCount;
			}
			Destroy(gameObject);
		}
	}
}
