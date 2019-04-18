using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour {

	private string _rewardVideoPlacementID = "rewardedVideo";
	public GameObject player;

	// Show reward ad video
	public void ShowRewardVideo() {
		if(Advertisement.IsReady(_rewardVideoPlacementID)) {
			var options = new ShowOptions {
				resultCallback = HandleRewardVideo
			};
			Advertisement.Show(_rewardVideoPlacementID, options);
		}
	}

	// Handle reward ad video callback
	void HandleRewardVideo(ShowResult result) {
		switch(result) {
			case ShowResult.Finished:
				Player playerScript = player.GetComponent<Player>();
				if(playerScript != null) {
					playerScript.gems += 100;
				}
				break;
			case ShowResult.Skipped:
				break;
			case ShowResult.Failed:
				break;
		}
	}
}
