using UnityEngine;
using System.Collections;

public class StartGameButtons : MonoBehaviour 
{
	public string mainMenuScene;
	public string newGameScene;
	private SaveGameManager manager;
	
	public void NewGame()
	{
		Application.LoadLevel(newGameScene);
	}
	
	public void LoadGame()
	{
		manager = gameObject.AddComponent<SaveGameManager>();
	}
	
	public void Return()
	{
		Application.LoadLevel(mainMenuScene);
	}
}
