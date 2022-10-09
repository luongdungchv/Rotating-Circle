using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class Manager : MonoBehaviour {
	public static event EventHandler OnSettingSaved;
	public static Manager ins;
	public static bool isPaused;

	public GameObject losePanel;
	public GameObject pausePanel;
	public bool isStop = false;

	public GameObject ball;

	public Image currentPlayerImg;

	public NewBehaviourScript player;

	public Toggle lightToggle;

	public int playerIndex;

	public Text score;
	public Text highscore;

	// Use this for initialization

	void Start () {
		
	    ins = this;

		playerIndex = PlayerPrefs.GetInt ("PCount", 0);
	}
	void Update(){
		if (Input.GetKey (KeyCode.Escape)) {
			Time.timeScale = 0;
			pausePanel.SetActive (true);
		}
		if (Input.GetKey (KeyCode.Menu)) {
			Time.timeScale = 0;
			pausePanel.SetActive (true);
		}
		if (Input.GetKey (KeyCode.Home)) {
			Time.timeScale = 0;
			pausePanel.SetActive (true);
		}

	
		if (playerIndex > player.player.Count - 1)
			playerIndex = 0;
		if (playerIndex < 0)
            playerIndex = player.player.Count - 1;
		currentPlayerImg.sprite = player.player [playerIndex];

	}
	public void Pause(){
        if (losePanel.activeSelf == false)
        {
            Time.timeScale = 0;
			isPaused = true;
            pausePanel.SetActive(true);
        }
	}
	public void Lose(){
        StartCoroutine(loseCo());
	}
	public void Resume(){
		Time.timeScale = 1;
		isPaused = false;
		pausePanel.SetActive (false);
		isPaused = false;
		GraphicManager.ins.settingData.isHigh = lightToggle.isOn;
		OnSettingSaved?.Invoke(this, EventArgs.Empty);
	}
	public void MainMenu(){
		SceneManager.LoadScene ("mainmenu");
		Manager.isPaused = false;
	}
	public void Restart(){
		losePanel.SetActive (false);
		SceneManager.LoadScene ("Game1");
		Time.timeScale = 1;
		Manager.isPaused = false;
	}
	public void rArrow(){
		
		playerIndex++;
		PlayerPrefs.SetInt ("PCount", playerIndex);

	}
	public void lArrow(){
		
		playerIndex--;
		PlayerPrefs.SetInt ("PCount", playerIndex);

	}
    public void release()
    {
		Debug.Log("click");
        if (Ball.ins.isReleased == false)
        {
            ball.gameObject.transform.parent.gameObject.tag = "edge";
            Ball.ins.Release();
        }
    }
    IEnumerator loseCo()
    {
        yield return new WaitForSeconds(0.8f);
        Time.timeScale = 0;
        losePanel.SetActive(true);
    }
}
