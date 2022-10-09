using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour {
	public Image currenPlayer;
	public NewBehaviourScript players;
	int a;
	public GameObject aboutPanel;
	public GameObject settingPanel;

	public SettingData settingData;

	public Toggle qualityToggle;

	void Update(){
		a = PlayerPrefs.GetInt ("PCount", 0);
		if (a > players.player.Count - 1)
			a = 0;
		if (a < 0)
            a = players.player.Count - 1;
		currenPlayer.sprite = players.player [a];
		Debug.Log (a);
	}
	public void Play(){
		SceneManager.LoadScene ("Game1");
		Time.timeScale = 1;
		
	}
	public void Quit(){
		Application.Quit ();
	}
	//Character changing
	public void rArrow(){
		a++;
		PlayerPrefs.SetInt ("PCount", a);
	}
	public void lArrow(){
		a--;
		PlayerPrefs.SetInt ("PCount",a);
	}
	//About
	public void about(){
		aboutPanel.SetActive (true);
	}
	public void closeAbout(){
		aboutPanel.SetActive (false);
	}
	//Setting Stuffs
	public void OpenSetting()
    {
		settingPanel.SetActive(true);
		qualityToggle.isOn = settingData.isHigh;
    }
	public void CloseSetting()
    {
		settingPanel.SetActive(false);
    }
	public void SaveSetting()
    {
		settingData.isHigh = qualityToggle.isOn;
    }
}
