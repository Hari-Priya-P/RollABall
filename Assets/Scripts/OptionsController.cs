using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class OptionsController : MonoBehaviour
{
	int c1, c2;

	public Text timer;
	public Text announcement;
	public GameObject g1, g2;

	static bool pause;
	private string wintext;
	private bool timerOn = false;
	private bool showPopUp = false;
	private float totalTime = 120;


	void Start() {

		pause = false;
		timerOn = true;
		timer.text = "Time: 2:00";
		announcement.text = "";
	}


	void Update() {

		if(timerOn) 
		{
			if(totalTime > 0) 
			{
				totalTime -= Time.deltaTime;
				DisplayTimer(totalTime);
				if(totalTime < 15) 
				{
					announcement.text = "Time almost up!";
				}
			}

			else 
			{
				totalTime = 0;
				timerOn = false;
				compareScores();
			}
		}
	}


	void compareScores() {

		announcement.text = "";
		// Get scores of the two players
		c1 = g1.GetComponent<PlayerController>().getCount();
		c2 = g2.GetComponent<PlayerController>().getCount();

		if(c1 < c2) {
			wintext += "PlayerB wins the game!";
		}
		else if(c1 > c2) {
			wintext += "PlayerA wins the game!";
		}
		else if(c1 == c2) {
			wintext += "Nobody wins the game!";
		}

		PauseGame();
		showPopUp = true;
	}


	// Popup Window
	void OnGUI()
    {
      if (showPopUp)
       {
         GUI.Window(0, new Rect((Screen.width/2)-150, (Screen.height/2)-75, 300, 150), ShowGUI, "Game Over!");
     
       }
    }


    void ShowGUI(int windowID)
    {
        GUI.Label(new Rect(60, 40, 300, 30), wintext);
 
        if (GUI.Button(new Rect(120, 80, 75, 30), "OK"))
        {
            showPopUp = false;
        }
        if(showPopUp == false) {
        	ResetGame();
        	PlayGame();
        }
    }


    // Display timer on screen
	void DisplayTimer(float displayTime) {
		displayTime += 1;
		float min = Mathf.FloorToInt(displayTime / 60);
		float sec = Mathf.FloorToInt(displayTime % 60);
		timer.text = "Time: " + string.Format("{0:00}:{1:00}", min, sec);
	}


	// For Reset button
    public void ResetGame() {
    	SceneManager.LoadScene("MiniGame");
    }


    // For Play/Pause button
    public void PauseorPlayGame() {
    	if(pause == false)
    		PauseGame();
    	else
    		PlayGame();
    }


    // Pause Game
    public static void PauseGame() {
    	Time.timeScale = 0;
    	pause = true;
    }


    // Resume Game
    public static void PlayGame() {
    	Time.timeScale = 1;
    	pause = false;
    }


    // Exit game to return to Main Menu
    public void QuitGame() {
    	SceneManager.LoadScene("MainMenu");
    }

}
