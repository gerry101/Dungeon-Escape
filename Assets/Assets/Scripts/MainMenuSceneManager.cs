using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneManager : MonoBehaviour {

	[SerializeField]
	private GameObject _MainMenuAnimationsPanel;
	private Animator _MainMenuAnimator;
	private bool _isAnimatorAvailable;

	// Use this for initialization
	void Start() {
		_isAnimatorAvailable = false;
		_MainMenuAnimator = _MainMenuAnimationsPanel.GetComponent<Animator>();
		if(_MainMenuAnimator != null) {
			_isAnimatorAvailable = true;
		}
	}

	// Load the Game scene
	public void LoadGameScene() {
		Debug.Log("Should load0");
		if(_isAnimatorAvailable) {
			_MainMenuAnimator.SetTrigger("FadeOut");
			StartCoroutine(WaitGameScene());
		} else {
			SceneManager.LoadScene("Game");
		}
	}

	// Quit the game
	public void QuitGame() {
		Application.Quit();
	}

	// Wait and Load Game Scene
	IEnumerator WaitGameScene() {
		yield return new WaitForSeconds(_MainMenuAnimator.GetCurrentAnimatorStateInfo(0).length);

		SceneManager.LoadScene("Game");
	}
}
