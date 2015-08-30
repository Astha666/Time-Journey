using UnityEngine;
using System.Collections;

public class MainMenuButtons : MonoBehaviour 
{
	public string startGameScene;

	public void QuitGame()
	{
		Application.Quit();
	}

	public void StartGame()
	{
		GameManager.FirstTimeOptionsMenu = true;
		Application.LoadLevel(startGameScene);
	}

	public void OptionsMenu()
	{

	}
}
