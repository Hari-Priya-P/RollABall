using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void GoToGame() {
    	SceneManager.LoadScene("MiniGame");
    }
    public void DisplayInstructions() {
    	SceneManager.LoadScene("InstructionManual");
    }
    public void ExitGame() {
    	Application.Quit();
    }
}
